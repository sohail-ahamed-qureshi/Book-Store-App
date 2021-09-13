CREATE PROCEDURE [dbo].[spGetAddress]
	@userId int 
AS
	begin try
	if exists (select * from tbl_Address where userId = @userId)
	begin
		select tbl_Address.addressId, UserAccount.userId, isNull(UserAccount.fullName,''), isNull(tbl_Address.addresses,''), isNull(tbl_Address.city,''),
		isNull(tbl_Address.state,''), tbl_Address.mobileNumber, isNull(tbl_Address.typeOf,'')
		from tbl_Address
		inner join UserAccount
		on tbl_Address.userId = UserAccount.userId
		where UserAccount.userId = @userId
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
