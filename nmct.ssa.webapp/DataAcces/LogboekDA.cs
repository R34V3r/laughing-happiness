using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using nmct.ba.cashlessproject.model.Web;
using nmct.ssa.webapp.PresentationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.webapp.DataAcces
{
    public class LogboekDA
    {
        public const string CONNECTIONSTRING = "ConnectionString";

        public static List<LogboekPM> GetLogboek()
        {
            List<LogboekPM> list = new List<LogboekPM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Errorlog].[RegisterID], [Errorlog].[Timestamp], [Errorlog].[Message], [Errorlog].[Stacktrace]  FROM ProjectBA_IT.[dbo].Registers INNER JOIN [ProjectBA_IT].[dbo].[Errorlog] ON [Registers].[ID] = [Errorlog].[RegisterID]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Kassa kas = new Kassa()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"])
                };
                Logboek log = new Logboek()
                {
                    RegisterId = Convert.ToInt32(reader["RegisterId"]),
                    Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                    Message = reader["Message"].ToString(),
                    Stacktrace = reader["Stacktrace"].ToString()
                };

                LogboekPM logs = new LogboekPM();
                logs.kassa = kas;
                logs.logboek = log;

                list.Add(logs);
            }
            reader.Close();
            return list;
        }

        public static List<LogboekPM> GetLogboekById(int kassa)
        {
            List<LogboekPM> list = new List<LogboekPM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Errorlog].[RegisterID], [Errorlog].[Timestamp], [Errorlog].[Message], [Errorlog].[Stacktrace]  FROM ProjectBA_IT.[dbo].Registers INNER JOIN [ProjectBA_IT].[dbo].[Errorlog] ON [Registers].[ID] = [Errorlog].[RegisterID] WHERE [Registers].[ID] = @RegisterId";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", kassa);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par);

            while (reader.Read())
            {
                Kassa kas = new Kassa()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"])
                };
                Logboek log = new Logboek()
                {
                    RegisterId = Convert.ToInt32(reader["RegisterId"]),
                    Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                    Message = reader["Message"].ToString(),
                    Stacktrace = reader["Stacktrace"].ToString()
                };

                LogboekPM logs = new LogboekPM();
                logs.kassa = kas;
                logs.logboek = log;

                list.Add(logs);
            }
            reader.Close();
            return list;
        }

    }
}