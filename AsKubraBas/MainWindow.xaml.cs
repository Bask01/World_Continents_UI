using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;


namespace AsKubraBas
{

    public partial class MainWindow : Window
    {
        DataTable tblContinents;
        DataTable tblCountries;
        DataTable tblCities;

        public MainWindow()
        {
            InitializeComponent();
            LoadContinent();
        }

        public void LoadContinent()
        {
            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))
            {
                tblContinents = new DataTable();

                string query = "Select ContinentId, ContinentName from Continent";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                tblContinents.Load(reader);

                cmbContinents.ItemsSource = tblContinents.DefaultView;
                cmbContinents.DisplayMemberPath = "ContinentName";  // name column
                cmbContinents.SelectedValuePath = "ContinentId";  // Id column
            }
        }

        private void cmbContinents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))

            {
                tblCountries = new DataTable();
                int continentId = Convert.ToInt32(cmbContinents.SelectedValue);

                string query = "Select * from Country where ContinentId = @ContinentId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("ContinentId", continentId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                tblCountries.Load(reader);

                lstCountries.ItemsSource = tblCountries.DefaultView;
                lstCountries.DisplayMemberPath = "CountryName";
                lstCountries.SelectedValuePath = "CountryId";
            }

        }

        private void btnContinent_Click(object sender, RoutedEventArgs e)
        {
            AddContinent addContinent = new AddContinent();
            addContinent.ShowDialog();
        }

        private void lstCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))

            {
                tblCities = new DataTable();

                DataColumn[] pk = new DataColumn[1];
                pk[0] = tblCountries.Columns["CountryID"];
                tblCountries.PrimaryKey = pk;


                int countryId = Convert.ToInt32(lstCountries.SelectedValue);

                DataRow row = tblCountries.Rows.Find(countryId);

                string query = "Select * from City where CountryId = @countryId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("CountryId", countryId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                tblCities.Load(reader);

                dtgCities.ItemsSource = tblCities.DefaultView;
                if (row != null)
                {
                    lbl_Currency.Content = row["Currency"].ToString();
                    lbl_Language.Content = row["Language"].ToString();
                }
                else
                {
                    lbl_Language.Content = "";
                    lbl_Currency.Content = "";
                    dtgCities.ItemsSource = "";
                }
            }
        }

        private void btnCountry_Click(object sender, RoutedEventArgs e)
        {
            AddCountry addCountry = new AddCountry();
            addCountry.ShowDialog();
        }

        private void btnCity_Click(object sender, RoutedEventArgs e)
        {
            AddCity addCity = new AddCity();
            addCity.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

