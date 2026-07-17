Console.WriteLine("This is a practice of an Encapsulation in OOP");

/** 
 * Program draft idea
 * Room Class (Room has fields: RoomNumber, RoomType, RoomPrice, FloorNumber)
 */


/***
 * Valid case 
 */
Room r1 = new Room(3001, RoomType.Deluxe, 150, 3);
Console.WriteLine(r1.ToString());   
Console.WriteLine("----------------------------------");

/** 
 * Invalid Room Number
 */

try
{
    Room r2 = new Room(0, RoomType.Standard, 270, 5);
    Console.WriteLine(r2.ToString());

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
Console.WriteLine("----------------------------------");

/** 
 * Invalid Room Price
 */
try
{
    Room r3 = new Room(2701, RoomType.Suite, 0, 27);
    Console.WriteLine(r3.ToString());

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
Console.WriteLine("----------------------------------");

/** 
 * Invalid Floor Number
 */
try
{
    Room r4 = new Room(7002, RoomType.Suite, 0, -1);
    Console.WriteLine(r4.ToString());

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

public class Room
{
    public int RoomNumber { get; private set; }

    public RoomType RoomType { get; private set; }
    public decimal RoomPrice { get; private set; }

    public int FloorNumber { get; private set; }

    public Room(int roomNumber, RoomType roomType, decimal roomPrice, int floorNumber)
    {
        if (roomNumber <= 0)
            throw new ArgumentException
                ("Room number must be greater than zero.", 
                nameof(roomNumber));

        if (!Enum.IsDefined(typeof(RoomType), roomType))
            throw new ArgumentException
                ("Invalid room type.",
                nameof(roomType));

        if (floorNumber <= 0)
            throw new ArgumentException(
                "Floor number must be greater than zero.",
                nameof(floorNumber));

        RoomNumber = roomNumber;
        RoomType = roomType;
        FloorNumber = floorNumber;

        ChangeRoomPrice(roomPrice);        
    }

    public void ChangeRoomPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException(
                "Price must be greater than zero.",
                nameof(price));

        RoomPrice = price;
    }

    public override string ToString()
    {
        return $"Room Number: {RoomNumber}\n" +
               $"Room Type: {RoomType}\n" +
               $"Room Price: {RoomPrice:C}\n" +
               $"Floor Number: {FloorNumber}";
    }
}

public enum RoomType
{
    Standard,
    Deluxe,
    Suite
}