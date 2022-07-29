using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQSamples
{
    public class SamplesViewModel
    {
        #region Constructor
        public SamplesViewModel()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
            Sales = SalesOrderDetailRepository.GetAll();
        }
        #endregion

        #region Properties
        public bool UseQuerySyntax { get; set; }
        public List<Product> Products { get; set; }
        public List<SalesOrderDetail> Sales { get; set; }
        public string ResultText { get; set; }
        #endregion

        #region GetAllLooping
        /// <summary>
        /// Put all products into a collection via looping, no LINQ
        /// </summary>
        public void GetAllLooping()
        {
            List<Product> list = new List<Product>();

            foreach (Product product in Products)
            {
                list.Add(product);
            }

            ResultText = $"Total Products: {list.Count}";
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Put all products into a collection using LINQ
        /// </summary>
        public void GetAll()
        {
            List<Product> list = new List<Product>();

            if (UseQuerySyntax)
            {
                // Query Syntax
                list = (from prod in Products select prod).ToList();
            }
            else
            {
                // Method Syntax
                list = Products.Select(prod => prod).ToList();
            }

            ResultText = $"Total Products: {list.Count}";
        }
        #endregion

        #region GetSingleColumn
        /// <summary>
        /// Select a single column
        /// </summary>
        public void GetSingleColumn()
        {
            StringBuilder sb = new StringBuilder(1024);
            List<string> list = new List<string>();

            if (UseQuerySyntax)
            {
                // Query Syntax
                list.AddRange(from prod in Products select prod.Name);
            }
            else
            {
                // Method Syntax
                list.AddRange(Products.Select(prod => prod.Name));
            }

            foreach (string item in list)
            {
                sb.AppendLine(item);
            }

            ResultText = $"Total Products: {list.Count}" + Environment.NewLine + sb.ToString();
            Products.Clear();
        }
        #endregion

        #region GetSpecificColumns
        /// <summary>
        /// Select a few specific properties from products and create new Product objects
        /// </summary>
        public void GetSpecificColumns()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products select new Product { ProductID = prod.ProductID, Name = prod.Name, Size = prod.Size }).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.Select(prod => new Product { ProductID = prod.ProductID, Name = prod.Name, Size = prod.Size }).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region AnonymousClass
        /// <summary>
        /// Create an anonymous class from selected product properties
        /// </summary>
        public void AnonymousClass()
        {
            StringBuilder sb = new StringBuilder(2048);

            if (UseQuerySyntax)
            {
                // Query Syntax
                var products = (from prod in Products select new { Identifier = prod.ProductID, ProductName = prod.Name, ProductSize = prod.Size });
                // Loop through anonymous class
                foreach (var prod in products)
                {
                    sb.AppendLine($"Product ID: {prod.Identifier}");
                    sb.AppendLine($"   Product Name: {prod.ProductName}");
                    sb.AppendLine($"   Product Size: {prod.ProductSize}");
                }
            }
            else
            {
                // Method Syntax
                var products = Products.Select(prod => new { Identifier = prod.ProductID, ProductName = prod.Name, ProductSize = prod.Size });
                // Loop through anonymous class
                foreach (var prod in products)
                {
                    sb.AppendLine($"Product ID: {prod.Identifier}");
                    sb.AppendLine($"   Product Name: {prod.ProductName}");
                    sb.AppendLine($"   Product Size: {prod.ProductSize}");
                }
            }

            ResultText = sb.ToString();
            Products.Clear();
        }
        #endregion

        #region OrderBy
        /// <summary>
        /// Order products by Name
        /// </summary>
        public void OrderBy()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products orderby prod.Name select prod).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region OrderByDescending Method
        /// <summary>
        /// Order products by name in descending order
        /// </summary>
        public void OrderByDescending()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products orderby prod.Name descending select prod).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderByDescending(prod => prod.Name).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region OrderByTwoFields Method
        /// <summary>
        /// Order products by Color descending, then Name
        /// </summary>
        public void OrderByTwoFields()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products orderby prod.Color descending, prod.Name select prod).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderByDescending(prod => prod.Color).ThenBy(prod => prod.Name).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region WhereExpression
        /// <summary>
        /// Filter products using where. If the data is not found, an empty list is returned
        /// </summary>
        public void WhereExpression()
        {
            string search = "L";
            if(UseQuerySyntax)
            {
                // Query Systax
                Products = (from prod in Products where prod.Name.StartsWith(search) select prod).ToList();
            } 
            else
            {
                // Method Systax
                Products = Products.Where(prod => prod.Name.StartsWith(search)).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region WhereTwoFields
        /// <summary>
        /// Filter products using where with two fields. If the data is not found, an empty list is returned
        /// </summary>
        public void WhereTwoFields()
        {
            string search = "L";
            decimal cost = 100;
            if(UseQuerySyntax)
            {
                // Query syntax
                Products = (from prod in Products where prod.Name.StartsWith(search) && prod.StandardCost > cost select prod).ToList();
            } 
            else
            {
                // Method syntax
                Products = Products.Where(prod => prod.Name.StartsWith(search) && prod.StandardCost > cost).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region WhereExtensionMethod
        /// <summary>
        /// Filter products using a custom extension method
        /// </summary>
        public void WhereExtensionMethod()
        {
            string search = "Red";
            if(UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products select prod).ByColor(search).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.ByColor(search).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region First
        /// <summary>
        /// Locate a specific product using First(). First() searches forward in the list.
        /// NOTE: First() throws an exception if the result does not produce any values
        /// </summary>
        public void First()
        {
            string color = "asdf";
            Product product = null;
            try
            {
                if (UseQuerySyntax)
                {
                    // Query Syntax
                    product = (from prod in Products select prod).First(prod => prod.Color == color);
                }
                else
                {
                    // Method Syntax
                    product = Products.First(prod => prod.Color == color);
                }
                Products.Clear();

                ResultText = $"Found: {product}";
            } 
            catch
            {
                ResultText = "Not Found";
            }
            
        }
        #endregion

        #region FirstOrDefault
        /// <summary>
        /// Locate a specific product using FirstOrDefault(). FirstOrDefault() searches forward in the list.
        /// NOTE: FirstOrDefault() returns a null if no value is found
        /// </summary>
        public void FirstOrDefault()
        {
            const string color = "Red";
            Product product = null;
            if(UseQuerySyntax)
            {
                // Query Syntax
                product = (from prod in Products select prod).FirstOrDefault(prod => prod.Color == color);
            }
            else
            {
                // Method Syntax
                product = Products.FirstOrDefault(prod => prod.Color == color);
            }
            Products.Clear();
            ResultText = product != null ? $"Found {product}" : "Not Found";
        }
        #endregion

        #region Last
        /// <summary>
        /// Locate a specific product using Last(). Last() searches from the end of the list backwards.
        /// NOTE: Last returns the last value from a collection, or throws an exception if no value is found
        /// </summary>
        public void Last()
        {
            const string color = "Red";
            Product product = null;
            try
            {
                if (UseQuerySyntax)
                {
                    // Query Syntax
                    product = (from prod in Products select prod).Last(prod => prod.Color == color);
                }
                else
                {
                    // Method Syntax
                    product = Products.Last(prod => prod.Color == color);
                }

                Products.Clear();
                ResultText = $"Found: {product}";
            }
            catch
            {
                ResultText = "Not Found";
            }
            
        }
        #endregion

        #region LastOrDefault
        /// <summary>
        /// Locate a specific product using LastOrDefault(). LastOrDefault() searches from the end of the list backwards.
        /// NOTE: LastOrDefault returns the last value in a collection or a null if no values are found
        /// </summary>
        public void LastOrDefault()
        {
            const string color = "Red";
            Product product = null;
            if(UseQuerySyntax)
            {
                // Query Syntax
                product = (from prod in Products select prod).LastOrDefault(prod => prod.Color == color);
            } 
            else
            {
                // Method Syntax
                product = Products.LastOrDefault(prod => prod.Color == color);
            }

            Products.Clear();
            ResultText = product != null ? $"Found {product}" : "Not Found";
        }
        #endregion

        #region Single
        /// <summary>
        /// Locate a specific product using Single()
        /// NOTE: Single() expects only a single element to be found in the collection, otherwise an exception is thrown
        /// </summary>
        public void Single()
        {
            const int productId = 706;
            Product product = null;
            try
            {
                if (UseQuerySyntax)
                {
                    // Query Syntax
                    product = (from prod in Products select prod).Single(prod => prod.ProductID == productId);
                }
                else
                {
                    // Method Syntax
                    product = Products.Single(prod => prod.ProductID == productId);
                }

                Products.Clear();
                ResultText = $"Found {product}";

            }
            catch
            {
                ResultText = "Not Found or multiple elements found";
            }
        }
        #endregion

        #region SingleOrDefault
        /// <summary>
        /// Locate a specific product using SingleOrDefault()
        /// NOTE: SingleOrDefault() returns a single element found in the collection, or a null value if none found in the collection, if multiple values are found an exception is thrown.
        /// </summary>
        public void SingleOrDefault()
        {
            const int productId = 706;
            Product product = null;
            if(UseQuerySyntax)
            {
                // Query Syntax
                product = (from prod in Products select prod).SingleOrDefault(prod => prod.ProductID == productId);
            }
            else
            {
                // Method Syntax
                product = Products.SingleOrDefault(prod => prod.ProductID == productId);
            }

            Products.Clear();
            ResultText = product != null ? $"Found {product}" : "Not Found or multiple elements found";
        }
        #endregion

        #region ForEach Method
        /// <summary>
        /// ForEach allows you to iterate over a collection to perform assignments within each object.
        /// In this sample, assign the Length of the Name property to a property called NameLength
        /// When using the Query syntax, assign the result to a temporary variable.
        /// </summary>
        public void ForEach()
        {
            if(UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products let temp = prod.NameLength = prod.Name.Length select prod).ToList();
            }
            else
            {
                // Method Syntax
                Products.ForEach(prod => prod.NameLength = prod.Name.Length);
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region ForEachCallingMethod Method
        /// <summary>
        /// Iterate over each object in the collection and call a method to set a property
        /// This method passes in each Product object into the SalesForProduct() method
        /// In the SalesForProduct() method, the total sales for each Product is calculated
        /// The total is placed into each Product objects' ResultText property
        /// </summary>
        public void ForEachCallingMethod()
        {
            if(UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products let temp = prod.TotalSales = SalesForProduct(prod) select prod).ToList();
            }
            else
            {
                // Method Syntax
                Products.ForEach(prod => prod.TotalSales = SalesForProduct(prod));
            }

            ResultText = $"Total Products: {Products.Count}";
        }

        private decimal SalesForProduct(Product prod)
        {
            return Sales.Where(sale => sale.ProductID == prod.ProductID).Sum(sale => sale.LineTotal);
        }
        #endregion

        #region Take Method
        public void Take()
        {
            if(UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products orderby prod.Name select prod).Take(5).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).Take(5).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region TakeWhile Method
        public void TakeWhile()
        {
            if(UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products orderby prod.Name select prod).TakeWhile(prod => prod.Name.StartsWith("A")).ToList();
            }
            else
            {
                Products = Products.OrderBy(prod => prod.Name).TakeWhile(prod => prod.Name.StartsWith("A")).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region Skip Method
        public void Skip()
        {
            if(UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products orderby prod.Name select prod).Skip(20).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).Skip(20).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region SkipWhile Method
        public void SkipWhile()
        {
            if(UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products orderby prod.Name select prod).SkipWhile(prod => prod.Name.StartsWith("A")).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).SkipWhile(prod => prod.Name.StartsWith("A")).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region Distinct
        public void Distinct()
        {
            List<string> colors;

            if(UseQuerySyntax)
            {
                // Query Syntax
                colors = (from prod in Products select prod.Color).Distinct().ToList();
            }
            else
            {
                // Method Syntax
                colors = Products.Select(prod => prod.Color).Distinct().ToList();
            }

            // Build string of Distinct Colors
            foreach (var color in colors)
            {
                Console.WriteLine($"Color: {color}");
            }
            Console.WriteLine($"Total Colors: {colors.Count}");

            // Clear products
            Products.Clear();
        }
        #endregion
    }
}
