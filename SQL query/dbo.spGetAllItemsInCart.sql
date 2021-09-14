CREATE PROCEDURE [dbo].[spGetAllItemsInCart]
	@userId int
AS
	
	select isnull(UserAccount.fullName,''),isnull(BookStore.bookName,''),isnull(BookStore.author,''),
	isnull(BookStore.description,''),BookStore.price * Cart.quantity, Cart.quantity
	from Cart
	join UserAccount 
	on UserAccount.userId = Cart.userId
	join BookStore
	on BookStore.bookId = Cart.bookId
	where UserAccount.userId = @userId
RETURN 0