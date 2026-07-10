using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace June2026.EFCorePractice.ModelFirst
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder _sb = new SqlConnectionStringBuilder
                {
                    DataSource = ".\\MSSQLSERVER01",
                    InitialCatalog = "June2026",
                    UserID = "sa",                        
                    Password = "sasa@123",                
                    TrustServerCertificate = true
                };
                optionsBuilder.UseSqlServer(_sb.ConnectionString);
            }
        }

        public DbSet<StaffEntity> Staffs { get; set; }
    }
}
