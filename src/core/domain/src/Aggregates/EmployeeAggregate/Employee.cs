using Domain.Entities;
using Domain.Exceptions;
using Domain.Patterns.Strategy;
using Domain.ValueObjects;

namespace Domain.Aggregates.EmployeeAggregate;

public class Employee
{
    private readonly List<PositionHistory> _positionHistory = [];

    private readonly List<ManagerAssignment> _managerAssignments = [];

    private Employee()
    {
    }

    private Employee(
        string name,
        Salary salary,
        Position currentPosition)
    {
        Name = name;
        Salary = salary;
        CurrentPosition = currentPosition;
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public Salary Salary { get; private set; } = default!;

    public Position CurrentPosition { get; private set; } = default!;

    public IReadOnlyCollection<PositionHistory> PositionHistory => _positionHistory.AsReadOnly();

    public IReadOnlyCollection<ManagerAssignment> ManagerAssignments => _managerAssignments.AsReadOnly();

    public static Employee Create(string name, Salary salary, Position currentPosition)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Employee name is required.");
        }

        ArgumentNullException.ThrowIfNull(salary);

        ArgumentNullException.ThrowIfNull(currentPosition);

        var employee = new Employee(name.Trim(), salary, currentPosition);

        employee._positionHistory.Add(new PositionHistory(currentPosition, DateTime.UtcNow));

        return employee;
    }

    public decimal CalculateYearlyBonus(IBonusStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);

        var context = new BonusCalculationContext(
            Salary,
            CurrentPosition);

        return strategy.Calculate(context);
    }

    public void ChangePosition(Position newPosition)
    {
        ArgumentNullException.ThrowIfNull(newPosition);

        _positionHistory.Last().Finish(DateTime.UtcNow);

        CurrentPosition = newPosition;

        _positionHistory.Add(
            new PositionHistory(newPosition, DateTime.UtcNow));
    }

    public void AssignManager(int managerId)
    {
        if (managerId <= 0)
        {
            throw new DomainException(
                "Manager id is invalid.");
        }

        _managerAssignments.LastOrDefault()?.Finish(DateTime.UtcNow);

        _managerAssignments.Add(
            new ManagerAssignment(managerId, DateTime.UtcNow));
    }

    public void UpdateSalary(Salary salary)
    {
        ArgumentNullException.ThrowIfNull(salary);

        Salary = salary;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Employee name is required.");
        }

        Name = name.Trim();
    }
}