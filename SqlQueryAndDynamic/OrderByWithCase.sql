USE AdventureWorks2017

GO

SELECT p.ProductID,p.Name,p.Color
FROM Production.Product p
ORDER BY   CASE 
WHEN p.Color = 'silver' THEN 0
WHEN p.Color = 'black' THEN 1
WHEN p.Color = 'yellow' THEN 2
WHEN p.Color IS NULL THEN 3
ELSE 4
END ASC ,p.Color

GO

INSERT INTO Weblag.dbo.Test
(
    ID,
    MaxTest
)
VALUES
(   1,  
    REPLICATE(CAST(N'تست'AS VARCHAR(max)),10000) 
    )

GO
 
SELECT ID, DATALENGTH(MaxTest) [Length]
FROM Weblag.dbo.Test
GO

DECLARE @dec DECIMAL(4,1) = 12.34
DECLARE @num NUMERIC(4,1) =12.34

SELECT @dec [Decimal],@num [Numeric]