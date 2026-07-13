using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class PositionHistory
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public Position Position { get; private set; } = default!;

    public DateTime StartDate { get; }

    public DateTime? EndDate { get; private set; }

    private PositionHistory()
    {
    }

    public PositionHistory(Position position, DateTime startDate)
    {
        ArgumentNullException.ThrowIfNull(position);

        if (startDate == default)
        {
            throw new DomainException(
                "Start date is required.");
        }

        Position = position;
        StartDate = startDate;
    }

    public void Finish(DateTime endDate)
    {
        if (endDate < StartDate)
        {
            throw new DomainException(
                "End date cannot be earlier than start date.");
        }

        EndDate = endDate;
    }
}