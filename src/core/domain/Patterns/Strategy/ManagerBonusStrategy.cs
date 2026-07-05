using Domain.ValueObjects;

namespace Domain.Patterns.Strategy;

public sealed class ManagerBonusStrategy : IBonusStrategy
{
    private const decimal BonusPercentage = 0.20m;

    public decimal Calculate(
        BonusCalculationContext context)
    {
        return context.Salary.Amount * BonusPercentage;
    }
}