using System.Collections.Generic;
using System.Linq;
using Smartwyre.DeveloperTest.DataContext;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IProductDataStore
{
    Product GetProduct(string productIdentifier);
    List<Product> GetProducts();
}
public class ProductDataStore : IProductDataStore
{
    private readonly SmartwyreDataContext _context;
    public ProductDataStore(SmartwyreDataContext context)
    {
        _context = context;
    }
    public Product GetProduct(string productIdentifier)
    {
        return _context.Products.FirstOrDefault(x => x.Identifier == productIdentifier);
    }

    public List<Product> GetProducts()
    {
        return _context.Products.ToList();
    }
}
