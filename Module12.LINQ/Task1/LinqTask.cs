using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(x =>
            {
                decimal sum = x.Orders.Sum(o => o.Total);
                return sum > limit;
            });
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
        IEnumerable<Customer> customers,
        IEnumerable<Supplier> suppliers)
        {
            return customers.Select(x => (customer: x, suppliers: suppliers.Where(s => s.Country == x.Country && s.City == x.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
        IEnumerable<Customer> customers,
        IEnumerable<Supplier> suppliers)
        {
            return customers.GroupJoin(suppliers,
            c => $"{c.Country}{c.City}",
            s => $"{s.Country}{s.City}",
            (c, s) => (customers: c, suppliers: s));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(customer => customer.Orders.Any(o => o.Total > limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(IEnumerable<Customer> customers)
        {
            return customers.Where(c => c.Orders.Length > 0)
                            .Select(customer => (customer, dateOfEntry: customer.Orders.Min(o => o.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(IEnumerable<Customer> customers)
        {
            return customers.Where(c => c.Orders.Length > 0)
                            .Select(customer => (customer, dateOfEntry: customer.Orders.Min(o => o.OrderDate)))
                            .OrderBy(x => x.dateOfEntry.Year)
                            .ThenBy(x => x.dateOfEntry.Month)
                            .ThenByDescending(x => x.customer.Orders.Max(o => o.Total))
                            .ThenBy(x => x.customer.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers.Where(customer =>
            {
                bool isNonDigitPostalCode = !int.TryParse(customer.PostalCode, out var res);
                bool isUndefinedRegion = string.IsNullOrWhiteSpace(customer.Region);
                bool hasNoOperatorCode = customer.Phone.Count(c => c is '(' or ')') == 0;

                return isNonDigitPostalCode || isUndefinedRegion || hasNoOperatorCode;
            });
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            return products.GroupBy(x => x.Category,
                           (c, productsByCategory) =>
                           {
                               var unitsInStockGroup = productsByCategory.GroupBy(p => p.UnitsInStock,
                               (unitsInStock, productsByUnityStock) => new Linq7UnitsInStockGroup
                               {
                                   UnitsInStock = unitsInStock, Prices = productsByUnityStock.Select(p => p.UnitPrice).OrderBy(x => x)
                               });

                               return new Linq7CategoryGroup { Category = c, UnitsInStockGroup = unitsInStockGroup };
                           })
                           .ToList();
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
        IEnumerable<Product> products,
        decimal cheap,
        decimal middle,
        decimal expensive)
        {
            return products.GroupBy(product =>
            {
                decimal category = product.UnitPrice switch
                {
                    var price when price <= cheap => cheap,
                    var price when price > cheap && price <= middle => middle,
                    var price when price > middle && price <= expensive => expensive,
                    _ => product.UnitPrice
                };

                return category;
            },
            (category, p) => (category, products: p));
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(IEnumerable<Customer> customers)
        {
            return customers.GroupBy(c => c.City,
                            (city, c) =>
                            {
                                var customersGroup = c.ToArray();
                                var averageIncome = (int)Math.Round(customersGroup.Average(cg => cg.Orders.Sum(o => o.Total)));
                                var averageIntensity = (int)Math.Round(customersGroup.Average(cg => cg.Orders.Length));
                                return (city, averageIncome, averageIntensity);
                            })
                            .ToList();
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var countryNames = suppliers.Select(s => s.Country).Distinct().OrderBy(x => x.Length).ThenBy(x => x);

            return string.Concat(countryNames);
        }
    }
}