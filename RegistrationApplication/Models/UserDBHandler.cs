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


        public bool AddUsers(UserModel userModel)
        {
            SqlConnector();
            SqlCommand cmd = new SqlCommand("AddNewUser", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email",userModel.Email);
            cmd.Parameters.AddWithValue("@Password", userModel.Password);
            sqlConnection.Open();
            var response = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return (response == 1) ? true : false;
        }

        public List<UserModel> GetUsers()
        {
            SqlConnector();
            List<UserModel> UserList = new List<UserModel>();
            SqlCommand cmd = new SqlCommand("GetUserDetails", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sqlConnection.Open();
            sd.Fill(dt);
            sqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
                UserList.Add(
                    new UserModel
                        {
                            Email = Convert.ToString(dr["Email"])
                        }
                    );

            return UserList;
        }
    }

}