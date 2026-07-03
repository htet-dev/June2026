using June2026.AdoDotNet;
using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("This is about how ADO Dotnet works!");

AdoDotNetService service = new AdoDotNetService();
service.Read();
service.Create();
service.Update();
service.Delete();

Console.ReadLine();