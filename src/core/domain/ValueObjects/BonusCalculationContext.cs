using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed record BonusCalculationContext
{
    public Salary Salary { get; }

    public Position Position { get; }

    public BonusCalculationContext(
        Salary salary,
        Position position)
    {
        Salary = salary ?? throw new DomainException("Salary is required.");

        Position = position ?? throw new DomainException("Position is required.");
    }
}