using Domain.ValueObjects;

namespace Domain.Patterns.Strategy;

public sealed class RegularEmployeeBonusStrategy : IBonusStrategy
{
    private const decimal BonusPercentage = 0.10m;

    public decimal Calculate(
        BonusCalculationContext context)
    {
        return context.Salary.Amount * BonusPercentage;
    }
}