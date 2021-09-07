CREATE PROCEDURE [dbo].[spRegisterUser]
	@fullName varchar(255),
	@email varchar(255),
	@password varchar(25),
	@mobileNumber bigInt,
	@createdDate date,
	@updatedDate date
AS
Declare	@user int =0;
Select @user = userId from UserAccount where email = @email;
if(@user = 0)
Begin
	Insert into UserAccount(fullName, email, password, mobileNumber, createdDate, updatedDate)
	values (@fullName, @email, @password, @mobileNumber, @createdDate, @updatedDate);
End
RETURN 0