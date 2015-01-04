using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Web_API_2NMCT1.Helper;

namespace Web_API_2NMCT1.Models
{
    public class OrganisationDA
    {

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims){
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MATTHIASSURFACE", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Organisation> GetOrganisations(IEnumerable<Claim> claims)
        {


            List<Organisation> list = new List<Organisation>();
            string sql = "SELECT * FROM Organisation";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Organisation c = new Organisation();
                c.ID = Convert.ToInt32(reader["ID"]);
                c.Login = reader["Login"].ToString();
                c.Password = reader["Password"].ToString();
                c.DbName = reader["DbName"].ToString();
                c.DbLogin = reader["DbLogin"].ToString();
                c.DbPassword = reader["DbPassword"].ToString();
                c.OrganisationName = reader["OrganisationName"].ToString();
                c.Address = reader["Address"].ToString();
                c.Email = reader["Email"].ToString();
                c.Phone = reader["Phone"].ToString();

                list.Add(c);
            }
            
            return list;
        }

        public static int InsertOrganisation(Organisation c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Organisations VALUES(@Login,@Password,@DbName,@Login,@DbPassword,@OrganisationName,@Address,@Email,@Phone)";
            DbParameter par1 = Database.AddParameter("connection", "@Login", c.Login);
            DbParameter par2 = Database.AddParameter("connection", "@Password", c.Password);
            DbParameter par3 = Database.AddParameter("connection", "@DbName", c.DbName);
            DbParameter par4 = Database.AddParameter("connection", "@DbPassword", c.DbPassword);
            DbParameter par5 = Database.AddParameter("connection", "@OrganisationName", c.OrganisationName);
            DbParameter par6 = Database.AddParameter("connection", "@Address", c.Address);
            DbParameter par7 = Database.AddParameter("connection", "@Email", c.Email);
            DbParameter par8 = Database.AddParameter("connection", "@Phone", c.Phone);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)),sql,par1, par2, par3,par4,par5,par6,par7,par8);
        }

        public static void UpdateOrganisation(Organisation c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Organisations SET Login=@Login, Password=@Password, DbName=@DbName, DbPassword=@DbPassword OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@Login", c.Login);
            DbParameter par2 = Database.AddParameter("connection", "@Password", c.Password);
            DbParameter par3 = Database.AddParameter("connection", "@DbName", c.DbName);
            DbParameter par4 = Database.AddParameter("connection", "@DbPassword", c.DbPassword);
            DbParameter par5 = Database.AddParameter("connection", "@OrganisationName", c.OrganisationName);
            DbParameter par6 = Database.AddParameter("connection", "@Address", c.Address);
            DbParameter par7 = Database.AddParameter("connection", "@Email", c.Email);
            DbParameter par8 = Database.AddParameter("connection", "@Phone", c.Phone);
            DbParameter par9 = Database.AddParameter("connection", "@ID", c.ID);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
        }

        public static void DeleteOrganisation(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Organisations WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}