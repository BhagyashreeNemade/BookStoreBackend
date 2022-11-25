create database BookStore

use BookStore
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

create proc ForgotPassword

(
@EmailId varchar(50)
)

as
begin 
     update UserRegistration set Password=null where Email=@EmailId
end 
go

create proc spResetPassword

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
	TypeId int identity (1,1) primary key,
	Type varchar(200)
	)

insert into AddressType values ('Home');
insert into AddressType values ('Work');
insert into AddressType values ('Others');


create table AddressTable(
	AddressId int identity (1,1) primary key,
	Address varchar(200) not null,
	City varchar(200) not null,
	State varchar(200) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references UserRegistration(UserId)
	)

go	
create procedure spAddAddress
(
    @Address varchar(200),
	@City varchar(200),
	@State varchar(200),
	@TypeId int,
	@UserId int
)
as
BEGIN 
	insert into AddressTable
	values(@Address, @City, @State, @TypeId, @UserId);
END
go

create procedure spDeleteAddress
(
	@AddressId int,
	@UserId int
)
as
BEGIN
	delete from AddressTable where AddressId = @AddressId and UserId = @UserId;
END
go 

go


create procedure spUpdateAddress
(
@AddressId int,
@Address varchar(300),
@City varchar(250),
@State varchar(250),
@TypeId int,
@UserId int
)
as
BEGIN
Update AddressTable set
Address = @Address, City = @City,
State = @State , TypeId = @TypeId,UserId=@UserId
where AddressId = @AddressId
end
go
create procedure spGetAllAddress
(
	@UserId int
)
as
BEGIN 
	select*from AddressTable where UserId=@UserId
	
	
END
go



create table Orders
(
         OrdersId int not null identity (1,1) primary key,
		 UserId INT NOT NULL,
		 FOREIGN KEY (UserId) REFERENCES UserRegistration(UserId),
		 AddressId int
		 FOREIGN KEY (AddressId) REFERENCES AddressTable(AddressId),
	     BookId INT NOT NULL
		 FOREIGN KEY (BookId) REFERENCES Books(BookId),
		 TotalPrice int,
		 BookQuantity int,
		 OrderDate Date
);

select * from orders
go
create PROC sp_AddingOrders
	@UserId INT,
	@AddressId int,
	@BookId INT ,
	@BookQuantity int
AS
	Declare @TotPrice int
BEGIN
	Select @TotPrice=DiscountPrice from Books where BookId = @BookId;
	IF (EXISTS(SELECT * FROM Books WHERE BookId = @BookId))
	begin
		IF (EXISTS(SELECT * FROM UserRegistration WHERE UserId = @UserId))
		Begin
		Begin try
			Begin transaction			
				INSERT INTO Orders(UserId,AddressId,BookId,TotalPrice,BookQuantity,OrderDate)
				VALUES ( @UserId,@AddressId,@BookId,@BookQuantity*@TotPrice,@BookQuantity,GETDATE())
				Update Books set Quantity=Quantity-@BookQuantity
				Delete from Cart where BookId = @BookId and UserId = @UserId
			commit Transaction
		End try
		Begin catch
			Rollback transaction
		End catch
		end
		Else
		begin
			Select 1
		end
	end 
	Else
	begin
			Select 2
	end	
END

go
create PROC sp_GetAllOrders
	@UserId INT
AS
BEGIN
	select 
		Books.BookId,Books.BookName,Books.Author,Books.DiscountPrice,Books.ActualPrice,Books.BookDetail,Books.BookImage,Orders.OrdersId,Orders.OrderDate
		FROM Books
		inner join Orders
		on Orders.BookId=Books.BookId where Orders.UserId=@UserId
END
go
create procedure spRemoveFromOrder
(
	@OrdersId int,
	@UserId int

)
as
declare @BookQuantity int,
		@Bookid int
begin
		if(exists(select*from Orders where OrdersId=@OrdersId))
			begin
			select @Bookid=BookId from Orders where OrdersId=@OrdersId and UserId=@UserId
			select @BookQuantity=BookQuantity from Orders where OrdersId=@OrdersId and UserId=@UserId
			update Books set Quantity=Quantity+@BookQuantity where BookId=@Bookid 
			delete from Orders where OrdersId=@OrdersId
			end
end

go
create table Feedback(
	FeedbackId int identity (1,1) primary key,
	Rating int not null,
	Comment varchar(max) not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references UserRegistration(UserId)
	)

go
create procedure spAddFeedback
(
    @Rating int,
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