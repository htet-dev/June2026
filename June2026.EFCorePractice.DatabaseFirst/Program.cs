using June2026.Database.AppDbContextModels;

Console.WriteLine("This is an EF Core - Database First Practice.");


AppDbContext db = new AppDbContext();
var lstStaff = db.TblStaffs
            .OrderBy(x => x.StaffName)
            .ToList();

foreach (var item in lstStaff)
{
    Console.WriteLine($"Staff ID: {item.StaffId}, Staff Name: {item.StaffName}");
}


var lstStudent = db.TblStudents
                    .Where(x => x.IsDelete == false)
                    .OrderBy(x => x.StudentName); 


foreach(var item in lstStudent)
{
    Console.WriteLine($"Student Number: {item.StudentNo}, Student Name {item.StudentName}");
}

