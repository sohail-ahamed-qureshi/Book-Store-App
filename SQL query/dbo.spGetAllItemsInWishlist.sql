CREATE PROCEDURE [dbo].[spGetAllItemsInWishlist]
	@userId int
AS
Begin Try
	select BookStore.bookId, isnull(UserAccount.fullName,''),isnull(BookStore.bookName,''),isnull(BookStore.author,''),
	BookStore.price 
	from WishList
	inner join UserAccount 
	on WishList.userId = UserAccount.userId
	inner join BookStore
	on WishList.bookId = BookStore.bookId
	where UserAccount.userId = @userId
End try
begin catch
	select 
	error_Number() as ErrorNumber,
	ERROR_STATE() as ErrorState,
	ERROR_SEVERITY() as ErrorSeverity,
	ERROR_PROCEDURE() as ErrorProcedure,
	ERROR_LINE() as ErrorLine,
	ERROR_MESSAGE() as ErrorMessage;
end catch
RETURN 0