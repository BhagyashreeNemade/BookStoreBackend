create database BookStore

use BookStore
drop table UserRegistration

create table UserRegistration(
UserId int not null identity(1,1) primary key,
FullName varchar(50),
Email varchar(50),
Password varchar(50),
Mobile_Number varchar(50)
)

select*from UserRegistration
go
create proc spAddUser(
@fullname varchar(50),
@email varchar(50) ,
@password varchar(50) ,
@mobilenumber varchar(50) 
)
as
begin
insert into UserRegistration
values(@fullname,@email,@password,@mobilenumber)
end
go

use BookStore
create table UserLogin(
Email varchar(50),
Password varchar(50)
)

select*from UserLogin
go
create proc spUserLogin

(
@email varchar(50),
@password varchar(50)
)

as
begin 
     select * from UserRegistration where (Email = @email and Password = @password)
end
go
select * from Users
go
CREATE PROCEDURE ForgotPassword
(
@EmailId varchar(180)
)
As
Begin
	Select * from UserRegistration where Email=@EmailId
End;

go

CREATE PROCEDURE SP_ResetPassword @EmailId VARCHAR(100), @Password VARCHAR (100)
AS
BEGIN
UPDATE UserRegistration
SET Password= @Password where EmailId=@EmailId
END
drop procedure ForgotPassword

(
@EmailId varchar(50)
)

as
begin 
     update UserRegistration set Password=null where Email=@EmailId
end 
drop procedure Reset_Password
GO
CREATE PROCEDURE [dbo].[Reset_Password] @Email VARCHAR(100), @Password VARCHAR (100)
AS
BEGIN
UPDATE UserRegistration SET Password = @Password WHERE Email = @Email
END
go

create procedure spResetPassword

(
@EmailId varchar(50),
@Password varchar(50)
)

as
begin 
     update UserRegistration set Password=@Password where Email=@EmailId
end 
go

create table Admin(
	AdminId int identity (1,1) primary key,
	FullName varchar(200) not null,
	Email varchar(200) not null,
	Password varchar(200) not null,
	MobileNumber varchar(200) not null
	)
go
create procedure spAdminLogin
(
	@email varchar(200),
    @password varchar(200)
)
as
BEGIN
	select * from Admin where Email = @email and Password = @password;
END
go

insert into Admin values('Admin','admin@gmail.com','123','7275707070')
delete from Admin where AdminId=1
select*from Admin

create table Books(
	BookId int identity (1,1) primary key,
	BookName varchar(200) not null,
	Author varchar(200) not null,
	BookImage varchar(max) not null,
	BookDetail varchar(max) not null,
	DiscountPrice float not null,
	ActualPrice float not null,
	Quantity int not null,
	Rating float,
	RatingCount int
	)
go
create procedure spAddBook
(
    @BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(200),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@Quantity int,
	@Rating float,
	@RatingCount int,
	@BookId int output
)
as
BEGIN
	insert into Books
	values(@BookName, @Author, @BookImage, @BookDetail, @DiscountPrice, @ActualPrice, @Quantity, @Rating, @RatingCount);
	set @BookId = SCOPE_IDENTITY()
	return @BookId;
END
go
create procedure spGetAllBooks
as
BEGIN
	select * from Books;
END
Go

create procedure spGetBookById
(
	@BookId int
)
as
BEGIN
	select * from Books where BookId = @BookId;
END
go

go

create procedure spDeleteBook
(
	@BookId int
)
as
BEGIN 
	delete from Books where BookId = @BookId;
END 
go

create procedure spUpdateBook
(
	@BookId int,
	@BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(200),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@Quantity int,
	@Rating float,
	@RatingCount int
)
as
BEGIN 
	update Books 
	set BookName = @BookName, Author = @Author, BookImage = @BookImage, BookDetail = @BookDetail, DiscountPrice = @DiscountPrice, ActualPrice = @ActualPrice, Quantity = @Quantity, Rating = @Rating, RatingCount = @RatingCount where BookId = @BookId;
