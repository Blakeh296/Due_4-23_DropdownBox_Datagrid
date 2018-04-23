
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

ALTER PROC OrdersByName (
							 @CustomerID int
										)
	AS
		BEGIN
				SELECT SOH.SalesOrderID, SOH.TotalDue, SOH.OrderDate, SOH.ShipDate
				, SP.LastName + ', ' + SP.FirstName [Sales Person], A.City, St.Name 
				FROM Sales.Customer C
				INNER JOIN Sales.SalesOrderHeader SOH
				ON C.CustomerID = SOH.CustomerID
				INNER JOIN Person.Person SP
				ON SOH.SalesPersonID = SP.BusinessEntityID
				INNER JOIN Person.[Address] A
				ON SOH.ShipToAddressID = A.AddressID
				INNER JOIN Person.StateProvince St
				ON A.StateProvinceID = St.StateProvinceID
				WHERE C.CustomerID = @CustomerID --AND PO IS NOT NULL? from select * from Sales.SalesOrderHeader
		END
		 GO

ALTER PROC CustomerList
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