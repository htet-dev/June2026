using June2026.EFCorePractice.ModelFirst;

Console.WriteLine("This is an EF Core - Model First Practice.");

AppDbContext db = new AppDbContext();
var lst = db.Staffs.ToList();     //this is a LINQ method syntax.  it works the same as "Select * from Tbl_Staff"

foreach(var item in lst)
{
    Console.WriteLine(item.Id);
    Console.WriteLine(item.Name);
    Console.WriteLine("-------------------------");
}


/***  
 * Record Read 
 */
var staff2 = db.Staffs.Where(x => x.Id == 1000).FirstOrDefault();


/***  
 * Record Create 
 */

StaffEntity staffEntity = new StaffEntity
{
    Name = "Blueberry"
};

db.Staffs.Add(staffEntity);
int result = db.SaveChanges();     // Blueberry will be saved into the database after execution this line



/***  
 * Record Update 
 */
var staff1 = db.Staffs.Where(x => x.Id == 1).FirstOrDefault();
if (staff1 is null)
{
    Console.WriteLine("Staff not found.");
}
else
{
    staff1.Name = "Htet Htet";
    db.SaveChanges();
}


/*** 
 * Record Delete
 */

var staff3 = db.Staffs.Where(x => x.Id == 1).FirstOrDefault();
if (staff3 is null)
{
    Console.WriteLine("Staff not found.");
    return;
}
db.Staffs.Remove(staff3);
db.SaveChanges();


Console.ReadLine();