using Domain.ValueObjects;

namespace Domain.Patterns.Strategy;

public interface IBonusStrategy
{
    decimal Calculate(BonusCalculationContext context);
}