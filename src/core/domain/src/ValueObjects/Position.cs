using Domain.Enums;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed record Position
{
    public PositionType Type { get; }

    public string Name { get; }

    public Position(PositionType type, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Position name cannot be empty.");
        }

        Type = type;
        Name = name;
    }
}