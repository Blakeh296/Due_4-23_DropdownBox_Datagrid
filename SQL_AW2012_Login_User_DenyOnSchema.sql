
	-- NEED OTHER PROCEDURE

Use AdventureWorks2012;

-- CREATES THE LOGIN
CREATE LOGIN AdvWorks2012
-- WITH PASSWORD
	WITH PASSWORD = 'Password123'

	GO
-- CREATES USER
CREATE USER AdvWorks2012 FOR LOGIN AdvWorks2012;
GO

										-- DENY SELECT ON SCHEMA --
DENY SELECT ON SCHEMA ::
	HumanResources
	TO AdvWorks2012

										-- DENY INSERT ON SCHEMA --
DENY INSERT ON SCHEMA ::
	HumanResources
	TO AdvWorks2012

										-- DENY ALTER ON SCHEMA --
DENY ALTER ON SCHEMA ::
	HumanResources
	TO AdvWorks2012

										-- DENY UPDATE ON SCHEMA --
DENY UPDATE ON SCHEMA ::
	HumanResources
	TO AdvWorks2012

										-- DENY INSERT ON SCHEMA --
DENY INSERT ON SCHEMA ::
	Person
	TO AdvWorks2012

										-- DENY ALTER ON SCHEMA --
DENY ALTER ON SCHEMA ::
	Person
	TO AdvWorks2012

										-- DENY UPDATE ON SCHEMA --
DENY UPDATE ON SCHEMA ::
	Person
	TO AdvWorks2012
	GO

										-- CREATE PROC --

Create procedure sp_CustomerSales
(
@CustID int
)
as
Select	soh.SalesOrderID, soh.OrderDate, soh.ShipDate,CONCAT(p.FirstName,' ', p.LastName)[Sales Person],a.City,psp.Name[State], soh.TotalDue
From	sales.SalesOrderHeader soh
Inner join sales.Customer c
on		c.CustomerID = soh.CustomerID
inner join sales.SalesPerson sp
on		sp.TerritoryID = soh.TerritoryID
inner join person.Person p
on		p.BusinessEntityID = sp.BusinessEntityID
inner join person.Address a
on		a.AddressID = soh.ShipToAddressID
inner join person.StateProvince psp
on		psp.StateProvinceID = a.StateProvinceID
Where	c.CustomerID = @CustID;
go 

CREATE PROC CustomerList
AS
	BEGIN
			SELECT C.CustomerID, P.LastName + ', ' + P.FirstName [Lname_Fname] FROM Sales.Customer C
			INNER JOIN Person.Person P
			ON C.CustomerID = P.BusinessEntityID

	END

		select * from  Sales.SalesOrderHeader

	
		EXEC OrdersByName @CustomerID = 30030

			--drop login AdvWorks2012

			--drop user AdvWorks2012

			--drop proc OrdersByName