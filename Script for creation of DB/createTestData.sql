USE Gruvo
GO

DECLARE @counter INT = 0;

WHILE @counter < 10
BEGIN
	
	INSERT INTO users(Email, Password, Login, RegDate)
	VALUES(
	CONCAT('test', @counter,'@gmail.com'), 
	'827ccb0eea8a706c4c34a16891f84e7b', /* 12345 */
	CONCAT('Person_', @counter), 
	GETDATE())
	
	INSERT INTO posts(UserId, Message, PostDate)
	VALUES(
	(SELECT UserId FROM users WHERE Login = CONCAT('Person_', @counter)),
	CONCAT('Hello from Person_', @counter, ' !!!'),
	CURRENT_TIMESTAMP)
	
	IF(@counter > 0)
	BEGIN
		INSERT INTO subscriptions(SubscriberId, SubscribedId, SubDate)
		VALUES(
		(SELECT UserId FROM users WHERE Login = CONCAT('Person_', @counter - 1)),
		(SELECT UserId FROM users WHERE Login = CONCAT('Person_', @counter)),
		GETDATE()
		)
	END

	SET @counter = @counter + 1
END
