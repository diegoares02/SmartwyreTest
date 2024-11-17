using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveStrategy
{
    public class FixedCashAmountStrategy : IIncentiveStrategy
    {
        private Rebate _rebate;
        private Product _product;
        public FixedCashAmountStrategy(Rebate rebate, Product product)
        {
            _rebate = rebate;
            _product = product;
        }
        public CalculateRebateResult Calculate()
        {
            var result = new CalculateRebateResult();
            var rebateAmount = 0m;

            if (!_product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) ||
                    _rebate.Amount == 0)
            {
                result.Success = false;
            }
            else
            {
                rebateAmount = _rebate.Amount;
                result.Success = true;
            }
            return result;
        }
    }
}
