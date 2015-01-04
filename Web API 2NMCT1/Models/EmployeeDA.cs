using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Web_API_2NMCT1.Helper;
using nmct.ba.cashlessproject.model;

namespace Web_API_2NMCT1.Models
{
    public class EmployeeDA
    {

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MATTHIASSURFACE", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Employee> GetEmployees(IEnumerable<Claim> claims)
        {


            List<Employee> list = new List<Employee>();
            string sql = "SELECT * FROM Employee";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Employee c = new Employee();
                c.ID = Convert.ToInt32(reader["ID"]);
                c.EmployeeName = reader["EmployeeName"].ToString();
                c.Address = reader["Address"].ToString();
                c.Email = reader["Email"].ToString();
                c.Phone = reader["Phone"].ToString();

                list.Add(c);
            }

            return list;
        }

        public static int InsertEmployee(Employee c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Employees VALUES(@EmployeeName,@Address,@Email,@Phone)";
            DbParameter par5 = Database.AddParameter("connection", "@EmployeeName", c.EmployeeName);
            DbParameter par6 = Database.AddParameter("connection", "@Address", c.Address);
            DbParameter par7 = Database.AddParameter("connection", "@Email", c.Email);
            DbParameter par8 = Database.AddParameter("connection", "@Phone", c.Phone);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par5, par6, par7, par8);
        }

        public static void UpdateEmployee(Employee c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Employees SET EmployeeName=@EmployeeName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";
            DbParameter par5 = Database.AddParameter("connection", "@EmployeeName", c.EmployeeName);
            DbParameter par6 = Database.AddParameter("connection", "@Address", c.Address);
            DbParameter par7 = Database.AddParameter("connection", "@Email", c.Email);
            DbParameter par8 = Database.AddParameter("connection", "@Phone", c.Phone);
            DbParameter par9 = Database.AddParameter("connection", "@ID", c.ID);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par5, par6, par7, par8, par9);
        }

        public static void DeleteEmployee(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Employees WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("connection", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }

    }
}