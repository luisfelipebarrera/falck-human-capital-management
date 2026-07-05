using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed record Salary
{
    public decimal Amount { get; }

    public Salary(decimal amount)
    {
        if (amount < 0)
        {
            throw new DomainException("Salary cannot be negative.");
        }

        Amount = amount;
    }

    public override string ToString()
    {
        return Amount.ToString("0.00");
    }
}