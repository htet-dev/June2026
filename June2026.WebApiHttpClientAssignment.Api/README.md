# Console App calls Web APIs using HttpClient

## Assignment 

### There are total 8 Endpoints 

Product Controller:
HTTP Method				Endpoint					Action						
GET						/api/product				Get all products			
GET						/api/product/{id}			Get a product by ID
POST					/api/product				Create a new product (data comes from JSON request body)
PATCH					/api/product/{id}			Update an existing product (data comes from JSON request body)
DELETE					/api/product/{id}			Delete a product			

---

Sale Controller:
HTTP Method				Endpoint					Action
GET						/api/sale					Get all sales
GET						/api/sale/{id}				Get sale by ID
POST					/api/sale					Create a new sale (data comes from JSON request body)


## Database

## Create the `Product` Table

The `Description` column stores additional information about the product beyond the `ProductName`.

```sql
CREATE TABLE Tbl_Product
(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    Price DECIMAL(10,2) NOT NULL,
    ExpiryDate DATE NULL,
    TotalAvailableQty INT NOT NULL
);
```

## Insert Sample Data

```sql
INSERT INTO Tbl_Product
(
    ProductName,
    Description,
    Price,
    ExpiryDate,
    TotalAvailableQty
)
VALUES
('Milk', 'Full cream fresh milk (2L)', 4.50, '2026-08-15', 100),
('Bread', 'Wholemeal sliced bread', 3.20, '2026-08-05', 50),
('Eggs', 'Free-range eggs (12 pack)', 8.99, '2026-08-20', 75),
('Butter', 'Salted butter (500g)', 6.50, '2026-09-10', 40),
('Cheese', 'Cheddar cheese (500g)', 7.80, '2026-10-15', 35),
('Rice', 'Jasmine rice (5kg)', 18.99, NULL, 60),
('Coffee', 'Instant coffee (200g)', 12.50, '2027-03-30', 45),
('Laptop', '15.6-inch laptop with Intel Core i7, 16GB RAM, 512GB SSD', 1299.99, NULL, 15),
('Wireless Mouse', 'Bluetooth wireless optical mouse', 29.95, NULL, 120),
('Keyboard', 'Mechanical USB keyboard', 79.99, NULL, 80);
```

## Create the `Sale` Table

```sql
CREATE TABLE Tbl_Sale
(
    SaleId INT IDENTITY(1,1) PRIMARY KEY,
    SaleDate DATETIME NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL
);
```

# Insert Sample Data

```sql
INSERT INTO Tbl_Sale
(
    SaleDate,
    TotalAmount
)
VALUES
('2026-07-01 09:15:00', 25.50),
('2026-07-02 10:30:00', 120.00),
('2026-07-03 11:45:00', 89.99),
('2026-07-04 13:20:00', 45.75),
('2026-07-05 15:10:00', 199.50),
('2026-07-06 16:40:00', 65.00),
('2026-07-07 18:05:00', 150.25),
('2026-07-08 19:30:00', 39.95),
('2026-07-09 20:15:00', 275.80),
('2026-07-10 21:00:00', 99.99);
```

## Create the `Sale Detail` Table

```sql
CREATE TABLE Tbl_SaleDetail
(
    SaleDetailId INT IDENTITY(1,1) PRIMARY KEY,
    SaleId INT NOT NULL,
    ProductId INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    Qty INT NOT NULL,

    CONSTRAINT FK_SaleDetail_Sale
        FOREIGN KEY (SaleId)
        REFERENCES Sale(SaleId),

    CONSTRAINT FK_SaleDetail_Product
        FOREIGN KEY (ProductId)
        REFERENCES Product(ProductId)
);
```

# Insert Sample Data
```sql
INSERT INTO Tbl_SaleDetail
(
    SaleId,
    ProductId,
    UnitPrice,
    Qty
)
VALUES
(1, 1, 4.50, 2),
(1, 2, 3.20, 1),
(2, 4, 6.50, 3),
(3, 3, 8.99, 2),
(4, 5, 7.80, 1),
(5, 8, 1299.99, 1),
(6, 9, 29.95, 2),
(7, 10, 79.99, 1),
(8, 6, 18.99, 4),
(9, 7, 12.50, 3);
```

## Run Scaffold command

### Step 1: Open Command Prompt

1. Open the solution folder in **File Explorer**.
2. Press **Ctrl + L** to focus the address bar.
3. Type: 

```text
cmd
```

4. Press **Enter**.

### Step 2: Run the Scaffold Command

Run the following command to generate the `DbContext` and entity models from the SQL Server database:

```bash
dotnet ef dbcontext scaffold "Server=DESKTOP-J11BIHB\MSSQLSERVER01;Database=June2026;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f
```


### Generated Output

The scaffold command will generate:

- **`AppDbContext.cs`** – Entity Framework Core database context.
- **`AppDbContextModels`** – Folder containing the entity classes (`Tbl_Product`, `Tbl_Sale`, `Tbl_SaleDetail`).