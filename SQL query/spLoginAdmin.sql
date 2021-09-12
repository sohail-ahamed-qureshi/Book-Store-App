CREATE PROCEDURE [dbo].[spLoginAdmin]
	@email varchar (255)
AS
  select fullname, email, password, AdminAccount.AdminId, Roles.roleName
  from UserRoles
  inner join AdminAccount
  on UserRoles.AdminId = AdminAccount.AdminId
  inner join Roles
  on UserRoles.roleId = Roles.roleId
  where AdminAccount.email = @email 
RETURN 0
