using June2026.Database.AppDbContextModels;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

Console.WriteLine("This is an EFCore - Database First - Homework");

/*** 
 * Step 1 
 */
Console.WriteLine("This is Step 1:");

AppDbContext db = new AppDbContext();
var lstTicket = db.TblMovieTickets
    .Where(x => x.TicketPrice >= 15)
    .OrderBy(x => x.TicketPrice);

foreach (var item in lstTicket)
{
    Console.WriteLine($"Ticket Type: {item.TicketType}, Ticket Price: {item.TicketPrice}");
}


///*** 
// * Step 2 
// After Tbl_Task is deleted in the database and run scaffold in C#,
// TblTasks will be removed from the AppDBContextModels
// hence, the following codes will throw a compile error 
// as TblTasks cannot be found
// But running scaffold works perfectly without any error
// */

//Console.WriteLine("This is Step 2:");

//AppDbContext db2 = new AppDbContext();
//var lstTask = db2.TblTasks
//    .Where(x => !x.IsDelete)
//    .OrderBy(x => x.TaskCategory);

//foreach (var item in lstTask)
//{
//    Console.WriteLine($"Task Category: {item.TaskCategory}, Task Description: {item.TaskDescription}");
//}


/*** 
 * Step 3
 */
Console.WriteLine("This is Step 3:");

AppDbContext db3 = new AppDbContext();
var lstAddress = db3.TblAddresses    
    .OrderBy(x => x.AddressType);

foreach (var item in lstAddress)
{
    Console.WriteLine($"Address Type: {item.AddressType}, Address Description: {item.AddressDescription}");
}


/***
 * After Step 3, running scaffold works perfectly without any error.
 */