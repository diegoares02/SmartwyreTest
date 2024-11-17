using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveStrategy
{
    public interface IIncentiveStrategy
    {
        CalculateRebateResult Calculate();
    }
    public class IncentiveContext
    {
        private IIncentiveStrategy _strategy;

        public void SetStrategy(IIncentiveStrategy strategy)
        {
            _strategy = strategy;
        }

        public CalculateRebateResult ExecuteStrategy()
        { return _strategy.Calculate(); }
    }
}
