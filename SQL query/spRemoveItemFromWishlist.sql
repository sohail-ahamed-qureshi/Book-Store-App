CREATE PROCEDURE [dbo].[spRemoveItemFromWishlist]
	@userId int,
	@bookId int
AS
	begin try
		begin transaction
		if exists (Select * from WishList where bookId = @bookId and userId =  @userId)
		begin
			delete from WishList where bookId = @bookId and userId =  @userId
			commit transaction
		end
		else
		begin
			rollback transaction
			return 0
		end
	end try
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
