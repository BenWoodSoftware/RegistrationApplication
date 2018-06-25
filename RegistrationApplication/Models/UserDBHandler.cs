using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

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
            var hashedPass = ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(userModel.Password), GenerateSalt());

            SqlConnector();
            SqlCommand cmd = new SqlCommand("AddNewUser", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", userModel.Email.ToLower());
            cmd.Parameters.AddWithValue("@Password", Convert.ToBase64String(hashedPass));
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

        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[32];

                rng.GetBytes(salt);

                return salt;

            }
        }

        public static byte[] ComputeHMAC_SHA256(byte[] data, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                return hmac.ComputeHash(data);
            }
        }

        public bool CheckForDuplicate(string email)
        {
            var userRegister = GetUsers();
            foreach(var item in userRegister) 
            {
                if (email.ToLower().Equals(item.Email))
                {
                    return true;
                }
            };
            return false;
        }
        

    }

}