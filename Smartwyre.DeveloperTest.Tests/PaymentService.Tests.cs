using Ninject;
using Smartwyre.DeveloperTest.DataSeed;
using Smartwyre.DeveloperTest.Dependency;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;
public class PaymentServiceTests
{
    private RebateService rebateService;
    public PaymentServiceTests()
    {
        IKernel kernel = new StandardKernel();
        kernel.Load<DependencyModule>();
        DataContextSeed dataContextSeed = kernel.Get<DataContextSeed>();
        dataContextSeed.Initialize();
        rebateService = kernel.Get<RebateService>();
    }
    [Fact]
    public void TestFixedCashAmount()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product3";
        request.RebateIdentifier = "rebate2";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.True(result.Success);
    }

    [Fact]
    public void TestFixedCashAmountRebateNull()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product3";
        request.RebateIdentifier = "rebate5";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.Null(result);
    }

    [Fact]
    public void TestFixedCashAmountRebateAmountZero()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product3";
        request.RebateIdentifier = "rebate4";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }

    [Fact]
    public void TestFixedCashAmountProductFixedRateRebateRebateAmountNotZero()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product1";
        request.RebateIdentifier = "rebate2";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }

    [Fact]
    public void TestFixedRateRebate()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product1";
        request.RebateIdentifier = "rebate1";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.True(result.Success);
    }

    [Fact]
    public void TestFixedRateRebateNotFixedRateRebateSupportIncentive()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product2";
        request.RebateIdentifier = "rebate1";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }

    [Fact]
    public void TestFixedRateRebateProductNull()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product";
        request.RebateIdentifier = "rebate1";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }

    [Fact]
    public void TestAmountPerUom()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product2";
        request.RebateIdentifier = "rebate3";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.True(result.Success);
    }

    [Fact]
    public void TestAmountPerUomProductNull()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product";
        request.RebateIdentifier = "rebate3";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }

    [Fact]
    public void TestAmountPerUomNotAmountPerUomSupportIncentive()
    {
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.ProductIdentifier = "product1";
        request.RebateIdentifier = "rebate3";
        request.Volume = 0;

        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }
}
