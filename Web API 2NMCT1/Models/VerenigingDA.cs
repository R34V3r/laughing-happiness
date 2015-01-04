using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using Web_API_2NMCT1.Helper;

namespace ncmt.ba.cashlessproject.api.Models
{
    public class VerenigingDA
    {
        public static Organisation CheckCredentials(string username, string password)
        {

            string dumylogin = Cryptography.Encrypt(username);
            string dumypassword = Cryptography.Encrypt(password);
            string sql = "SELECT * FROM Vereniging WHERE Login=@Login AND Password=@Password";
            DbParameter par1 = Database.AddParameter("connection", "Login", Cryptography.Encrypt(username));
            DbParameter par2 = Database.AddParameter("connection", "Password", Cryptography.Encrypt(password));
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("connection"), sql, par1, par2);
                reader.Read();
                return new Organisation()
                {
                    ID = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbNaam"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = reader["DbPassword"].ToString(),
                    OrganisationName = reader["VerenigingNaam"].ToString(),
                    Address = reader["Adres"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Telefoon"].ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}