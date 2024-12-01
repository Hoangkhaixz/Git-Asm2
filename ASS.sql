CREATE DATABASE Hig;
USE Hig;
-- Tạo bảng Employee
CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY,
    EmployeeName NVARCHAR(255),
    Gender VARCHAR(10),
    Email VARCHAR(255),
    Phone VARCHAR(15),
    BirthOfDate DATE
);
INSERT INTO Employee (EmployeeID, EmployeeName, Gender, Email, Phone, BirthOfDate)
VALUES
(1, N'Nguyễn Văn Tuấn', 'Male', 'nguyenvana@gmail.com', '0987654321', '1990-01-01'),
(2, N'Lê Thị Huệ', 'Female', 'lethihue1@gmail.com', '0971234567', '1995-05-15'),
(3, N'Trần Văn Hà', 'Male', 'tranvanha25@gmail.com', '0968765432', '2005-12-20'),
(4, N'Phạm Thị Cụt', 'Female', 'phamthicut92@gmail.com', '0954321876', '1992-03-10');
-- Tạo bảng Customer với khóa chính tự tăng
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName NVARCHAR(255),
    Gender VARCHAR(10),
    BirthofDate DATE,
    Phone VARCHAR(15),
    Gmail NVARCHAR(255)
);
INSERT INTO Customer (CustomerName, Gender, BirthofDate, Phone, Gmail)
VALUES
(N'Lê Thị Huệ(Dog)', 'Female', '1990-08-15', '0911112222', 'hoangle@gmail.com'),
(N'Nguyễn Thị Hoa', 'Female', '1993-03-25', '0933334444', 'hoanguyen@gmail.com'),
(N'Phạm Văn Tùng', 'Male', '1985-10-10', '0944445555', 'tungpham@gmail.com'),
(N'Trần Thị Mai', 'Female', '1999-12-30', '0955556666', 'maitran@gmail.com'),
(N'Lý Đức Anh', 'Male', '2002-04-18', '0966667777', 'ducanhly@gmail.com'),
(N'Võ Thị Thu', 'Female', '1997-01-20', '0977778888', 'thuvo@gmail.com'),
(N'Đặng Văn Hùng', 'Male', '1987-09-05', '0988889999', 'hungdang@gmail.com'),
(N'Hoàng Thị Ngọc', 'Female', '2000-02-28', '0999990000', 'ngochoang@gmail.com');
-- Select
SELECT CustomerID, CustomerName, Gender,Gmail FROM Customer;
SELECT * FROM  Customer;
--Update
UPDATE Customer
SET Gmail = 'lethihue@gmail.com'
WHERE CustomerID = 1;
-- Dele
DELETE Customer
WHERE CustomerID = 6;
-- Tạo bảng Users với các khóa ngoại liên kết đến Employee và Customer
CREATE TABLE Users (
    UsersID INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(50),
    PasswordHash VARCHAR(255),
    Salt VARCHAR(32),
    Role VARCHAR(10) CHECK (Role IN ('Admin', 'User')) NOT NULL,
    EmployeeID INT,
    CustomerID INT,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
-- Tạo bảng Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(MAX),
    ProductImage VARBINARY(MAX),
    SizeProduct VARCHAR(255),
	InputPrice DECIMAL(10,2),
    InventoryPrice DECIMAL(10,2),
    SellingPrice DECIMAL(10,2),
);
-- Tạo bảng CustomerProduct với khóa ngoại
CREATE TABLE CustomerProduct (
    CustomerProductID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    ProductID INT,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
-- Tạo bảng Statistic với các khóa ngoại
CREATE TABLE Statistic (
    StatisticID INT PRIMARY KEY IDENTITY(1,1),
    CustomerProductID INT,
    EmployeeID INT,
	ProductID INT,
    QuantitySold INT,
    SaleDate DATE,
    TotalPrice DECIMAL(10,2),
	InputPrice DECIMAL(18,2),
    FOREIGN KEY (CustomerProductID) REFERENCES CustomerProduct(CustomerProductID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
ALTER TABLE Statistic
ADD CostPrice DECIMAL(18, 2);
ALTER TABLE Statistic
DROP COLUMN CostPrice;
ALTER TABLE Statistic
ADD CostPrice DECIMAL(18, 2) NOT NULL;
-- Tạo bảng USER với các khóa ngoại tham chiếu đúng
-- Xóa bảng theo thứ tự phụ thuộc
DROP TABLE IF EXISTS Statistic;
DROP TABLE IF EXISTS CustomerProduct;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Customer;
DROP TABLE IF EXISTS Employee;


