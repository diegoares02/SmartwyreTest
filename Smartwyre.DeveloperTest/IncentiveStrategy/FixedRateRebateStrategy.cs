using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveStrategy
{
    public class FixedRateRebateStrategy : IIncentiveStrategy
    {
        private Rebate _rebate;
        private Product _product;
        private CalculateRebateRequest _calculateRebateRequest;
        public FixedRateRebateStrategy(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            _rebate = rebate;
            _product = product;
            _calculateRebateRequest = request;
        }
        public CalculateRebateResult Calculate()
        {
            var result = new CalculateRebateResult();
            var rebateAmount = 0m;

            if (_product == null ||
                    !_product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
            {
                result.Success = false;
            }
            else
            {
                rebateAmount += _product.Price * _rebate.Percentage * _calculateRebateRequest.Volume;
                result.Success = true;
            }
            return result;
        }
    }
}
