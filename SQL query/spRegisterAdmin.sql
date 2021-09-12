CREATE PROCEDURE [dbo].[spRegisterAdmin]
	@fullname varchar(255),
	@email varchar(255),
	@password varchar(25),
	@mobileNumber bigInt,
	@createdDate date,
	@updatedDate date,
	@role varchar(25)
AS
declare @id int =0;
select @id = AdminId from AdminAccount where email = @email;
if(@id = 0)
begin
insert into AdminAccount (fullName, email, password, mobileNumber, createdDate, updatedDate) 
  values( @fullname, @email, @password, @mobileNumber,@createdDate ,@updatedDate);
  declare @roleId int = 0;
  select @roleId = roleId from Roles where roleName = @role;
  declare @adminId int = 0;
  select @adminId = AdminId from AdminAccount where email = @email;
  insert into UserRoles (userId, AdminId, roleId)
  values(null, @adminId, @roleId)
end
RETURN 0