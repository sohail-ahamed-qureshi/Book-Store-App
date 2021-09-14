CREATE PROCEDURE [dbo].[spRemoveItemToCart]
	@bookId int,
	@userId int
AS
Begin Try
	Begin transaction
	if exists(Select * from Cart where bookId = @bookId and userId = @userId)
	Begin
		 delete from Cart where bookId = @bookId and userId = @userId;
		 commit transaction
	end
	else
	begin
		rollback transaction
		return 0;
	end
end try
Begin catch
		select 
	error_Number() as ErrorNumber,
	ERROR_STATE() as ErrorState,
	ERROR_SEVERITY() as ErrorSeverity,
	ERROR_PROCEDURE() as ErrorProcedure,
	ERROR_LINE() as ErrorLine,
	ERROR_MESSAGE() as ErrorMessage;
end Catch
Return 0