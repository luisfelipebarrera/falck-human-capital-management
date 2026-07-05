namespace Domain.Entities;

public class ManagerAssignment
{
    public int EmployeeId { get; }

    public int ManagerId { get; }

    public DateTime StartDate { get; }

    public DateTime? EndDate { get; private set; }

    public ManagerAssignment(
        int employeeId,
        int managerId,
        DateTime startDate)
    {
        EmployeeId = employeeId;
        ManagerId = managerId;
        StartDate = startDate;
    }

    public void Finish(DateTime endDate)
    {
        EndDate = endDate;
    }
}