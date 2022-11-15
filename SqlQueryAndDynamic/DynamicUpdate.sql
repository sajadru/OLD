DROP TABLE IF EXISTS #Condition
SELECT TABLE_SCHEMA, TABLE_NAME,COLUMN_NAME,DATA_TYPE,dbo.IS_Computed(TABLE_NAME,COLUMN_NAME) [Is_computed],dbo.IS_PK(TABLE_NAME,COLUMN_NAME)[Is_Pk],dbo.IS_Identity(TABLE_NAME,COLUMN_NAME)[Is_identity],ROW_NUMBER()OVER( ORDER BY TABLE_NAME, COLUMN_NAME) [ID] INTO #Condition FROM INFORMATION_SCHEMA.COLUMNS

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
DECLARE @isHandle BIT =1
WHILE((SELECT COUNT(TABLE_NAME)FROM #condition)>0)
BEGIN

SELECT @tableName=TABLE_NAME,@schemaName=TABLE_SCHEMA FROM #Condition WHERE  id = (SELECT MIN(ID) ID FROM #Condition)


WHILE((SELECT COUNT(COLUMN_NAME)FROM #Condition WHERE TABLE_NAME = @tableName)>0)
BEGIN 

SELECT @columnName =COLUMN_NAME,@columnType=DATA_TYPE FROM #Condition WHERE  id = (SELECT MIN(ID) ID FROM #Condition)

IF EXISTS(SELECT * FROM #Condition WHERE is_computed = 1 AND TABLE_NAME = @tableName AND COLUMN_NAME = @columnName)
SET @isHandle=0
IF EXISTS(SELECT * FROM #Condition WHERE Is_identity = 1 AND TABLE_NAME = @tableName AND COLUMN_NAME = @columnName)
SET @isHandle=0
IF EXISTS(SELECT * FROM #Condition WHERE Is_Pk = 1 AND TABLE_NAME = @tableName AND COLUMN_NAME = @columnName)
BEGIN
SET @pkColumn = @columnName
SET @pkColumnType = @columnType
SET @isHandle = 0
END
IF (@isHandle =1)
BEGIN
IF(@spVariable IS NULL)
BEGIN
SET @spVariable ='@'+@columnName+' '+@columnType
SET @spBody =' SET '+'['+@columnName+']'+' = CASE WHEN '+'@'+@columnName+' IS NULL THEN '+'['+@columnName+']'+' ELSE '+'@'+@columnName+' END '
END
ELSE
BEGIN
SET @spVariable +=' , @'+@columnName+' '+@columnType+' =NULL'
SET @spBody +=' , ['+@columnName+']'+' = CASE WHEN '+'@'+@columnName+' IS NULL THEN '+'['+@columnName+']'+' ELSE '+'@'+@columnName+' END '
END
END

DELETE  FROM #condition
WHERE  id = (SELECT MIN(ID) ID FROM #Condition)
SET @isHandle =1
END
SET @spVariable +=' , @'+@pkColumn+' '+@pkColumnType
SET @spName ='SSP_'+@tableName+'_Update';
SET @sqlCommand = 'CREATE OR ALTER PROC '+'['+@spName+']'+'('+@spVariable+')'+' AS BEGIN UPDATE '+'['+@schemaName+']'+'.'+'['+@tableName+'] '+ @spBody+' WHERE '+'['+@pkColumn+']'+' = @'+@pkColumn+' END ';
EXEC (@sqlCommand)


SET @spVariable = NULL
SET @spBody = NULL
SET @pkColumn = null
END

GO 
