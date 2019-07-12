CREATE PROCEDURE [dbo].[Customers_Remove]
	@id INT
AS
BEGIN
	DELETE FROM [dbo].[Customers]
	WHERE Id = @id
END
GO
