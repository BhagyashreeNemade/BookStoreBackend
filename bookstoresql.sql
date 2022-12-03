create database BookStoreDB

--Use DB--
use BookStoreDB

--Table for User--
create table Users (
	UserId int identity (1,1) primary key,
	FullName varchar(100) not null,
	EmailId varchar(100) not null,
	Password varchar(100) not null,
	MobileNumber varchar(100) not null
);

--select table--
select * from Users

--Stored procedures for user--

--Register--
create procedure spRegister(
	@FullName varchar(100),
	@EmailId varchar(100),
	@Password varchar(100),
	@MobileNumber varchar(100)
	)
as
begin
	if(not exists(select EmailId from Users where EmailId=@EmailId))
	begin
		insert into Users
		values(@FullName,@EmailId,@Password,@MobileNumber);
	end
end

--Login--
create procedure spLogin(
	@EmailId varchar(100)
	)
as
begin
	select * from Users where EmailId=@EmailId;
end

--Forget Password--
create procedure spForget(
	@EmailId varchar(100)
	)
as
begin
	select * from Users where EmailId=@EmailId;
end

--reset password--
create procedure spResetPassword(
	@EmailId varchar(100),
	@Password varchar(100)
	)
as
begin
	update Users set Password = @Password where EmailId = @EmailId;
end

--Admin table--
create table Admin(
	AdminId int identity (1,1) primary key,
	FullName varchar(100) not null,
	EmailId varchar(100) not null,
	Password varchar(100) not null,
	MobileNumber bigint not null
);

--select--
select * from Admin

--inserting admin details--
insert into Admin 
values('Admin','admin@gmail.com','123',1234567890);

--admin login--
create procedure spAdminLogin(
	@EmailId varchar(100),
	@Password varchar(100)
	)
as
begin
	select * from Admin where EmailId=@EmailId and Password = @Password;
end
--Book table--
create table Books(
	BookId int identity(1,1) primary key,
	BookName varchar(100) not null,
	AuthorName varchar(100) not null,
	Rating float,
	ReviewerCount int,
	DiscountPrice int not null,
	OriginalPrice int not null,
	BookDetail varchar(max) not null,
	BookImage varchar(max) not null,
	BookQuantity int not null 
)

--select table--
select * from Books

--stored procedure for Books--
--add books--
create procedure spAddbook(
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Rating float,
	@ReviewerCount int,
	@DiscountPrice int,
	@OriginalPrice int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BookQuantity int
	)
as
begin
	insert into Books
	values(@BookName,@AuthorName,@Rating,@ReviewerCount,@DiscountPrice,@OriginalPrice,@BookDetail,@BookImage,@BookQuantity);
end

--Update book--
create procedure spUpdateBook(
	@BookId int,
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Rating float,
	@ReviewerCount int,
	@DiscountPrice int,
	@OriginalPrice int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BookQuantity int
	)
as 
begin
	update Books set 
	BookName= @BookName,
	AuthorName= @AuthorName,
	Rating = @Rating,
	ReviewerCount= @ReviewerCount,
	DiscountPrice = @DiscountPrice,
	OriginalPrice = @OriginalPrice,
	BookDetail= @BookDetail,
	BookImage = @BookImage,
	BookQuantity = @BookQuantity
	where BookId = @BookId;
end
		
--Delete book--
create procedure spDeleteBook(
	@BookId int
	)
as
begin
	delete from Books where BookId=@BookId;
end	

--Get all books--
create procedure spGetAllBooks
as
begin
	select * from Books;
end

--Get book by id--
create procedure spGetBookById(
	@BookId int
	)
as
begin
	select * from Books where BookId=@BookId;
end	

create table Wishlist(
	WishlistId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)

--select table--
select * from Wishlist;

--stored procedure for wishlist--
--add to wishlist--
create procedure spAddToWishlist(
	@BookId int,
	@UserId int
	)
as
begin
	if(not exists(select * from Wishlist where BookId=@BookId and UserId=@UserId))
	begin
		insert into Wishlist
		values(@UserId,@BookId);
	end