END
create table WishList(
	WishListId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references UserRegistration(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)
go
	create procedure spAddToWishList
(
	@UserId int,
	@BookId int
)
as
begin
insert into WishList
values( @UserId, @BookId);
end
go


create procedure spGetAllWishList
(
	@UserId int
)
as
BEGIN
	select 
		w.WishListId,
		w.BookId,
		w.UserId,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice		
	from WishList w
	inner join Books b
	on w.BookId = b.BookId
	where w.UserId = @UserId;
END
go 

create procedure spRemoveFromWishList
(
	@WishListId int
)
as
BEGIN 
	delete from WishList where WishListId = @WishListId;
END 
go

create table Cart(
	CartId int identity (1,1) primary key,
	CartsQty int default 1,
	UserId int not null foreign key (UserId) references UserRegistration(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)
go

create procedure spAddToCart
(
    @CartsQty int,
	@UserId int,
	@BookId int
)
as
BEGIN
IF (NOT EXISTS(SELECT * FROM Cart WHERE BookId = @BookId and UserId=@UserId))
		begin
		insert into Cart
		values(@CartsQty, @UserId, @BookId);
		end
end
go

create procedure spRemoveFromCart
(
	@CartId int
)
as
BEGIN
	delete from Cart where CartId = @CartId;
END

go

create procedure spGetAllCart
(
	@UserId int
)
as
BEGIN
	select 
		c.CartId,
		c.BookId,
		c.UserId,
		c.CartsQty,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice,
		b.Quantity
	from Cart c
	inner join Books b
	on c.BookId = b.BookId
	where c.UserId = @UserId;
END
go

create procedure spUpdateQtyInCart
(
	@CartId int,
	@CartsQty int
)
as
BEGIN
	update Cart set CartsQty = @CartsQty where CartId = @CartId;
END 
go
create table AddressType(
	TypeId int identity(1,1) primary key,
	AddType varchar(100)
	)

--adding types--
insert into AddressType values('Home');
insert into AddressType values('Work');
insert into AddressType values('Other');

--select table--
select * from AddressType;

--table for Address info--

create table Address(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references UserRegistration(UserId)
	)

--select table--
select * from Address;

--stored procedure for address--
--add address--
create procedure spAddAddress(
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
	)
as
begin
	insert into Address
	values(@Address,@City,@State,@TypeId,@UserId);
end

--update address--
create procedure spUpdateAddress(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
	)
as
begin
	update Address set
	Address=@Address,City=@City,State=@State,TypeId=@TypeId where UserId=@UserId and AddressId=@AddressId;
end

--get all address of user--
create procedure spGetAllAddress(
	@UserId int
	)
as
begin
	select * from Address where UserId=@UserId;
end






create table Orders(
	OrderId int identity(1,1) primary key,
	OrderQty int not null,
	TotalPrice float not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES UserRegistration(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
	AddressId int not null FOREIGN KEY REFERENCES Address(AddressId)
	)
	select 



select * from orders
select * from Orders

--stored procedures--
--add order--
create procedure spAddOrder(
	@UserId int,
	@BookId int,
	@AddressId int
	)
as
	declare @TotalPrice int;
	declare @OrderQty int;
begin
	set @TotalPrice = (select DiscountPrice from Books where BookId = @BookId); 
	set @OrderQty = (select CartsQty from Cart where BookId = @BookId); 
	if(exists(select * from Books where BookId = @BookId))
	begin
		Begin try
			Begin Transaction
				if((select Quantity from Books where BookId = @BookId)>= @OrderQty)
				begin
					insert into Orders values(@OrderQty,@TotalPrice*@OrderQty,GETDATE(),@UserId,@BookId,@AddressId);
					update Books set Quantity = (Quantity - @OrderQty) where BookId = @BookId;
					delete from Cart where BookId = @BookId and UserId = @UserId; 
				end
			commit Transaction
		End try

		Begin Catch
				rollback;
		End Catch
	end
end

--get all orders--
create procedure spGetAllOrders(@UserId int)
as
begin
	select 
		Orders.OrderId, Orders.UserId, Orders.AddressId, Books.BookId,
		Orders.TotalPrice, Orders.OrderQty, Orders.OrderDate,
		Books.BookName, Books.Author, Books.BookImage
		from Books 
		inner join Orders on Orders.BookId = Books.BookId 
		where Orders.UserId = @UserId; 
end

--delete order--
create procedure spRemoveOrder(@OrderId int)
as
begin
	delete from Orders where OrderId=@OrderId;
end
go

go
create table Feedback(
	FeedbackId int identity (1,1) primary key,
	Rating float not null,
	Comment varchar(max) not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references UserRegistration(UserId)
	)

go
create procedure spAddFeedback
(
    @Rating float,
	@Comment varchar(max),
	@BookId int,
	@UserId int
)
as
BEGIN
insert into Feedback
values(@Rating, @Comment, @BookId, @UserId);
END
go

create procedure spGetAllFeedback
(
	@BookId int
)
as
BEGIN
	SELECT Feedback.FeedbackId,
		   Feedback.UserId,
		   Feedback.BookId,
		   Feedback.Comment,
		   Feedback.Rating, 
		   UserRegistration.FullName 
	FROM UserRegistration 
	INNER JOIN Feedback 
	ON Feedback.UserId = UserRegistration.UserId WHERE BookId=@BookId

END
go
delete  from Feedback where FeedbackId=1