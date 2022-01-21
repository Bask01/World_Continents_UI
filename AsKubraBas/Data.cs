using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AsKubraBas
{
    class Data
    {
        private static string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=WorldDB;Integrated Security=True";


        public static string ConnectionString { get => connStr; }

    }
}
