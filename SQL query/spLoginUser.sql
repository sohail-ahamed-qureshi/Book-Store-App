CREATE PROCEDURE [dbo].[spLoginUser]
	@email varchar (255)
AS
  select fullname, mobileNumber, password, UserRoles.roleId, Roles.roleName
  from UserRoles
  inner join UserAccount
  on UserRoles.userId = UserAccount.userId
  inner join Roles
  on UserRoles.roleId = Roles.roleId
  where UserAccount.email = @email 
RETURN 0