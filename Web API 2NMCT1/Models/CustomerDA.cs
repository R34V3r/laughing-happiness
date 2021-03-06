﻿using nmct.ba.cashlessproject.model;
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
    public class CustomerDA
    {

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MATTHIASSURFACE", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Customer> GetCustomers(IEnumerable<Claim> claims)
        {


            List<Customer> list = new List<Customer>();
            string sql = "SELECT * FROM Customer";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Customer c = new Customer();
                c.ID = Convert.ToInt32(reader["ID"]);
                c.CustomerName = reader["CustomerName"].ToString();
                c.Address = reader["Address"].ToString();
                if (!DBNull.Value.Equals(reader["Picture"]))
                        c.Picture = (byte[])reader["Picture"];
                    else
                        c.Picture = new byte[0];
                c.Balance = Double.Parse(reader["Balance"].ToString());

                list.Add(c);
            }
            
            return list;
        }

        public static int InsertCustomer(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Customer VALUES(@CustomerName,@Address,@Picture,@Balance)";
            DbParameter par1 = Database.AddParameter("connection", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.AddParameter("connection", "@Address", c.Address);
            DbParameter par3 = Database.AddParameter("connection", "@Picture", c.Picture);
            DbParameter par4 = Database.AddParameter("connection", "@Balance", c.Balance);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)),sql,par1, par2, par3,par4);
        }

        public static void UpdateCustomer(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Customer SET CustomerName=@CustomerName, Address=@Address, Picture=@Picture, Balance=@Balance WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.AddParameter("connection", "@Address", c.Address);
            DbParameter par3 = Database.AddParameter("connection", "@Picture", c.Picture);
            DbParameter par4 = Database.AddParameter("connection", "@Balance", c.Balance);
            DbParameter par5 = Database.AddParameter("connection", "@ID", c.ID);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static void DeleteCustomer(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Customer WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}