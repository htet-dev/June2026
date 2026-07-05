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
    internal class DapperService
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

        public void Read()
        {
            using IDbConnection db = new SqlConnection(_sb.ConnectionString);
            db.Open();
            List<StudentDto> lst = db.Query<StudentDto>("SELECT * FROM Tbl_Student;").ToList();
            foreach (var item in lst)
            {
                Console.WriteLine($"Student Id: {item.StudentId}, Name: {item.StudentName}");
            }
            
        }

        public void Update()
        {
            using IDbConnection db = new SqlConnection(_sb.ConnectionString);
            db.Open();
            int result = db.Execute("UPDATE Tbl_Student Set StudentName = 'James Wilson'" +
                                    "WHERE StudentId = 5");
            Console.WriteLine($"Rows updated: {result}");
        }

        public void Delete()
        {
            using IDbConnection db = new SqlConnection(_sb.ConnectionString);
            db.Open();
            int result = db.Execute("Delete From Tbl_Student where StudentId = 5");
            Console.WriteLine($"Rows deleted: {result}");
        }    

    }
}
