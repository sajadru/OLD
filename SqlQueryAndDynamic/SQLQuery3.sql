DROP TABLE IF EXISTS #Condition
SELECT ROW_NUMBER() OVER(ORDER BY st.name) [Number],st.object_id[ID],ss.name[Schema],ST.name[Table],AC.name[Column],SType.name [Type],AC.is_identity,AC.is_computed ,dbo.IS_PK(ST.name,AC.name)[Is_Primary] INTO #Condition FROM sys.tables ST
INNER JOIN sys.all_columns AC ON ac.object_id = st.object_id
INNER JOIN sys.schemas SS ON ss.schema_id = st.schema_id
INNER JOIN sys.types SType ON stype.system_type_id = AC.system_type_id WHERE SType.name != 'sysname'
SELECT * FROM INFORMATION_SCHEMA.COLUMNS
DECLARE @tableName VARCHAR(100)
DECLARE @columnName VARCHAR(100)
DECLARE @pkColumn VARCHAR(100)
DECLARE @pkColumnType VARCHAR(30)
DECLARE @spName VARCHAR(100)
DECLARE @schemaName VARCHAR(100)
DECLARE @sqlCommand VARCHAR(MAX)
DECLARE @spBody VARCHAR(MAX)=NULL
DECLARE @spVariable VARCHAR(MAX)=NULL
DECLARE @columnType VARCHAR(30)

WHILE((SELECT COUNT([Table])FROM #condition)>0)
BEGIN

SELECT @tableName=[Table],@schemaName=[Schema] FROM #Condition

WHILE((SELECT COUNT([Column])FROM #Condition WHERE [Table] = @tableName)>0)
BEGIN 

SELECT @columnName =[Column],@columnType=[Type] FROM #Condition ORDER BY Number
IF EXISTS(SELECT * FROM #Condition WHERE is_computed = 1 AND [Table] = @tableName AND [Column] = @columnName ORDER BY Number)
CONTINUE
IF EXISTS(SELECT Is_Primary FROM #Condition WHERE [Table] = @tableName AND [Column] = @columnName ORDER BY Number)
BEGIN
SET @pkColumn = @columnName
SET @pkColumnType = @columnType
END
ELSE
BEGIN
IF(@spVariable IS NULL)
BEGIN

SET @spVariable ='@'+@columnName+' '+@columnType+' =NULL'
SET @spBody =' SET '+'['+@columnName+']'+' = CASE WHEN '+'@'+@columnName+' IS NULL THEN '+'['+@columnName+']'+' ELSE '+'@'+@columnName+' END '
END
ELSE
BEGIN
SET @spVariable +=' , @'+@columnName+' '+@columnType+' =NULL'
SET @spBody += ' , '+'['+@columnName+']'+' = CASE WHEN '+'@'+@columnName+' IS NULL THEN '+'['+@columnName+']'+' ELSE '+'@'+@columnName+' END '
END
END
DELETE  FROM #condition
WHERE  id = (SELECT TOP 1 ID FROM #Condition ORDER BY [Table],[Column])

END

SET @spName ='SSP_'+@tableName+'_Update'
SET @sqlCommand = 'CREATE OR ALTER PROC '+'['+@spName+']'+'('+'@'+@pkColumn+' '+@pkColumnType+' , '+@spVariable+')'
+' AS 
BEGIN
UPDATE '+'['+@schemaName+']'+'.'+'['+@tableName+'] '+
 @spBody+
' WHERE '+'['+@pkColumn+']'+' = @'+@pkColumn+' END '
EXEC (@sqlCommand)


SET @spVariable = NULL
SET @spBody = NULL
SET @pkColumn = null
END

GO 
