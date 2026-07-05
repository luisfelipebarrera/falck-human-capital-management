using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class PositionHistory
{
    public int EmployeeId { get; }

    public Position Position { get; private set; }

    public DateTime StartDate { get; }

    public DateTime? EndDate { get; private set; }

    public PositionHistory(int employeeId, Position position, DateTime startDate)
    {
        if (startDate == default)
        {
            throw new DomainException(
                "Start date is required.");
        }

        EmployeeId = employeeId;
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