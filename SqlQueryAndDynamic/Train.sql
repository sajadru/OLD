USE Weblag

SELECT  p.FirstName+' '+p.LastName [Full Name],ISNULL(SUM(a.Point),0) [Total Point]
FROM dbo.Person P
LEFT OUTER JOIN dbo.Activity_Person AP ON AP.PersonID = P.ID
LEFT OUTER JOIN dbo.Activity A ON A.ID = AP.ActivityID
GROUP BY p.ID,p.FirstName+' '+p.LastName
UNION
SELECT  N'جمع کل',SUM(A.Point)
FROM dbo.Activity_Person AP
INNER JOIN dbo.Activity A ON A.ID = AP.ActivityID
ORDER BY [Total Point] ASC
GO
--درست کردن Temporary Table 
--فقط جهت تست 
SELECT *
INTO #show
FROM sys.databases 
UPDATE #show
SET name = N'محتوا لازم را وارد میکنیم'
SELECT *
FROM sys.databases SD
WHERE SD.owner_sid <> 0x01 OR LEN(SD.owner_sid) > 4
UNION
SELECT  * FROM #show
WHERE database_id = 1
ORDER BY SD.name DESC
GO
SELECT SD.name
FROM sys.databases SD
WHERE SD.owner_sid <> 0x01 OR LEN(SD.owner_sid) > 4
UNION
SELECT N'دیتا بیس های ما'
ORDER BY 1 DESC
GO 
