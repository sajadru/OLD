use adventureworks2017
select * from production.product
--
select productid as 'chaghal',* from production.product
order by chaghal desc
--
select distinct color from production.product
--
select top 10 name from production.product
--
select top 10 with Ties name from production.product
order by name asc
--
select name , ListPrice from Production.product
order by name asc, listprice asc
offset 10 rows fetch next 5 rows only
-- < > <= >= = != or and in not in is null is not null like between
select * from production.product
where ListPrice > 100
select * from production.product
where name like 'b%'
--
select count(*) from production.product
select sum(listprice) from production.product
select min(listprice) from production.product
select avg(listprice) from production.product
--
select sum(listprice) as 'sum listprice',name,productid from production.product
group by name,productid
--
select sum(listprice) as 'dashagh' , [name] from production.product
group by name 
having name like '%b%'
--
select row_number() over(partition by color order by listprice),* from production.product
select rank() over(order by listprice),* from production.product
select dense_rank() over(order by listprice), * from production.product
select NTILE(12) over(order by name), * from production.product
--
select name,color,(
select name from production.ProductSubcategory psc
where psc.ProductSubcategoryID = p.ProductSubcategoryID) as 'subcategory name' 
from Production.product p
--
select * from(
select rank() over(order by listprice) as 'ra',name ,color  from Production.Product) s
where ra = 1