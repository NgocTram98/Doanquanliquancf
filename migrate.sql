-- --------------------------------------------------------
-- Host:                         (local)
-- Server version:               Microsoft SQL Server 2019 (RTM) - 15.0.2000.5
-- Server OS:                    Windows 10 Pro 10.0 <X64> (Build 18362: )
-- HeidiSQL Version:             10.2.0.5599
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for Quanlycafe
CREATE DATABASE IF NOT EXISTS "Quanlycafe";
USE "Quanlycafe";

-- Dumping structure for table Quanlycafe.Account
CREATE TABLE IF NOT EXISTS "Account" (
	"UserName" NVARCHAR(100) NOT NULL,
	"DisplayName" NVARCHAR(100) NOT NULL DEFAULT (N'Kter'),
	"PassWord" NVARCHAR(1000) NOT NULL DEFAULT ((0)),
	"Type" INT(10,0) NOT NULL DEFAULT ((0)),
	PRIMARY KEY ("UserName")
);

-- Dumping data for table Quanlycafe.Account: -1 rows
/*!40000 ALTER TABLE "Account" DISABLE KEYS */;
INSERT INTO "Account" ("UserName", "DisplayName", "PassWord", "Type") VALUES
	('daisy', 'Cúc', 'A368B0DE8B91CFB3F91892FBF1EBD4B2', 0),
	('phuc', 'Phúc', 'A9B7BA70783B617E9998DC4DD82EB3C5', 1);
/*!40000 ALTER TABLE "Account" ENABLE KEYS */;

-- Dumping structure for table Quanlycafe.Bill
CREATE TABLE IF NOT EXISTS "Bill" (
	"id" INT(10,0) NOT NULL,
	"DateCheckIn" DATE(0) NOT NULL DEFAULT (getdate()),
	"DateCheckOut" DATE(0) NULL DEFAULT NULL,
	"idTable" INT(10,0) NOT NULL,
	"status" INT(10,0) NOT NULL DEFAULT ((0)),
	"discount" INT(10,0) NULL DEFAULT ((0)),
	PRIMARY KEY ("id")
);

-- Dumping data for table Quanlycafe.Bill: -1 rows
/*!40000 ALTER TABLE "Bill" DISABLE KEYS */;
INSERT INTO "Bill" ("id", "DateCheckIn", "DateCheckOut", "idTable", "status", "discount") VALUES
	(29, '2019-11-28', '2019-11-28', 2, 1, 0),
	(31, '2019-11-28', '2019-11-28', 5, 1, 1),
	(32, '2019-11-28', NULL, 6, 0, 0),
	(33, '2019-11-28', '2019-11-28', 7, 1, 0),
	(36, '2019-11-28', '2019-11-28', 10, 1, 0),
	(37, '2019-11-28', '2019-11-28', 5, 1, 0),
	(38, '2019-11-28', NULL, 8, 0, 0),
	(39, '2019-11-28', '2019-11-28', 9, 1, 0),
	(40, '2019-11-28', NULL, 10, 0, 0);
/*!40000 ALTER TABLE "Bill" ENABLE KEYS */;

-- Dumping structure for table Quanlycafe.BillInfo
CREATE TABLE IF NOT EXISTS "BillInfo" (
	"id" INT(10,0) NOT NULL,
	"idBill" INT(10,0) NOT NULL,
	"idFood" INT(10,0) NOT NULL,
	"count" INT(10,0) NOT NULL DEFAULT ((0)),
	PRIMARY KEY ("id")
);

-- Dumping data for table Quanlycafe.BillInfo: -1 rows
/*!40000 ALTER TABLE "BillInfo" DISABLE KEYS */;
INSERT INTO "BillInfo" ("id", "idBill", "idFood", "count") VALUES
	(32, 29, 2, 5),
	(35, 31, 2, 1),
	(36, 31, 1, 4),
	(37, 32, 1, 1),
	(38, 33, 1, 1),
	(41, 36, 1, 1),
	(42, 37, 2, 1),
	(43, 38, 2, 1),
	(44, 39, 2, 3),
	(45, 40, 2, 2);
/*!40000 ALTER TABLE "BillInfo" ENABLE KEYS */;

-- Dumping structure for table Quanlycafe.Food
CREATE TABLE IF NOT EXISTS "Food" (
	"id" INT(10,0) NOT NULL,
	"name" NVARCHAR(100) NOT NULL DEFAULT (N'chua dat ten'),
	"price" FLOAT(53) NOT NULL DEFAULT ((0)),
	"idCategory" INT(10,0) NULL DEFAULT NULL,
	PRIMARY KEY ("id")
);

-- Dumping data for table Quanlycafe.Food: -1 rows
/*!40000 ALTER TABLE "Food" DISABLE KEYS */;
INSERT INTO "Food" ("id", "name", "price", "idCategory") VALUES
	(1, 'Thịt bò hầm xương', 100000, 22),
	(2, 'Cá lóc hầm hoa cúc', 100000, 21);
/*!40000 ALTER TABLE "Food" ENABLE KEYS */;

-- Dumping structure for table Quanlycafe.FoodCategory
CREATE TABLE IF NOT EXISTS "FoodCategory" (
	"id" INT(10,0) NOT NULL,
	"name" NVARCHAR(100) NOT NULL DEFAULT (N'chua dat ten'),
	PRIMARY KEY ("id")
);

