CREATE trigger tr_WishList_forInsert
	on WishList
	after insert
	as
	begin
		select BookStore.bookId, UserAccount.fullName, BookStore.bookName,BookStore.author, BookStore.price
		from inserted
		inner join BookStore
		on inserted.bookId =  BookStore.bookId
		inner join UserAccount
		on inserted.userId = UserAccount.userId
	end