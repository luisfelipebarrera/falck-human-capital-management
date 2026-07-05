using Domain.Enums;
using Domain.Patterns.Strategy;

namespace Domain.Patterns.Factory;

public interface IBonusStrategyFactory
{
    IBonusStrategy Create(PositionType position);
}