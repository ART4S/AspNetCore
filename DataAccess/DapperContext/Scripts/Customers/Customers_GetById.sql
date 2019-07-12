CREATE PROCEDURE [dbo].[Customers_GetById]
	@id INT
AS
BEGIN
	SELECT * FROM Customers
	WHERE Id = @id
END
GO
