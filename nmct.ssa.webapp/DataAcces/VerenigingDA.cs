using nmct.ba.cashlessproject.model.Web;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace nmct.ssa.webapp.DataAcces
{
        public class VerenigingDA
        {
            public const string CONNECTIONSTRING = "ConnectionString";

            public static List<Vereniging> GetVerenigingen()
            {
                List<Vereniging> list = new List<Vereniging>();
                string sql = "SELECT ID, Login, Password, DbName, DbLogin, DBPassword, OrganisationName, Address, Email, Phone FROM Organisations";
                DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

                while (reader.Read())
                {
                    Vereniging ver = new Vereniging()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        DbName = reader["DbName"].ToString(),
                        DbLogin = reader["DbLogin"].ToString(),
                        DbPassword = reader["DbPassword"].ToString(),
                        OrganisationName = reader["OrganisationName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                    list.Add(ver);
                }
                reader.Close();
                return list;
            }

            public static Vereniging GetVerenigingById(int id)
            {
                string sql = "SELECT ID, Login, Password, DbName, DbLogin, DBPassword, OrganisationName, Address, Email, Phone FROM Organisations WHERE ID = @ID";
                DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
                DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par);
                Vereniging reg = new Vereniging();

                while (reader.Read())
                {
                    reg.Id = Convert.ToInt32(reader["ID"]);
                    reg.Login = reader["Login"].ToString();
                    reg.Password = reader["Password"].ToString();
                    reg.DbName = reader["DbName"].ToString();
                    reg.DbLogin = reader["DbLogin"].ToString();
                    reg.DbPassword = reader["DbPassword"].ToString();
                    reg.OrganisationName = reader["OrganisationName"].ToString();
                    reg.Address = reader["Address"].ToString();
                    reg.Email = reader["Email"].ToString();
                    reg.Phone = reader["Phone"].ToString();
                }
                reader.Close();
                return reg;
            }

            public static int NieuweVereniging(Vereniging vereniging)
            {
                string sql = "INSERT INTO Organisations (Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone) VALUES(@Login, @Password, @DbName, @DbLogin, @DBPassword, @OrganisationName, @Address, @Email, @Phone)";
                DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", vereniging.Login);
                DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", vereniging.Password);
                DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", vereniging.DbName);
                DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", vereniging.DbLogin);
                DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", vereniging.DbPassword);
                DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", vereniging.OrganisationName);
                DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", vereniging.Address);
                DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", vereniging.Email);
                DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", vereniging.Phone);
                int id = Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
                CreateDatabase(vereniging);
                return id;
            }

            public static int UpdateVereniging(Vereniging vereniging)
            {
                string sql = "UPDATE Organisations SET Login = @Login, Password = @Password, DbName = @DbName, DbLogin = @DbLogin, DbPassword = @DbPassword, OrganisationName = @OrganisationName, Address = @Address, Email = @Email, Phone = @Phone WHERE ID = @ID;";
                DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", vereniging.Login);
                DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", vereniging.Password);
                DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", vereniging.DbName);
                DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", vereniging.DbLogin);
                DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", vereniging.DbPassword);
                DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", vereniging.OrganisationName);
                DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", vereniging.Address);
                DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", vereniging.Email);
                DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", vereniging.Phone);
                DbParameter par10 = Database.AddParameter(CONNECTIONSTRING, "@ID", vereniging.Id);
                return Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);
            }


            private static void CreateDatabase(Vereniging o)
            {
                string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt"));
                string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);
                foreach (string commandText in RemoveGo(sql))
                {
                    Database.ModifyData(CONNECTIONSTRING, commandText);
                }

                DbTransaction trans = null;
                try
                {
                    trans = Database.BeginTransaction(CONNECTIONSTRING);

                    string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt"));
                    string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);

                    foreach (string commandText in RemoveGo(sql2))
                    {
                        Database.ModifyData(trans, commandText);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }

            private static string[] RemoveGo(string input)
            {
                string[] splitter = new string[] { "\r\nGO\r\n" };
                string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                return commandTexts;
            }


        }
    
}