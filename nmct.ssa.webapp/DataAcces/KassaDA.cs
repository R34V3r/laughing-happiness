using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.Web;
using System.Data.Common;
using nmct.ssa.webapp.PresentationModels;
namespace nmct.ssa.webapp.DataAcces
{
    public class KassaDA
    {
        public const string CONNECTIONSTRING = "ConnectionString";

        public static List<Kassa> GetKassas()
        {
            List<Kassa> list = new List<Kassa>();
            string sql = "SELECT DISTINCT [Registers].[ID], [RegisterName],[Device],[PurchaseDate],[ExpiresDate] FROM [ProjectBA_IT].[dbo].[Registers]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Kassa kassa = new Kassa()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"])
                };
                list.Add(kassa);
            }
            reader.Close();
            return list;
        }

        public static List<KassaPM> GetAlleKassas()
        {
            List<KassaPM> list = new List<KassaPM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Organisations].[ID],[Organisations].[Login],[Organisations].[Password],[Organisations].[DbName],[Organisations].[DbLogin],[Organisations].[DbPassword],[Organisations].[OrganisationName],[Organisations].[Address],[Organisations].[Email],[Organisations].[Phone] FROM [ProjectBA_IT].[dbo].[Registers] LEFT JOIN [ProjectBA_IT].[dbo].Organisation_Register ON [Registers].[ID] = [Organisation_Register].[RegisterID] LEFT JOIN [ProjectBA_IT].[dbo].Organisations ON [Organisation_Register].[OrganisationID] = [Organisations].[ID] ORDER BY OrganisationName";
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

                KassaPM obj = new KassaPM();
                obj.kassa = kas;
                obj.vereniging = ver;

                list.Add(obj);
            }
            reader.Close();
            return list;
        }

        public static List<KassaPM> GetKassasByVereniging(int vereniging)
        {
            List<KassaPM> list = new List<KassaPM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Organisations].[ID],[Organisations].[Login],[Organisations].[Password],[Organisations].[DbName],[Organisations].[DbLogin],[Organisations].[DbPassword],[Organisations].[OrganisationName],[Organisations].[Address],[Organisations].[Email],[Organisations].[Phone] FROM [ProjectBA_IT].[dbo].[Registers] LEFT JOIN [ProjectBA_IT].[dbo].Organisation_Register ON [Registers].[ID] = [Organisation_Register].[RegisterID] LEFT JOIN [ProjectBA_IT].[dbo].Organisations ON [Organisation_Register].[OrganisationID] = [Organisations].[ID] WHERE Organisations.Id = @OrganisationId ORDER BY OrganisationName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationId", vereniging);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

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

                KassaPM obj = new KassaPM();
                obj.kassa = kas;
                obj.vereniging = ver;

                list.Add(obj);
            }
            reader.Close();
            return list;
        }

        public static int NieuweKassa(Kassa kassa)
        {
            string sql = "INSERT INTO Registers (RegisterName, Device, PurchaseDate, ExpiresDate) VALUES(@RegisterName, @Device, @PurchaseDate, @ExpiresDate)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", kassa.RegisterName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", kassa.Device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@PurchaseDate", kassa.PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ExpiresDate", kassa.ExpiresDate);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        }

        public static int KassaToevoegenVereniging(int kassaId, int verenigingId)
        {
            string sql = "INSERT INTO [ProjectBA_IT].[dbo].[Organisation_Register] ([OrganisationID],[RegisterID],[FromDate],[UntillDate]) VALUES (@OrganisationId,@RegisterId,null,null)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationId", verenigingId);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", kassaId);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2);
        }

        public static List<KassaPM> CheckKassaToekenning(int kassaId, int verenigingId)
        {
            List<KassaPM> list = new List<KassaPM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Organisations].[ID],[Organisations].[Login],[Organisations].[Password],[Organisations].[DbName],[Organisations].[DbLogin],[Organisations].[DbPassword],[Organisations].[OrganisationName],[Organisations].[Address],[Organisations].[Email],[Organisations].[Phone] FROM [ProjectBA_IT].[dbo].[Registers] LEFT JOIN [ProjectBA_IT].[dbo].Organisation_Register ON [Registers].[ID] = [Organisation_Register].[RegisterID] LEFT JOIN [ProjectBA_IT].[dbo].Organisations ON [Organisation_Register].[OrganisationID] = [Organisations].[ID] WHERE OrganisationID IS NOT NULL AND RegisterID = @RegisterId ORDER BY OrganisationName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", kassaId);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

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

                KassaPM obj = new KassaPM();
                obj.kassa = kas;
                obj.vereniging = ver;

                list.Add(obj);
            }
            reader.Close();
            return list;
        }

        public static int UpdateVerenigingKassa(int verenigingId, int kassaId)
        {
            string sql = "UPDATE [ProjectBA_IT].[dbo].[Organisation_Register] SET [OrganisationID] = @OrganisationId WHERE [RegisterID] = @RegisterId";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationId", verenigingId);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", kassaId);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2);
        }

    }
}