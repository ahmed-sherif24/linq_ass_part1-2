using LINQ.DataSources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;



// Get all products from the "Seafood" category. Print each 
//product's name and price.
//
var Products = Source.ProductList;
var seafoodProduct = Products
    .Where(p => string.Equals(p.Category, "seafood", StringComparison.OrdinalIgnoreCase))
    .Select(p => new
    {
        p.ProductName,
        p.UnitPrice
    });
foreach (var product in seafoodProduct)
{
    Console.WriteLine(product);
}
///////////////////////////////////////////////////////////////////
///
//  Get a list of only the product names from ProductList. Print each name.

var productName = Products
    .Select(p => p.ProductName);
foreach (var product in productName)
{
    Console.WriteLine(product);
}
////////////////////////////////////////////////////////
///
//  Sort all products by UnitPrice (ascending). Print each product's name and price.
//  

var ProdctOrdered = Products
    .OrderBy(p => p.UnitPrice);

/////////////////////////////////////////////////////
///
// Get all products where UnitPrice is between 10 and 30

var productsBetween = Products
    .Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30);
//////////////////////////////////////////////////////////////////
///
// Get all products that are in stock (UnitsInStock > 0) and belong to the "Condiments" category. 

var Q5 = Products
    .Where(p => p.UnitsInStock > 0 && string.Equals(p.Category, "Condiments", StringComparison.OrdinalIgnoreCase));
foreach (var product in Q5)
{

    Console.WriteLine(product);
}
// 6. Create a new anonymous type with three properties: 
//● Name → the product name 
//● Price → the unit price 
//● StockStatus → a string: "Available" if UnitsInStock > 0, 
//otherwise "Out of Stock" 
//● Print the result. 

var Q6 = Products
    .Select (p => new
    {
        Name = p.ProductName,
        Price = p.UnitPrice,
        StockStatus = p.UnitsInStock >0? "Available" : "Out of stock"
    });
foreach (var product in Q6)
    { Console.WriteLine(product); }
//////////////////////////////////////////////////////////////////////
///

//7.Print each product's name along with its position (1-based) 
//in the list. Expected format: 1.Chai, 2.Chang, etc.

var Q7 = Products
    .Select((p, i) => $"{i + 1}. {p.ProductName}");
foreach (var product in Q7)
    { Console.WriteLine(product); }
///////////////////////////////////////////////////////

//Sort ProductList by Category ascending, then within each 
//category, sort by UnitPrice descending.

var Q8 = Products
    .OrderBy (p => p.Category)
    .ThenByDescending(p=>p.UnitPrice);
/////////////////////////////////////////////////////////////


//Get all products from the "Beverages" category, sorted by 
//UnitsInStock descending. Print name and stock.

var Q9 = Products
    .Where(p => string.Equals(p.Category, "Beverages", StringComparison.OrdinalIgnoreCase))
    .Select(p => new
    {
        p.ProductName,
        p.UnitsInStock
    })
    .OrderByDescending(p => p.UnitsInStock);

foreach (var product in Q9)
    { Console.WriteLine(product); }
///////////////////////////////////////////////////////////////////////


//10.Using QUERY SYNTAX with a compound from clause, list 
//all orders placed in 1997 or later showing CustomerID and 
//OrderDate. 
var Customers = Source.CustomerList;

var Q10 =
    from customer in Customers
    from order in customer.Orders
    where order.OrderDate.Year >= 1997
    select new
    {
        customer.CustomerID,
        order.OrderDate
    };

foreach (var item in Q10)
{
    Console.WriteLine(item);
        
}

//////////////////////////////////////////////////////////////////
///

//12.Sort first by-word length and then by a 
//case-insensitive sort of the words in an array. 
String [] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry"};

var Q12 = Arr
    .OrderBy(p => p.Length)
    .ThenBy(p => p, StringComparer.OrdinalIgnoreCase);

///////////////////////////////////////////////////////////////
/////
//13.Create a list of all digits in the array whose second 
//letter is 'i' that is reversed from the order in the 
//original array. 

var Q13 = Arr
    .Where(p=> p .Length>1 && p[1]=='i')
    .Reverse()
    .ToList();



//////////////////////////
///
var TopExpen = Products
    .OrderByDescending(p => p.UnitPrice)
    .Take(3);
foreach (var item in TopExpen)
    { Console.WriteLine(item); }

var page = Products
    .Skip(5)
    .Take(5);

var TakeWhile = Products
    .TakeWhile(p => p.UnitPrice > 25);

var AllAvailable = Products
    .Where(p => string.Equals(p.Category, "seafood", StringComparison.OrdinalIgnoreCase))
    .All(p => p.UnitsInStock > 0);

int[] ids = { 3, 9, 13, 18 };
var IdCheck = ids
    .Any(id => id == 9);
Console.WriteLine(IdCheck);

var GroupByCat = Products
    .GroupBy(p => p.Category)
    ;
foreach (var item in GroupByCat)
{
    Console.WriteLine($"category : {item.Key} , count : {item.Count()}");
}

var x = Products
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        g.Key,
        ProductName = g.Select(p => p.ProductName)

    });

foreach (var item in x)
{
    Console.WriteLine(item.Key);
    foreach (var item2 in item.ProductName)
    { Console.WriteLine(item2); }
}

var CatMore3 = Products
    .GroupBy(p => p.Category)
    .Where(p=>p.Count() > 6);
foreach (var item in CatMore3)
    { Console.WriteLine(item.Key); }

var customerSelect = from customer in Customers
                     group customer by customer.Country into customerGroup
                     select new
                     {
                         country = customerGroup.Key,
                         NumberOfCustomer = customerGroup.Count(),
                         TotalValue = customerGroup.SelectMany(g => g.Orders)
                         .Sum(o => o.Total)
                     };
foreach (var item in customerSelect)
{
    Console.WriteLine(
        $"Country: {item.country}, Count: {item.NumberOfCustomer}, TotalOrderValue: {item.TotalValue}");
}

foreach (var item in Products)
{
    Console.WriteLine(item.UnitsInStock);
}

int totalStock = Products.Sum(p => p.UnitsInStock);

Console.WriteLine(totalStock);

var cheapest = Products.Min(p => p.UnitPrice);
var max = Products.Max(p => p.UnitPrice);

var disList = Products
    .Select(p => p.Category)
    .Distinct();
int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
int[] setB = { 3, 6, 9, 12, 15, 13 };

var setASelect = setA.Except(setB);

string[] list1 = { "Germany", "France", "UK", "Spain" };
string[] list2 = { "france", "SPAIN", "Italy" };

var list1Select =  list1.Except(list2,StringComparer.OrdinalIgnoreCase);

