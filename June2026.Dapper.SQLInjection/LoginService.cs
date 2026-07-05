using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace June2026.Dapper.SQLInjection
{
    internal class LoginService
    {
        private readonly SqlConnectionStringBuilder _sb = new SqlConnectionStringBuilder
        {
            DataSource = ".\\MSSQLSERVER01",
            InitialCatalog = "June2026",
            //UserID = "sa",                        //MSSQL server installation with only Windows Authentication, hence it needs to be reinstalled
            //Password = "sasa@123",                //MSSQL server installation with only Windows Authentication, hence it needs to be reinstalled
            IntegratedSecurity = true,              //it has to be true to pass Windows Authentication, otherwise cannot login to MSSQL Server
            TrustServerCertificate = true
        };

        public void Login(string username, string password)
        {
            using IDbConnection db = new SqlConnection(_sb.ConnectionString);
            string query = $"select * from Tbl_User where UserName = @Username and Password = @Password";
            var user = db.Query(query, new
            {
                Username = username,
                Password = password
            }).FirstOrDefault();

            if (user != null)
            {
                Console.WriteLine("Login successful.");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }

        }
    }
}
