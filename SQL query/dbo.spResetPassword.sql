CREATE PROCEDURE [dbo].[spResetPassword]
	@email varchar(255),
	@newPassword varchar(25),
	@updatedDate date
AS
Begin Try
	Begin Transaction
	update UserAccount 
	set password = @newPassword, updatedDate = @updatedDate
	where email = @email
	Commit 
End Try
Begin Catch
	Rollback transaction
End Catch
RETURN 0