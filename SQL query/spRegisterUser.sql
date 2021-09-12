CREATE PROCEDURE [dbo].[spRegisterUser]
	@fullName varchar(255),
	@email varchar(255),
	@password varchar(25),
	@mobileNumber bigInt,
	@createdDate date,
	@updatedDate date,
	@role varchar(25)
AS
Declare	@id int =0;
Select @id = userId from UserAccount where email = @email;
if(@id = 0)
Begin
	Insert into UserAccount(fullName, email, password, mobileNumber, createdDate, updatedDate)
	values (@fullName, @email, @password, @mobileNumber, @createdDate, @updatedDate);
	declare @roleId int = 0;
  select @roleId = roleId from Roles where roleName = @role;
  declare @userId int = 0;
  select @userId = userId from UserAccount where email = @email;
  insert into UserRoles (userId, AdminId, roleId)
  values(@userId, null, @roleId)
End
RETURN 0