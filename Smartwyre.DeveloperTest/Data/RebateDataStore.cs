using System;
using System.Collections.Generic;
using System.Linq;
using Smartwyre.DeveloperTest.DataContext;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;
public interface IRebateDataStore
{
    Rebate GetRebate(string rebateIdentifier);
    List<Rebate> GetRebates();
    void StoreCalculationResult(Rebate account, decimal rebateAmount);
}
public class RebateDataStore : IRebateDataStore
{
    private readonly SmartwyreDataContext _context;
    public RebateDataStore(SmartwyreDataContext context)
    {
        _context = context;
    }
    public Rebate GetRebate(string rebateIdentifier)
    {
        return _context.Rebates.FirstOrDefault(x => x.Identifier == rebateIdentifier);
    }

    public List<Rebate> GetRebates()
    {
        return _context.Rebates.ToList();
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        RebateCalculation rebateCalculation = new RebateCalculation();
        rebateCalculation.Identifier = Guid.NewGuid().ToString();
        rebateCalculation.RebateIdentifier = account.Identifier;
        rebateCalculation.IncentiveType = account.Incentive;
        rebateCalculation.Amount = rebateAmount;
        _context.RebateCalculations.Add(rebateCalculation);
        _context.SaveChanges();
    }
}
