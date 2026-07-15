# Entity Framework Core Assignment for Database First

## Assignment 

1. **First step** : ပထမဆုံး Table တစ်လုံး ဆောက်ပြီး Console မှာ Scaffold ဆွဲ ပြီး CRUD ထဲက Read Only ရေး
2. **Second step** : ဒုတိယ Table တစ်လုံး ဆောက် Scaffold ဆွဲ ပြီး Read Only ရေး
3. **Thirst Step** : ဒုတိယ Table ကိုဖျက်ပြီး တတိယ Table ဆောက် ပါ။ Scaffold ဆွဲပြ Read Only ရေး

Question => နောက်ဆုံး Test အပြီးမှာ Scaffold အလုပ်လုပ်သေးလား ဖြေပေးကြပါ

---

## Steps that I have done
### Step 1) create Tbl_MovieTicket table 
`
CREATE TABLE Tbl_MovieTicket
(
    TicketId INT IDENTITY(1,1) PRIMARY KEY,
    TicketType NVARCHAR(50) NOT NULL,
    TicketName NVARCHAR(100) NOT NULL,
    TicketPrice DECIMAL(10,2) NOT NULL
);
`

`
INSERT INTO Tbl_MovieTicket (TicketType, TicketName, TicketPrice)
VALUES
('Adult', 'Standard Adult', 20.00),
('Child', 'Standard Child', 12.50),
('Senior', 'Senior Citizen', 15.00),
('Student', 'Student Discount', 16.50);
`

- Run Scaffold command
- Record Reading in Program.cs


