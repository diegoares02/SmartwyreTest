using Microsoft.EntityFrameworkCore;
using System;
using Smartwyre.DeveloperTest.DataContext;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.DataSeed
{
    public interface IDataContextSeed
    {
        void Initialize();
    }
    public class DataContextSeed : IDataContextSeed
    {
        private readonly SmartwyreDataContext _context;

        public DataContextSeed(SmartwyreDataContext context)
        {
            _context = context;
        }
        public void Initialize()
        {
            if (_context.Products.Count() == 0)
            {
                var products = new List<Product>();
                products.Add(new Product { Id = 1, Identifier = "product1", SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 10, Uom = Guid.NewGuid().ToString() });
                products.Add(new Product { Id = 2, Identifier = "product2", SupportedIncentives = SupportedIncentiveType.AmountPerUom, Price = 20, Uom = Guid.NewGuid().ToString() });
                products.Add(new Product { Id = 3, Identifier = "product3", SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 30, Uom = Guid.NewGuid().ToString() });

                _context.Products.AddRange(products);

                var rebates = new List<Rebate>();
                rebates.Add(new Rebate { Id = 1, Identifier = "rebate1", Amount = 10, Incentive = IncentiveType.FixedRateRebate, Percentage = 10 });
                rebates.Add(new Rebate { Id = 2, Identifier = "rebate2", Amount = 20, Incentive = IncentiveType.FixedCashAmount, Percentage = 20 });
                rebates.Add(new Rebate { Id = 3, Identifier = "rebate3", Amount = 30, Incentive = IncentiveType.AmountPerUom, Percentage = 30 });
                rebates.Add(new Rebate { Id = 4, Identifier = "rebate4", Amount = 0, Incentive = IncentiveType.AmountPerUom, Percentage = 30 });

                _context.Rebates.AddRange(rebates);

                _context.SaveChanges();
            }            
        }
    }
}
