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
INSERT INTO dbo.Account
           (UserName ,
			DisplayName ,
			PassWord ,
			Type 
			)
VALUES     (
			N'T',
			N'Tran',
			N'1',
			0
			)	
CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
     SELECT * FROM dbo.Account WHERE UserName=@userName
END
EXEC dbo.USP_GetAccountByUserName @userName = N'Tram'

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

UPDATE dbo.TableFood SET STATUS = N'Có người'WHERE id =9

EXEC dbo.USP_GetTableList


-- Các thao tác sau là thao tác thêm dữ liệu vào CSDL, với mục đích kiểm thử

-- them category

insert into dbo.foodcategory (name)
VALUES (N'Lẩu nồi to'), (N'Món hầm'), (N'Súp mật ong'), 
	(N'Thịt rừng'), (N'Món quay'), (N'Món kho'), (N'Lẩu nồi đất'), (N'Nước uống');
select * from dbo.foodcategory

-- Thêm món ăn vào danh sách món
select * from dbo.food
delete from dbo.food
insert into dbo.food (name, price, idCategory) values 
	(N'Lẩu kim chi', 79000, 21),
	(N'Lẩu cá hồi', 45000, 21),
	(N'Lẩu thuốc bắc gà ác', 88000, 21),
	(N'Lẩu thuốc nam gà cồ', 99000, 21),
	(N'Gà hầm bắc kinh', 129000, 22),
	(N'Bò hầm kim chi cá mòi', 180000, 22),
	(N'Yến chưng mật ong đường phèn', 120000, 23),
	(N'Rong biển mật ong', 30000, 23),
	(N'Heo rừng nướng', 79000, 24),
	(N'Gà quay lu', 89000, 25),
	(N'Cá lóc kho tộ', 59000, 26),
	(N'Lẩu dụm bò nồi đất', 119000, 27),
	(N'Lẩu má heo nồi đất', 79000, 27),
	(N'Sting', 12000, 28),
	(N'Pepsi', 12000, 28),
	(N'7 up', 12000, 28),
	(N'Strong bow táo', 19000, 28),
	(N'Nước suối', 12000, 28)
	

-- Thêm vào bill
select * from bill
insert into bill (DateCheckOut, idTable, status) values
(NULL, 1, 0), (NULL, 5, 0)
-- Thêm chi tiết cho bill
select * from billinfo
insert into billinfo (idBill, idFood, count) values 
(1, 19, 1), 
(1, 28, 2), 
(2, 27, 2), 
(2, 20, 3);


