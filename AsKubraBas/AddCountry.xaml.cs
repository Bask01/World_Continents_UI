using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace AsKubraBas
{
    /// <summary>
    /// Interaction logic for AddCountry.xaml
    /// </summary>
    public partial class AddCountry : Window
    {
        DataTable tblContinents;
        public AddCountry()
        {
            InitializeComponent();
            LoadContinent();

        }

        private void LoadContinent()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int continentId = Convert.ToInt32(cmbContinents.SelectedValue);
            string query = "Insert into Country(CountryName, Language, Currency, ContinentId) values (@CountryName, @Language, @Currency, @ContinentId)";

            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))

            {

                if (cmbContinents.SelectedValue == null || txtCountryname.Text == "")
                {
                    if(cmbContinents.SelectedValue == null)
                    {
                        lblValid1.Content = "";
                        lblValid1.Content = "This Field is required!";
                    }
                    
                    if (txtCountryname.Text == "")
                    {
                        lblValid2.Content = "";
                        lblValid2.Content = "This Field is required!";
                    }                 
                }

                else
                {
                    lblValid1.Content = "";
                    lblValid2.Content = "";
                    txtCountryname.Text = "";
                    txtCurrency.Text = "";
                    txtLanguage.Text = "";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("CountryName", txtCountryname.Text);
                    cmd.Parameters.AddWithValue("Language", txtLanguage.Text);
                    cmd.Parameters.AddWithValue("Currency", txtCurrency.Text);
                    cmd.Parameters.AddWithValue("ContinentId", cmbContinents.SelectedValue);
                    conn.Open();

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {
                        lblValid1.Content = "";
                        txtCountryname.Text = "";
                        txtCurrency.Text = "";
                        txtLanguage.Text = "";
                        MessageBox.Show("Country inserted!");
                    }
                    else if (result == 0)
                        MessageBox.Show("*Country not inserted!");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lblValid.Content = "";
            txtCountryname.Text = "";
            txtCurrency.Text = "";
            txtLanguage.Text = "";
            Close();
        }
    }
}
