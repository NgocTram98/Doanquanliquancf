CREATE DATABASE Quanlycafe
USE Quanlycafe
CREATE TABLE TableFood
(
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(100) NOT NULL DEFAULT N'Ban chua dat ten', 
    status NVARCHAR(100) NOT NULL DEFAULT N'trong'
)

CREATE TABLE Account 
(
    UserName NVARCHAR(100) PRIMARY KEY,
    DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Kter',
    PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
    Type INT NOT NULL DEFAULT 0
)
CREATE TABLE FoodCategory
(
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(100) NOT NULL DEFAULT N'chua dat ten'
)

CREATE TABLE Food
(
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(100) NOT NULL DEFAULT N'chua dat ten',
    price FLOAT NOT NULL DEFAULT 0,
    idCategory INT NOT NULL
    FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
CREATE TABLE Bill
(
    id INT IDENTITY PRIMARY KEY,
    DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
    DateCheckOut DATE, -- thời gian checkout
    idTable INT NOT NULL, -- bàn nào
    status INT NOT NULL DEFAULT 0 -- check out hay chưa: 0 - chưa, 1: rồi
    FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
CREATE TABLE BillInfo
(
    id INT IDENTITY PRIMARY KEY,
    idBill INT NOT NULL,
    idFood INT NOT NULL,
    count INT NOT NULL DEFAULT 0
    FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
    FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
--them tai khoan
INSERT INTO dbo.Account
           (UserName ,
			DisplayName ,
			PassWord ,
			Type 
			)
VALUES     (
			N'Tram',
			N'Ngoc Tram',
			N'1',
			1
			)		
			
CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
     SELECT * FROM dbo.Account WHERE UserName=@userName
END

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
     SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord 
END
--tao danh sach ban an
DECLARE @i INT = 0
WHILE @i <=10
BEGIN
	INSERT dbo.TableFood (name) VALUES (N'Bàn ' + CAST(@i As nvarchar(100)))
	SET @i=@i+1
END
--
CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood

-- Các thao tác sau là thao tác thêm dữ liệu vào CSDL, với mục đích kiểm thử
-- them category

-- Thêm vào bill
CREATE PROC USP_InsertBill
@idtable int 
AS
BEGIN
     INSERT INTO bill (DateCheckOut, idTable, status) 
     VALUES (Null, @idtable, 0)
END

alter table dbo.bill 
	add discount int default 0	
update dbo.bill set discount = 0

CREATE PROC USP_InsertFood
@name nvarchar(100), @category int, @price float
AS 
BEGIN
	INSERT INTO dbo.food (name, price, idCategory)
	VALUES (@name, @price, @category)
END

-- đổi thông tin của người dùng
CREATE PROC USP_ChangeInfo
@account varchar(100), @oldpassword varchar(100), @password varchar(100), @displayname varchar(100)
AS 
BEGIN
	UPDATE dbo.Account SET PassWord=@password, DisplayName=@displayName WHERE UserName=@account and PassWord=@oldpassword
END

CREATE PROC USP_EditFood 
@id int, @name nvarchar(100), @category int, @price float
as
begin
	update dbo.food set name= @name, price = @price, idCategory = @category Where id=@id
end


CREATE PROC USP_FindFood 
@name nvarchar(100)
as
begin
	SELECT * FROM food WHERE name LIKE N'%'+@name+'%'
END

CREATE PROC USP_InsertCategory
@name nvarchar(100)
AS 
BEGIN
	INSERT INTO dbo.category (name)
	VALUES (@name)
END

CREATE PROC USP_EditCategory
@id int, @name nvarchar(100)
as
begin
	update dbo.foodcategory set name= @name Where id=@id
end

CREATE PROC USP_InsertTable
@name nvarchar(100)
AS 
BEGIN
	INSERT INTO dbo.tablefood (name, status)
	VALUES (@name, N'trong')
END

CREATE PROC USP_EditTable
@id INT, @name nvarchar(100)
AS 
BEGIN
	
	UPDATE dbo.tablefood SET name=@name WHERE id=@id
END


CREATE PROC USP_InsertAccount
@username nvarchar(100),@displayname nvarchar(100), @password nvarchar(100), @type int
AS 
BEGIN
	INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
	VALUES (@username, @displayname, @password, @type)
END

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


CREATE PROC USP_DeleteAccount
@username varchar(100)
as 
begin
	delete from dbo.Account where UserName = @username
end