CREATE PROCEDURE [dbo].[spGetUser]
	@email varchar (255)
AS
	SELECT fullName, email, password, userId from UserAccount where email = @email
RETURN 0
