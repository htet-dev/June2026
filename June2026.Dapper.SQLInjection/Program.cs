using June2026.Dapper.SQLInjection;

Console.WriteLine("This is a practice program of Dapper and SQL Injection.");


DapperService service = new DapperService();
service.Read();
service.Update();
service.Delete();

Console.WriteLine("-- Login --");
Console.Write("Enter your username: ");
string username = Console.ReadLine();
Console.Write("Enter your password: ");
string password = Console.ReadLine();

LoginService loginService = new LoginService();
loginService.Login(username, password);

public class StudentDto
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string StudentNo { get; set; }
    public DateTime DateofBirth { get; set; }
    public string FatherName { get; set; }
    public string Email {  get; set; }
    public string MobileNo {  get; set; }
    public bool IsDelete {  get; set; }

}
