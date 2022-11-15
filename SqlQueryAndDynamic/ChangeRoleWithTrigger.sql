
CREATE TRIGGER TR_UpdateRole ON dbo.Activity_Person
AFTER INSERT
AS BEGIN 
IF (2 =(SELECT P.RoleCode FROM dbo.Person P
        WHERE p.ID =(SELECT I.PersonID FROM Inserted I)))
		 AND  
		 (10000<(SELECT SUM(A.Point)FROM dbo.Activity_Person AP 
          INNER JOIN dbo.Activity A ON A.ID = AP.ActivityID
		  WHERE AP.ID =(SELECT I.id FROM Inserted I)))
BEGIN
UPDATE dbo.Person 
SET RoleCode =4
WHERE ID = (SELECT I.PersonID FROM Inserted I)
END
END

GO

