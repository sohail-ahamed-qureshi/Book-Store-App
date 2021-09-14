CREATE PROCEDURE [dbo].[spMyOrders]
	@userId int
AS
Begin Try
	SELECT tbl_Orders.orderId, isNull(UserAccount.fullName,''),isNull(BookStore.bookName,''),tbl_Orders.quantity, tbl_Orders.orderDate,
			tbl_Orders.price,tbl_Orders.totalPrice
	from tbl_Orders
	inner join UserAccount
	on tbl_Orders.userId = UserAccount.userId
	inner join BookStore
	on tbl_Orders.BookId = BookStore.bookId
	where tbl_Orders.userId = @userId
End Try
Begin Catch
select 
	error_Number() as ErrorNumber,
	ERROR_STATE() as ErrorState,
	ERROR_SEVERITY() as ErrorSeverity,
	ERROR_PROCEDURE() as ErrorProcedure,
	ERROR_LINE() as ErrorLine,
	ERROR_MESSAGE() as ErrorMessage;
End Catch
RETURN 0
