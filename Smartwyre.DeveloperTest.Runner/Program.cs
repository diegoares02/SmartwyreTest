using System;
using System.Collections.Generic;
using Ninject;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.DataSeed;
using Smartwyre.DeveloperTest.Dependency;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        IKernel kernel = new StandardKernel();
        kernel.Load<DependencyModule>();

        DataContextSeed dataContextSeed = kernel.Get<DataContextSeed>();
        dataContextSeed.Initialize();

        RebateService rebateService = kernel.Get<RebateService>();
        RebateDataStore rebateDataStore = kernel.Get<RebateDataStore>();
        ProductDataStore productDataStore = kernel.Get<ProductDataStore>();

        List<Product> products = productDataStore.GetProducts();
        List<Rebate> rebates = rebateDataStore.GetRebates();

        PrintProducts(products);
        PrintRebates(rebates);

        try
        {
            Console.WriteLine("Enter Product Identifier");
            string product = Console.ReadLine();
            Console.WriteLine("Enter Product Identifier");
            string rebate = Console.ReadLine();
            Console.WriteLine("Enter the volume in number");
            int volume = Convert.ToInt32(Console.ReadLine());

            CalculateRebateRequest request = new CalculateRebateRequest();
            request.ProductIdentifier = product;
            request.RebateIdentifier = rebate;
            request.Volume = volume;

            CalculateRebateResult result = rebateService.Calculate(request);
            Console.WriteLine(result.Success ? "The rebate was calculated successfully": "There was an error during rebate calculation");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }        
        Console.ReadKey();
    }

    private static void PrintProducts(List<Product> products)
    {
        Console.WriteLine("PRODUCTS");
        Console.WriteLine("Identifier   Price   Uom             SupportIncentive");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Identifier}    {product.Price}     {product.Uom}   {product.SupportedIncentives}");
        }
    }

    private static void PrintRebates(List<Rebate> rebates)
    {
        Console.WriteLine("REBATES");
        Console.WriteLine("Identifier   Incentive       Amount          Percentage");
        foreach (var rebate in rebates)
        {
            Console.WriteLine($"{rebate.Identifier}    {rebate.Incentive}     {rebate.Amount}       {rebate.Percentage}");
        }
    }
}
