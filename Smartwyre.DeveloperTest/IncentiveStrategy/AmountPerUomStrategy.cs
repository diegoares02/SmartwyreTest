using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveStrategy
{
    public class AmountPerUomStrategy : IIncentiveStrategy
    {
        private Rebate _rebate;
        private Product _product;
        private CalculateRebateRequest _calculateRebateRequest;
        public AmountPerUomStrategy(Rebate rebate, Product product, CalculateRebateRequest request)
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
                    !_product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
            {
                result.Success = false;
            }
            else
            {
                rebateAmount += _rebate.Amount * _calculateRebateRequest.Volume;
                result.Success = true;
            }
            return result;
        }
    }
}
