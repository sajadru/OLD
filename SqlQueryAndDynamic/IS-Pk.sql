CREATE OR ALTER FUNCTION IS_PK (@tabel VARCHAR(50),@column VARCHAR(50))
RETURNS BIT
AS
BEGIN
DECLARE @result BIT 
IF EXISTS(
SELECT ST.name[table], ac.name FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS IST
INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ISC ON ISC.CONSTRAINT_NAME=IST.CONSTRAINT_NAME
 INNER JOIN sys.tables ST ON st.name = IST.TABLE_NAME 
 INNER JOIN sys.all_columns AC ON ac.name = ISC.COLUMN_NAME
 WHERE st.name =@tabel AND ac.name = @column AND IST.CONSTRAINT_TYPE ='PRIMARY KEY')
 SET @result = 1
 ELSE
 SET @result =0
RETURN @result
END
GO


CREATE OR ALTER FUNCTION IS_Computed (@tabel VARCHAR(50),@column VARCHAR(50))
RETURNS BIT
AS
BEGIN
DECLARE @result BIT 
IF EXISTS(
SELECT ST.name[table], ac.name FROM sys.tables ST
 INNER JOIN sys.all_columns AC ON AC.object_id = ST.object_id
 WHERE st.name =@tabel AND ac.name = @column AND AC.is_computed =1)
 SET @result = 1
 ELSE
 SET @result =0
RETURN @result
END
GO
CREATE OR ALTER FUNCTION IS_FK (@tabel VARCHAR(50),@column VARCHAR(50))
RETURNS BIT
AS
BEGIN
DECLARE @result BIT 
IF EXISTS(
SELECT ST.name[table], ac.name FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS IST
INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ISC ON ISC.CONSTRAINT_NAME=IST.CONSTRAINT_NAME
 INNER JOIN sys.tables ST ON st.name = IST.TABLE_NAME 
 INNER JOIN sys.all_columns AC ON ac.name = ISC.COLUMN_NAME
 WHERE st.name =@tabel AND ac.name = @column AND IST.CONSTRAINT_TYPE !='PRIMARY KEY')
 SET @result = 1
 ELSE
 SET @result =0
RETURN @result
END
GO


CREATE OR ALTER FUNCTION IS_Identity (@tabel VARCHAR(50),@column VARCHAR(50))
RETURNS BIT
AS
BEGIN
DECLARE @result BIT 
IF EXISTS(
SELECT ST.name[table], ac.name FROM sys.tables ST
 INNER JOIN sys.all_columns AC ON AC.object_id = ST.object_id
 WHERE st.name =@tabel AND ac.name = @column AND AC.is_identity =1)
 SET @result = 1
 ELSE
 SET @result =0
RETURN @result
END
GO