end

--remove from wishlist--
create procedure spRemoveFromWishlist(
	@WishlistId int
	)
as
begin
	delete from Wishlist where WishlistId = @WishlistId;
end

--get wishlist item--
create procedure spGetAllWishlistItem(
	@UserId int
	)
as
begin
	select wish.WishlistId,wish.BookId,wish.UserId,
		book.BookName,book.BookImage,book.AuthorName,book.DiscountPrice,book.OriginalPrice		
		from WishList wish inner join Books book
		on wish.BookId = book.BookId
		where wish.UserId = @UserId;
end

create table Cart(
	CartId int identity(1,1) primary key,
	BookInCart int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
)

--select table--
select * from Cart

--stored procedure for cart--
--Add to cart--
create procedure spAddToCart(
	@BookId int,
	@BookInCart int,
	@UserId int
	)
as
begin
	if(not exists(select * from Cart where BookId=@BookId and UserId=@UserId))
	begin
		insert into Cart(BookId,UserId)
		values(@BookId,@UserId);
	end
end

--update cart--
create procedure spUpdateCart(
	@CartId int,
	@BookInCart int
	)
as
begin
	update Cart set BookInCart=@BookInCart where CartId=@CartId;
end

--remove from cart--
create procedure spRemoveFromCart(
	@CartId int
	)
as
begin
	delete from Cart where CartId=@CartId;
end

--get all cart items--
create procedure spGetAllCartItem(
	@UserId int
	)
as
begin
	select cart.CartId,cart.BookId,cart.BookInCart,cart.UserId,
		book.BookName,book.BookImage,book.AuthorName,book.DiscountPrice,book.OriginalPrice from Cart cart inner join Books book 
		on book.BookId=cart.BookId where cart.UserId = @UserId;
end

--Table for Address type--
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
	UserId int not null foreign key (UserId) references Users(UserId)
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
--Table--
create table Feedback(
	FeedbackId int identity (1,1) primary key,
	Rating float not null,
	Comment varchar(max) not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)

--select table--
select * from Feedback

--Stored procedures--
--add feedback--
create procedure spAddFeedback(
	@Rating float,
	@Comment varchar(max),
	@BookId int,
	@UserId int
	)
as
	declare @TotalRating float;
begin
	if(not exists(select * from Feedback where BookId=@BookId and UserId=@UserId))
	begin
		insert into Feedback values(@Rating,@Comment,@BookId,@UserId);

		select @TotalRating = avg(@Rating) from Books where BookId = @BookId;

		Update Books set Rating = @TotalRating, ReviewerCount = (ReviewerCount+1) where BookId=@BookId;
	end
end

--get feedback--
create procedure spGetFeedback(
	@BookId int
	)
as
begin
	select Feedback.FeedbackId,Feedback.Comment,Feedback.BookId,Feedback.Rating,Feedback.UserId,Users.FullName
	from Users
	inner join Feedback
	on Feedback.UserId = Users.UserId where BookId=@BookId;
end

truncate table Feedback
--get all address of user--
create procedure spGetAllAddress(
	@UserId int
	)
as
begin
	select * from Address where UserId=@UserId;
end

--order table--
create table Orders(
	OrderId int identity(1,1) primary key,
	OrderQty int not null,
	TotalPrice float not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
	AddressId int not null FOREIGN KEY REFERENCES Address(AddressId)
	)

--select table--
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
	set @OrderQty = (select BookInCart from Cart where BookId = @BookId); 
	if(exists(select * from Books where BookId = @BookId))
	begin
		Begin try
			Begin Transaction
				if((select BookQuantity from Books where BookId = @BookId)>= @OrderQty)
				begin
					insert into Orders values(@OrderQty,@TotalPrice*@OrderQty,GETDATE(),@UserId,@BookId,@AddressId);
					update Books set BookQuantity = (BookQuantity - @OrderQty) where BookId = @BookId;
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
		Books.BookName, Books.AuthorName, Books.BookImage
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