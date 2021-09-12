CREATE PROCEDURE [dbo].[spAddItemtoWishlist]
	@userId int,
	@bookId int
AS
	begin Try
			Begin Transaction
			if exists (Select * from WishList where bookId = @bookId  and  userId =  @userId)
				begin
					Rollback transaction
					return 0
				end
				else
					begin
						declare @createdDate smalldatetime = GetDate();
						declare @modifiedDate smalldatetime = GetDate();

						Insert into WishList (bookId, userId, createdDate, modifiedDate)
						values(@bookId, @userId, @createdDate, @modifiedDate)
						Commit transaction
					end	
		end try
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
