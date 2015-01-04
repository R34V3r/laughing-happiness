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
    public class RegisterDA
    {

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MATTHIASSURFACE", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Register> GetRegisters(IEnumerable<Claim> claims)
        {


            List<Register> list = new List<Register>();
            string sql = "SELECT * FROM Register";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Register c = new Register();
                c.ID = Convert.ToInt32(reader["ID"]);
                c.RegisterName = reader["RegisterName"].ToString();
                c.Device = reader["Device"].ToString();

                list.Add(c);
            }

            return list;
        }

        public static int InsertRegister(Register c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Registers VALUES(@RegisterName,@Device)";
            DbParameter par1 = Database.AddParameter("connection", "@RegisterName", c.RegisterName);
            DbParameter par2 = Database.AddParameter("connection", "@Device", c.Device);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);
        }

        public static void UpdateRegister(Register c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Registers SET RegisterName=@RegisterName,Device=@Device WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@RegisterName", c.RegisterName);
            DbParameter par2 = Database.AddParameter("connection", "@Device", c.Device);
            DbParameter par3 = Database.AddParameter("connection", "@ID", c.ID);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);
        }

        public static void DeleteRegister(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }

    }
}