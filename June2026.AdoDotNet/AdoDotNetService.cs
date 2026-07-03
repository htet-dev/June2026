using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace June2026.AdoDotNet;

internal class AdoDotNetService
{

    private readonly SqlConnectionStringBuilder sb;     //declare this as readonly so that nobody is allowed to create a new object

    public AdoDotNetService()
    {
        sb = new SqlConnectionStringBuilder()       // 1. initialize a new SqlConnectionStrinbBuilder object , 2. initialize values to the object
        {
            DataSource = ".\\MSSQLSERVER01",
            InitialCatalog = "June2026",            //database name
            //sb.UserID = "sa",                     //MSSQL server installation with only Windows Authentication, hence it needs to be reinstalled
            //sb.Password = "sasa@123",             //MSSQL server installation with only Windows Authentication, hence it needs to be reinstalled
            IntegratedSecurity = true,              //it has to be true to pass Windows Authentication, otherwise cannot login to MSSQL Server
            TrustServerCertificate = true
        };
    }


    public void Read()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        Console.WriteLine("Connection opening...");        
        connection.Open();

        string query = "SELECT * FROM Tbl_Student;";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        foreach(DataRow item in dt.Rows)
        {
            Console.WriteLine("Student Name: " + item["StudentName"]);
            Console.WriteLine("Student Number: " + item["StudentNo"]);
            Console.WriteLine("Student Email: " + item["Email"]);
            DateTime dtDob = Convert.ToDateTime(item["DateofBirth"]);                //Before conversion, DateofBirth was 2000-09-18
            Console.WriteLine("Date of Birth: " + dtDob.ToString("dd-MMM-yyyy"));    // this will print in 18-Sept-2000 format
        }

        Console.WriteLine("Connection closing...");
        connection.Close();

    }

    public void Create()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        string query = @"INSERT INTO [dbo].[Tbl_Student]
           ([StudentName]
           ,[StudentNo]
           ,[DateofBirth]
           ,[FatherName]
           ,[Email]
           ,[MobileNo]
           ,[IsDelete])
        VALUES
        ('John Smith',
        'STU006',
        '2002-05-15',
        'David Smith',
        'john.smith@email.com',
        '0412345678',
        0);";
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();     //this returns the row count of how many records are newly inserted into the database
        connection.Close();
    }


    public void Update()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        string query = @"UPDATE [dbo].[Tbl_Student]
                            SET [StudentName] = 'Adam Smith'
                            WHERE [StudentNo] = 'STU006';";
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();      //this returns the row count of how many records are updated in the database
        connection.Close();
    }


    public void Delete()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        string query = @"DELETE FROM [dbo].[Tbl_Student]
                            WHERE StudentNo = 'STU006';";
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();      //this returns the row count of how many records are deleted in the database
        connection.Close();
    }
    
}
