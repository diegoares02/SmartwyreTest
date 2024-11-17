using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IncentiveStrategy;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebaseDataStore;
    private readonly IProductDataStore _productDataStore;
    public RebateService(IRebateDataStore rebaseDataStore, IProductDataStore productDataStore)
    {
        _rebaseDataStore = rebaseDataStore;
        _productDataStore = productDataStore;
    }
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var incentiveContext = new IncentiveContext();
        var rebateAmount = 0m;

        Rebate rebate = _rebaseDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null) return null;

        switch (rebate.Incentive)
        {
            case IncentiveType.FixedCashAmount:
                incentiveContext.SetStrategy(new FixedCashAmountStrategy(rebate, product));
                break;

            case IncentiveType.FixedRateRebate:
                incentiveContext.SetStrategy(new FixedRateRebateStrategy(rebate, product, request));
                break;

            case IncentiveType.AmountPerUom:
                incentiveContext.SetStrategy(new AmountPerUomStrategy(rebate, product, request));
                break;
        }
        var result = incentiveContext.ExecuteStrategy();
        if (result.Success)
        {
            _rebaseDataStore.StoreCalculationResult(rebate, rebateAmount);
        }

        return result;
    }
}
