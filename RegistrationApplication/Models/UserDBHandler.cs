using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RegistrationApplication.Models
{
    public class UserDBHandler
    {

        private SqlConnection sqlConnection;
        private void SqlConnector()
        {
            var sqlConnectionString = ConfigurationManager.ConnectionStrings["UserConnection"].ToString();
            sqlConnection = new SqlConnection(sqlConnectionString);
        }
    }
}