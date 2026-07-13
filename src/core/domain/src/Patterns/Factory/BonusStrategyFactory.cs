using Domain.Enums;
using Domain.Exceptions;
using Domain.Patterns.Strategy;

namespace Domain.Patterns.Factory;

public sealed class BonusStrategyFactory : IBonusStrategyFactory
{
    public IBonusStrategy Create(PositionType position)
    {
        return position switch
        {
            PositionType.RegularEmployee =>
                new RegularEmployeeBonusStrategy(),

            PositionType.Manager =>
                new ManagerBonusStrategy(),

            _ => throw new DomainException($"Position '{position}' is not supported.")
        };
    }
}