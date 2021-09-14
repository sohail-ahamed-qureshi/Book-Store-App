CREATE PROCEDURE [dbo].[spIncreaseQuantityInCart]
	@bookId int,
	@userId int,
	@quantity int
AS
	Begin Try
	declare @stock int =0;
	--checking if item exists in cart
	if exists(Select * from Cart where bookId = @bookId and userId = @userId)
	begin 
	--increasing/decrease item quantity in cart
		
		update Cart 
		set quantity = (quantity + @quantity)
		where bookId = @bookId
		and userId = @userId; 
		Print 'Cart has been updated'

		--check if quantity is below 1
		Select @stock = quantity from Cart where bookId = @bookId and userId = @userId
		if (@stock <= 0)
		Begin
			update Cart 
		set quantity = 1
		where bookId = @bookId
		and userId = @userId; 
		end

	-- return book details
		select Cart.bookId, isNull(UserAccount.fullName,''), isNull(bookName,''), isNull(Author,''),isNull( Description,''), price, Cart.quantity
		from Cart
		join BookStore
		on Cart.bookId = BookStore.bookId
		join UserAccount
		on Cart.userId = UserAccount.userId
		where Cart.bookId = @bookId and Cart.userId = @userId
	end
	end Try
	Begin Catch
	select 
	error_Number() as ErrorNumber,
	ERROR_STATE() as ErrorState,
	ERROR_SEVERITY() as ErrorSeverity,
	ERROR_PROCEDURE() as ErrorProcedure,
	ERROR_LINE() as ErrorLine,
	ERROR_MESSAGE() as ErrorMessage;
	end catch
RETURN 0