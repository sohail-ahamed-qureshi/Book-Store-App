CREATE PROCEDURE [dbo].[spPlaceOrder]
	@cartId int,
	@addressId int
AS
begin try
	--check for details present in cart table get details of user and book
		Declare @userId int =0;
		Declare @bookId int =0;
		Declare @quantity int =0;
		Declare @price int= 0;
		Declare @Totalprice money = 0;
		Declare @stock int = 0;
	if exists (Select * from Cart where cartId = @cartId)
		begin
			begin transaction
				--check for address in address table and get details
			if exists (Select * from tbl_Address where addressId = @addressId)
				begin
					
				--Get Details of User and Book 
				select @userId = userId , @bookId = bookId , @quantity = quantity from Cart
				where cartId = @cartId
			
				Select @Totalprice = (BookStore.price * @quantity), @price = BookStore.price from BookStore 
				where bookId = @bookId
				
				--check for stock in book store available for placed order if available reduce the stock
				Select @stock = BookStore.quantity from BookStore where bookId = @bookId
				if(@stock >= @quantity )
					begin
						Update BookStore 
						set BookStore.quantity = (BookStore.quantity - @quantity)
						where bookId = @bookId;

						--delete the existing cart details
						delete from Cart 
						where cartId = @cartId;

						--insert in order placed table
						DECLARE @orderDate DATETIME = GETDATE()
						insert into tbl_Orders (userId, BookId, addressId, quantity,price,orderDate, totalPrice)
						values(@userId, @bookId, @addressId, @quantity,@price,@orderDate, @Totalprice);
					end
				end
			Commit transaction;
		end
	else
		Begin
			Return 0;
		end	
end try
begin catch
	rollback transaction;	
	select 
	error_Number() as ErrorNumber,
	ERROR_STATE() as ErrorState,
	ERROR_SEVERITY() as ErrorSeverity,
	ERROR_PROCEDURE() as ErrorProcedure,
	ERROR_LINE() as ErrorLine,
	ERROR_MESSAGE() as ErrorMessage;
end catch
RETURN 0
