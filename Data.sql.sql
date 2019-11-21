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
insert into bill (DateCheckOut, idTable, status) values
(Null, 3, 1)
insert into billinfo (idBill, idFood, count) values (1002, 19, 1), 
(1002, 28, 2)


select * from dbo.bill
select * from dbo.billinfo
select dbo.Food.name, dbo.billInfo.count, dbo.Food.price, dbo.Food.price*dbo.billInfo.count as sum from dbo.bill, dbo.billinfo, dbo.tableFood, dbo.Food
	where dbo.billinfo.idBill = dbo.bill.id and dbo.bill.idTable = dbo.tableFood.id 
	and dbo.billinfo.idFood = dbo.Food.id 
	and dbo.bill.status=0 
	and dbo.tableFood.id=1
	
select dbo.Food.name, dbo.billInfo.count, dbo.Food.price, dbo.Food.price*dbo.billInfo.count as sum 
	from dbo.bill, dbo.billinfo, dbo.tableFood, dbo.Food    
	where dbo.billinfo.idBill = dbo.bill.id and 
	dbo.bill.idTable = dbo.tableFood.id and dbo.billinfo.idFood = dbo.Food.id and dbo.bill.status= 0  and dbo.tableFood.id=1

CREATE PROC USP_InsertBill
@idtable int 
AS
BEGIN
     INSERT INTO bill (DateCheckOut, idTable, status) 
     VALUES (Null, @idtable, status)
END
select * from billinfo
select SUM (dbo.Food.price* dbo.billInfo.count) as sum 
            from dbo.bill, dbo.billinfo, dbo.tableFood, dbo.Food    where dbo.billinfo.idBill = dbo.bill.id and dbo.bill.idTable = dbo.tableFood.id
            and dbo.billinfo.idFood = dbo.Food.id 
            and dbo.bill.status= 0 
            and dbo.tableFood.id=5
            
select * from bill	 

alter table dbo.bill 
	add discount int default 0
update dbo.bill set discount = 0
select * from food

SELECT bill.id, bill.discount, bill.datecheckin, bill.datecheckout, bill.status, SUM (billinfo.count*food.price)*(100-bill.discount)/100 AS sum  
FROM dbo.bill, dbo.billinfo, dbo.Food  
WHERE dbo.billinfo.idBill = dbo.bill.id  AND dbo.billinfo.idFood = dbo.food.id 
GROUP BY dbo.bill.id, bill.datecheckin, bill.datecheckout, bill.status, bill.discount HAVING bill.datecheckin >= '11/19/2019' AND(bill.datecheckout <= '11/20/2019' OR ISNULL(bill.datecheckout, '') = '')

CREATE PROC USP_InsertFood
@name nvarchar(100), @category int, @price float
AS 
BEGIN
	INSERT INTO dbo.food (name, price, idCategory)
	VALUES (@name, @price, @category)
END


DROP PROC USP_Setpassword
CREATE PROC USP_ChangeInfo
@account varchar(100), @oldpassword varchar(100), @password varchar(100), @displayname varchar(100)
AS 
BEGIN
	UPDATE dbo.Account SET PassWord=@password, DisplayName=@displayName WHERE UserName=@account and PassWord=@oldpassword
END

select * from dbo.food
DROP PROC USP_EditFood 
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
end

SELECT * FROM food WHERE name LIKE N'%' + pattern + '%'

select * from account
EXEC USP_Setpassword @account=N'Tram', @oldpassword='1', @password=N'2'

select * from food