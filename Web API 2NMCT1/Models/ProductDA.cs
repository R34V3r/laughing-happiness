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
    public class ProductDA
    {

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MATTHIASSURFACE", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Product> GetProducts(IEnumerable<Claim> claims)
        {


            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Product";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Product c = new Product();
                c.ID = Convert.ToInt32(reader["ID"]);
                c.ProductName = reader["ProductName"].ToString();
                c.Price = Convert.ToDouble(reader["Price"]);
                c.Available = Convert.ToBoolean(reader["Available"]);

                list.Add(c);
            }

            return list;
        }

        public static int InsertProduct(Product c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Products VALUES(@ProductName,@Price,@Available)";
            DbParameter par1 = Database.AddParameter("connection", "@ProductName", c.ProductName);
            DbParameter par2 = Database.AddParameter("connection", "@Price", c.Price);
            DbParameter par3 = Database.AddParameter("connection", "@DbName", c.Available);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);
        }

        public static void UpdateProduct(Product c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Products SET ProductName=@ProductName, Price=@Price, Available=@Available WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@ProductName", c.ProductName);
            DbParameter par2 = Database.AddParameter("connection", "@Price", c.Price);
            DbParameter par3 = Database.AddParameter("connection", "@Available", c.Available);
            DbParameter par4 = Database.AddParameter("connection", "@ID", c.ID);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4);
        }

        public static void DeleteProduct(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Products WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }

    }
}