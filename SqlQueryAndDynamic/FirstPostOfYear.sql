SELECT * FROM(
SELECT RegisterDateTime,
       YEAR(RegisterDateTime) [year] FROM Post.Post )p
PIVOT ( MIN(p.RegisterDateTime) FOR P.year IN ([2018],[2019],[2020],[2021])) AS pvt
GO

SELECT MIN(RegisterDateTime)[Minimum],
       YEAR(RegisterDateTime) [year] FROM Post.Post
	   GROUP BY YEAR(RegisterDateTime) 