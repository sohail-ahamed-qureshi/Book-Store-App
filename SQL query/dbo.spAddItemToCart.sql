CREATE PROCEDURE [dbo].[spAddItemToCart]
	@userId int,
	@bookId int,
	@quantity int
AS
	if(@quantity > 0)
	begin
		declare @stock int;
		select @stock = BookStore.quantity from BookStore where BookStore.bookId = @bookId;
		if(@stock >= @quantity)
		Begin
			if exists(Select * from Cart where bookId = @bookId and userId = @userId)
				Begin
					return 0
				end
			else
				Begin
					insert into Cart (bookId, userId, quantity)
					values (@bookId, @userId,@quantity);
				end
			end
		end
RETURN 0