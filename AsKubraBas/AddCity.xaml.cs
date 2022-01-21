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
    /// Interaction logic for AddCity.xaml
    /// </summary>
    public partial class AddCity : Window
    {
        DataTable tblCountry;
        public AddCity()
        {
            InitializeComponent();
            LoadCountry();
        }


        private void LoadCountry()
        {
            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))
            {
                tblCountry = new DataTable();

                string query = "Select CountryId, CountryName from Country";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                tblCountry.Load(reader);

                cmbCountry.ItemsSource = tblCountry.DefaultView;
                cmbCountry.DisplayMemberPath = "CountryName";  // name column
                cmbCountry.SelectedValuePath = "CountryId";  // Id column
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            int countrytId = Convert.ToInt32(cmbCountry.SelectedValue);
            string query = "Insert into City(CityName, IsCapital, Population, CountryId) values (@CityName, @IsCapital, @Population, @CountryId)";

            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))

            {

                if (txtCity.Text == "" ||  cmbCountry.SelectedValue == null)
                {
                    if(txtCity.Text == "")
                    {
                        lbl_valid2.Content = "";
                        lbl_valid2.Content = "This field is required!";
                    }
                    if(cmbCountry.SelectedValue == null)
                    {
                        lbl_valid.Content = "";
                        lbl_valid.Content = "This field is required!";
                    }
                   
                }
                else
                {
                    lbl_valid.Content = "";
                    lbl_valid2.Content = "";
                    txtCity.Text = "";
                    txtPopulation.Text = "";
                    cbxCapital.IsChecked = false;

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("CityName", txtCity.Text);
                    cmd.Parameters.AddWithValue("IsCapital", cbxCapital.IsChecked);
                    cmd.Parameters.AddWithValue("Population", txtPopulation.Text);
                    cmd.Parameters.AddWithValue("CountryId", cmbCountry.SelectedValue);
                    conn.Open();

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {
                        lbl_valid.Content = "";
                        lbl_valid2.Content = "";
                        txtCity.Text = "";
                        txtPopulation.Text = "";
                        cbxCapital.IsChecked = false;
                        MessageBox.Show ("City inserted!");
                    }
                    else if (result == 0)
                        MessageBox.Show("City not inserted!");
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            lbl_valid.Content = "";
            lbl_valid2.Content = "";
            txtCity.Text = "";
            txtPopulation.Text = "";
            cbxCapital.IsChecked = false;
            Close();
        }
    }
}
