using Domain.Entities;
using Domain.Exceptions;
using Domain.Patterns.Factory;
using Domain.ValueObjects;

namespace Domain.Aggregates.EmployeeAggregate;

public class Employee
{
    private readonly List<PositionHistory> _positionHistory = [];

    private readonly List<ManagerAssignment> _managerAssignments = [];

    private readonly IBonusStrategyFactory _bonusStrategyFactory;

    public int Id { get; }

    public string Name { get; private set; }

    public Salary Salary { get; private set; }

    public Position CurrentPosition { get; private set; }

    public IReadOnlyCollection<PositionHistory> PositionHistory => _positionHistory.AsReadOnly();

    public IReadOnlyCollection<ManagerAssignment> ManagerAssignments => _managerAssignments.AsReadOnly();

    public Employee(int id, string name, Salary salary, Position currentPosition, IBonusStrategyFactory bonusStrategyFactory)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Employee name is required.");
        }

        Id = id;

        Name = name;

        Salary = salary ?? throw new DomainException("Salary is required.");

        CurrentPosition = currentPosition ?? throw new DomainException("Position is required.");

        _bonusStrategyFactory = bonusStrategyFactory ?? throw new DomainException("Bonus strategy factory is required.");

        _positionHistory.Add(
            new PositionHistory(
                id,
                currentPosition,
                DateTime.UtcNow));
    }

    public decimal CalculateYearlyBonus()
    {
        var strategy =
            _bonusStrategyFactory.Create(CurrentPosition.Type);

        var context =
            new BonusCalculationContext(
                Salary,
                CurrentPosition);

        return strategy.Calculate(context);
    }

    public void ChangePosition(Position newPosition)
    {
        ArgumentNullException.ThrowIfNull(newPosition);

        var currentPositionHistory = _positionHistory.Last();

        currentPositionHistory.Finish(DateTime.UtcNow);

        CurrentPosition = newPosition;

        _positionHistory.Add(
            new PositionHistory(
                Id,
                newPosition,
                DateTime.UtcNow));
    }

    public void AssignManager(int managerId)
    {
        if (managerId <= 0)
        {
            throw new DomainException("Manager id is invalid.");
        }

        _managerAssignments
            .LastOrDefault()
            ?.Finish(DateTime.UtcNow);

        _managerAssignments.Add(
            new ManagerAssignment(
                Id,
                managerId,
                DateTime.UtcNow));
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
            throw new DomainException("Employee name is required.");
        }

        Name = name;
    }
}