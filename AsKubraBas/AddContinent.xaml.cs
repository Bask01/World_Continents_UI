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
    /// Interaction logic for AddContinent.xaml
    /// </summary>
    public partial class AddContinent : Window
    {
        public AddContinent()
        {
            InitializeComponent();
        }

          private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            string query = "Insert into Continent(ContinentName) values (@ContinentName)";

            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))

            {
                if (txtContname.Text == "")
                {
                    lbl_valid.Content = "";
                    lbl_valid.Content = "This Field is required!";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("ContinentName", txtContname.Text);
                    conn.Open();

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {
                        lbl_valid.Content = "";
                        MessageBox.Show("Continent inserted!");
                    }
                    else if (result == 0)
                        MessageBox.Show("Continent not inserted!");
                }            

            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            txtContname.Text = "";
            lbl_valid.Content = "";
            Close();
        }
    }
}
