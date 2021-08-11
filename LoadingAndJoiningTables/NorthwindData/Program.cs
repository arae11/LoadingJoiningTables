using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NorthwindData
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                /* //LAZY LOADING
                var ordersQuery =
                    from order in db.Orders.Include(o => o.Customer)
                    where order.Freight > 750
                    select order;
                foreach(var order in ordersQuery)
                {
                    if (order.Customer != null)
                    {
                        Console.WriteLine($"{order.Customer.CompanyName} of {order.Customer.City} paid {order.Freight} " +
                            $"for shipping");
                    }
                }
                */

                /*
                var ordersQuery2 = db.Orders
                    .Include(o => o.Customer)
                    .Include(c => c.OrderDetails)
                    .Where(o => o.Freight > 750)
                    .Select(o => o);
                foreach(var o in ordersQuery2)
                {
                    Console.WriteLine($"Order {o.OrderId} was made by {o.Customer.CompanyName}");
                    foreach(var od in o.OrderDetails)
                    {
                        Console.WriteLine($"\t ProductId: {od.ProductId}");
                    }
                }
                */

                /* //EAGER LOADING
                var ordersQuery3 = db.Orders
                    .Include(o => o.Customer)
                    .Include(c => c.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .Where(o => o.Freight > 750);
                foreach (var o in ordersQuery3)
                {
                    Console.WriteLine($"Order {o.OrderId} was made by {o.Customer.CompanyName}");
                    foreach (var od in o.OrderDetails)
                    {
                        Console.WriteLine(
                            $"\t ProductId: {od.ProductId}" +
                            $" - Product: {od.Product.ProductName}" +
                            $" - Quantity: {od.Quantity}");
                    }
                }
                */

                /* //RELATIONAL APPROACH
                var orderQueryUsingJoin =
                    from order in db.Orders
                    where order.Freight > 750
                    join customer in db.Customers on order.CustomerId equals customer.CustomerId
                    select new { CustomerContactName = customer.ContactName, City = customer.City, Freight = order.Freight }; //an object of anonymous type
                foreach (var result in orderQueryUsingJoin)
                {
                    Console.WriteLine($"{result.CustomerContactName} of {result.City} paid {result.Freight} for shipping");
                } 
                */

                /*
                var orderCustomerBerlinParisQuery =
                    from o in db.Orders
                    join c in db.Customers on o.CustomerId equals c.CustomerId
                    where c.City == "Berlin" || c.City == "Paris"
                    select new { o.OrderId, c.CompanyName };
                foreach(var item in orderCustomerBerlinParisQuery)
                {
                    Console.WriteLine($"Orders with ID {item.OrderId} was ordered by {item.CompanyName}");
                }
                */

                //////////////////////////////////////////////////////////////////
                /* LAB EXERCISES*/
                //////////////////////////////////////////////////////////////////

                // 1.1 Write a query that lists all Customers in either Paris or London. Include Customer ID, Company Name and all address fields. 
                /*var query =
                    from c in db.Customers
                    where c.City == "Paris" || c.City == "London"
                    select new { c.CustomerId, c.CompanyName, c.Address };
                foreach (var customer in queryOne)
                {
                    Console.WriteLine($"Company: {customer.CompanyName} with ID: {customer.CustomerId} is located at: {customer.Address}, {customer.City}, {customer.Country}");
                }

                var method = db.Customers
                    .Where(c => c.City == "Paris" || c.City == "London")
                    .Select(c => c);
                foreach (var customer in queryTwo)
                {
                    Console.WriteLine($"Company: {customer.CompanyName} with ID: {customer.CustomerId} is located at: {customer.Address}, {customer.City}, {customer.Country}");
                }*/


                //1.2 List all products stored in bottles.
                /*var query =
                    from p in db.Products
                    where p.QuantityPerUnit.Contains("bottle")
                    select new { p.ProductName };
                 foreach (var product in query)
                 {
                     Console.WriteLine($"{product.ProductName}");
                 }

                 db.Products.Where(p => p.QuantityPerUnit.Contains("bottle")).Select(p => p);
                 foreach (var product in method)
                 {
                     Console.WriteLine($"{product.ProductName}");
                 }*/


                // 1.3 Repeat question above, but add in the Supplier Name and Country. 
                /*var query =
                    from p in db.Products
                    join s in db.Suppliers on p.SupplierId equals s.SupplierId
                    where p.QuantityPerUnit.Contains("bottle")
                    select new { p.ProductName, s.CompanyName, s.SupplierId };
                foreach (var product in query)
                {
                    Console.WriteLine($"Product Name: {p.ProductName} \n ID: {p.Supplier.SupplierId} - Supplier Name: {p.Supplier.CompanyName}");
                }
                
                db.Products
                        .Include(p => p.Supplier)
                        .Where(p => p.QuantityPerUnit.Contains("bottle"))
                        .Select(p => p);
                      foreach (var p in method)
                        {
                            Console.WriteLine($"Product Name: {p.ProductName} \n ID: {p.Supplier.SupplierId} - Supplier Name: {p.Supplier.CompanyName}");
                        }
                        */

                // 1.4 Write an SQL Statement that shows how many products there are in each category.
                //     Include Category Name in result set and list the highest number first. 
                /*var query =
                       from p in db.Products
                       join c in db.Categories on p.CategoryId equals c.CategoryId
                       group p by c.CategoryName into newGroup
                       select new { Category = newGroup.Key, NumOfProd = newGroup.Count() };
               foreach (var result in query)
               {
                   Console.WriteLine($"{result.Category} - {result.NumOfProd}");
               }*/

                /*
                db.Products
                    .Include(p => p.Category)
                    .GroupBy(p => p.CategoryId)
                    .Select(categoryGroup => new { category = categoryGroup.Key, count = categoryGroup
                    .Count() }).ToList()
                    .ForEach(cg => Console.WriteLine(cg));
                */


                // 1.5 List all UK employees using concatenation to join their title of courtesy, first name and last name together.
                //     Also include their city of residence. 

                /*var query =
                    from e in db.Employees
                    select new { e.TitleOfCourtesy, e.FirstName, e.LastName, e.City };
                foreach(var e in query)
                {
                    Console.WriteLine($"{e.TitleOfCourtesy}{e.FirstName} {e.LastName}, {e.City}");
                }*/

                /*
                var query2 = db.Employees
                    .Select(e => e);
                foreach(var e in query2)
                {
                    Console.WriteLine($"{e.TitleOfCourtesy}{e.FirstName} {e.LastName}, {e.City}");
                }*/

                // 1.6 List Sales Totals for all Sales Regions (via the Territories table using 4 joins) with a Sales Total greater than 1,000,000.
                //     Use rounding or FORMAT to present the numbers.




                // 1.7 Count how many Orders have a Freight amount greater than 100.00 and either USA or UK as Ship Country. 
                var query =
                    from o in db.Orders
                    where o.Freight > 100 && (o.ShipCountry == "USA" || o.ShipCountry == "UK")
                    group o by o.OrderId into newGroup
                    select new { Orders = newGroup.Key, NumOfOrders = newGroup.Count() };
                foreach (var o in query)
                {
                    Console.WriteLine($"{o.NumOfOrders}");
                }

                var query2 = db.Orders
                    .Where(o => o.Freight > 100 && (o.ShipCountry == "USA" || o.ShipCountry == "UK"))
                    .Select(orderGroup => new { Orders = orderGroup.Key, count = orderGroup
                    .Count()})
                foreach (var o in query2)
                {
                    Console.WriteLine($"{o.OrderId}");
                }



                // 1.8 Write an SQL Statement to identify the Order Number of the Order with the highest amount of discount applied to that order. 




                // 3.1 List all Employees from the Employees table and who they report to. No Excel required. 




                // 3.2 List all Suppliers with total sales over $10,000 in the Order Details table.
                //     Include the Company Name from the Suppliers Table and present as a bar chart as below: 




                // 3.3 List the Top 10 Customers YTD for the latest year in the Orders file. Based on total value of orders shipped.




            }
        }
    }
}