-- Dumping data for table Quanlycafe.FoodCategory: -1 rows
/*!40000 ALTER TABLE "FoodCategory" DISABLE KEYS */;
INSERT INTO "FoodCategory" ("id", "name") VALUES
	(21, 'Lẩu nồi to đùng'),
	(22, 'Món hầm'),
	(23, 'Súp mật ong'),
	(24, 'Thịt rừng'),
	(25, 'Món quay'),
	(26, 'Món kho'),
	(27, 'Lẩu nồi đất'),
	(28, 'Nước uống');
/*!40000 ALTER TABLE "FoodCategory" ENABLE KEYS */;

-- Dumping structure for table Quanlycafe.TableFood
CREATE TABLE IF NOT EXISTS "TableFood" (
	"id" INT(10,0) NOT NULL,
	"name" NVARCHAR(100) NOT NULL DEFAULT (N'Ban chua dat ten'),
	"status" NVARCHAR(100) NOT NULL DEFAULT (N'trong'),
	PRIMARY KEY ("id")
);

-- Dumping data for table Quanlycafe.TableFood: -1 rows
/*!40000 ALTER TABLE "TableFood" DISABLE KEYS */;
INSERT INTO "TableFood" ("id", "name", "status") VALUES
	(1, 'Bàn 0', 'trong'),
	(2, 'Bàn 1', 'trong'),
	(3, 'Bàn 2 có ma', 'trong'),
	(4, 'Bàn 3', 'trong'),
	(5, 'Bàn 4', 'trong'),
	(6, 'Bàn 5', 'co nguoi'),
	(7, 'Bàn 6', 'trong'),
	(8, 'Bàn 7', 'co nguoi'),
	(9, 'Bàn 8', 'trong'),
	(10, 'Bàn 9', 'co nguoi'),
	(1002, 'Bàn 11', 'trong');
/*!40000 ALTER TABLE "TableFood" ENABLE KEYS */;

-- Dumping structure for procedure Quanlycafe.USP_ChangeInfo
DELIMITER //
CREATE PROC USP_ChangeInfo
@account varchar(100), @oldpassword varchar(100), @password varchar(100), @displayname varchar(100)
AS 
BEGIN
	UPDATE dbo.Account SET PassWord=@password, DisplayName=@displayName WHERE UserName=@account and PassWord=@oldpassword
END
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_DeleteAccount
DELIMITER //
CREATE PROC USP_DeleteAccount
@username varchar(100)
as 
begin
	delete from dbo.Account where UserName = @username
end//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_EditAccount
DELIMITER //
CREATE PROC USP_EditAccount
@username nvarchar(100),@displayname nvarchar(100), @password nvarchar(100), @type int
AS 
BEGIN
	UPDATE dbo.Account SET 
	DisplayName = @displayname,
	PassWord = @password,
	Type = @type
	WHERE UserName = @username
END
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_EditCategory
DELIMITER //
CREATE PROC USP_EditCategory
@id int, @name nvarchar(100)
as
begin
	update dbo.foodcategory set name= @name Where id=@id
end
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_EditFood
DELIMITER //
CREATE PROC USP_EditFood 
@id int, @name nvarchar(100), @category int, @price float
as
begin
	update dbo.food set name= @name, price = @price, idCategory = @category Where id=@id
end
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_EditTable
DELIMITER //
CREATE PROC USP_EditTable
@id INT, @name nvarchar(100)
AS 
BEGIN
	
	UPDATE dbo.tablefood SET name=@name WHERE id=@id
END//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_FindFood
DELIMITER //
CREATE PROC USP_FindFood 
@name nvarchar(100)
as
begin
	SELECT * FROM food WHERE name LIKE N'%'+@name+'%'
end
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_GetAccountByUserName
DELIMITER //
CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
     SELECT * FROM dbo.Account WHERE UserName=@userName
END
EXEC dbo.USP_GetAccountByUserName @userName = N'Tram'
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_GetTableList
DELIMITER //
CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_InsertAccount
DELIMITER //
CREATE PROC USP_InsertAccount
@username nvarchar(100),@displayname nvarchar(100), @password nvarchar(100), @type int
AS 
BEGIN
	INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
	VALUES (@username, @displayname, @password, @type)
END
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_InsertBill
DELIMITER //
CREATE PROC USP_InsertBill
@idtable int 
AS
BEGIN
     INSERT INTO bill (DateCheckOut, idTable, status) 
     VALUES (Null, @idtable, 0)
END
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_InsertCategory
DELIMITER //
CREATE PROC USP_InsertCategory
@name nvarchar(100)
AS 
BEGIN
	INSERT INTO dbo.category (name)
	VALUES (@name)
END
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_InsertFood
DELIMITER //
CREATE PROC USP_InsertFood
@name nvarchar(100), @category int, @price float
AS 
BEGIN
	INSERT INTO dbo.food (name, price, idCategory)
	VALUES (@name, @price, @category)
END//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_InsertTable
DELIMITER //
CREATE PROC USP_InsertTable
@name nvarchar(100)
AS 
BEGIN
	INSERT INTO dbo.tablefood (name, status)
	VALUES (@name, N'trong')
END
//
DELIMITER ;

-- Dumping structure for procedure Quanlycafe.USP_Login
DELIMITER //

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
     SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord 
END//
DELIMITER ;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
