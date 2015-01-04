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
    public class SaleDA
    {

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MATTHIASSURFACE", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Sale> GetSales(IEnumerable<Claim> claims)
        {


            List<Sale> list = new List<Sale>();
            string sql = "SELECT * FROM Sale";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Sale c = new Sale();
                c.ID = Convert.ToInt32(reader["ID"]);
                c.Timestamp = Convert.ToDateTime(reader["Timestamp"]);
                c.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                c.RegisterID = Convert.ToInt32(reader["RegisterID"]);
                c.ProductID = Convert.ToInt32(reader["ProductID"]);
                c.Amount = Convert.ToDouble(reader["Amount"]);
                c.TotalPrice = Convert.ToDouble(reader["TotalPrice"]);

                list.Add(c);
            }

            return list;
        }

        public static int InsertSale(Sale c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Sales VALUES(@Timestamp,@CustomerID,@RegisterID,@ProductID,@Amount,@TotalPrice)";
            DbParameter par1 = Database.AddParameter("connection", "@Timestamp", c.Timestamp);
            DbParameter par2 = Database.AddParameter("connection", "@CustomerID", c.CustomerID);
            DbParameter par3 = Database.AddParameter("connection", "@RegisterID", c.RegisterID);
            DbParameter par4 = Database.AddParameter("connection", "@ProductID", c.ProductID);
            DbParameter par5 = Database.AddParameter("connection", "@Amount", c.Amount);
            DbParameter par6 = Database.AddParameter("connection", "@TotalPrice", c.TotalPrice);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6);
        }

        public static void UpdateSale(Sale c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Sales SET Login=@Login, Password=@Password, DbName=@DbName, DbPassword=@DbPassword SaleName=@SaleName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@Timestamp", c.Timestamp);
            DbParameter par2 = Database.AddParameter("connection", "@CustomerID", c.CustomerID);
            DbParameter par3 = Database.AddParameter("connection", "@RegisterID", c.RegisterID);
            DbParameter par4 = Database.AddParameter("connection", "@ProductID", c.ProductID);
            DbParameter par5 = Database.AddParameter("connection", "@Amount", c.Amount);
            DbParameter par6 = Database.AddParameter("connection", "@TotalPrice", c.TotalPrice);
            DbParameter par7 = Database.AddParameter("connection", "@ID", c.ID);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6, par7);
        }

        public static void DeleteSale(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Sales WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }

    }
}