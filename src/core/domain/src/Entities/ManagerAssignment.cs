using Domain.Exceptions;

namespace Domain.Entities;

public class ManagerAssignment
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public int ManagerId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime? EndDate { get; private set; }

    public ManagerAssignment()
    {
    }

    public ManagerAssignment(int managerId, DateTime startDate)
    {
        if (managerId <= 0)
        {
            throw new DomainException(
                "Manager id is invalid.");
        }

        ManagerId = managerId;
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