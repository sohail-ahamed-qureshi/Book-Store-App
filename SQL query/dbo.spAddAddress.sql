CREATE PROCEDURE [dbo].[spAddAddress]
	@userId int,
	@address varchar(max),
	@mobileNumber bigInt,
	@city varchar(25),
	@state varchar(25),
	@typeOf varchar(25)
AS
	Begin try
		Begin Transaction
			if exists (Select * from tbl_Address where userId = @userId)
				begin
				--if the same typeOf address exists of that user update the address
					if exists (Select * from tbl_Address where userId = @userId and typeOf = @typeOf)
						begin 
								update tbl_Address set addresses =@address, mobileNumber = @mobileNumber,
										city =@city, state =@state 
										where userId = @userId and typeOf = @typeOf;
								Commit transaction
						end
				end
				 --if address of that type doesnt exists of particular user then add the address
			else
				begin
						insert into tbl_Address (userId, mobileNumber, addresses, city, state, typeOf)
						values(@userId, @mobileNumber, @address, @city, @state, @typeOf);
						Commit transaction;
				end
	end Try
	Begin catch
		Rollback transaction;
		select 
		error_Number() as ErrorNumber,
		ERROR_STATE() as ErrorState,
		ERROR_SEVERITY() as ErrorSeverity,
		ERROR_PROCEDURE() as ErrorProcedure,
		ERROR_LINE() as ErrorLine,
		ERROR_MESSAGE() as ErrorMessage;
	end catch
RETURN 0