CREATE PROCEDURE [dbo].[spGetAllBooks]
AS
BEGIN
	SELECT bookId, isnull(bookName,''), isnull(author,''), isnull(description,''), price, rating, isnull(image,''), quantity from BookStore 
END
RETURN 0
