CREATE PROCEDURE [dbo].[Customers_GetAll] AS
BEGIN
	SELECT * FROM Customers c
	LEFT JOIN Orders o ON c.Id = o.CustomerId
END
GO
