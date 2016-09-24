Select Managers.LastName, COUNT(Orders.Number) AS "# of Orders"
FROM Orders
INNER JOIN Managers ON ( Orders.ManagerId = Managers.ManagerId )
WHERE Orders.CreateDate BETWEEN DATEADD(month,-3,GETDATE()) AND GETDATE()
GROUP BY Managers.LastName
ORDER BY "# of Orders" DESC