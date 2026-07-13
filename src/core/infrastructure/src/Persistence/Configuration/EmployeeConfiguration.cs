using Domain.Aggregates.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        ConfigureSalary(builder);

        ConfigureCurrentPosition(builder);

        ConfigurePositionHistory(builder);

        ConfigureManagerAssignments(builder);
    }

    private static void ConfigureSalary(EntityTypeBuilder<Employee> builder)
    {
        builder.OwnsOne(
            employee => employee.Salary,
            salary =>
            {
                salary.Property(s => s.Amount)
                    .HasColumnName("Salary")
                    .HasPrecision(18, 2)
                    .IsRequired();
            });
    }

    private static void ConfigureCurrentPosition(EntityTypeBuilder<Employee> builder)
    {
        builder.OwnsOne(
            employee => employee.CurrentPosition,
            position =>
            {
                position.Property(p => p.Name)
                    .HasColumnName("CurrentPosition")
                    .HasMaxLength(100);

                position.Property(p => p.Type)
                    .HasColumnName("CurrentPositionType");
            });
    }

    private static void ConfigurePositionHistory(EntityTypeBuilder<Employee> builder)
    {
        builder.OwnsMany(
            employee => employee.PositionHistory,
            history =>
            {
                history.ToTable("EmployeePositionHistory");

                history.WithOwner();

                history.HasKey(h => h.Id);

                history.Property(h => h.StartDate)
                    .IsRequired();

                history.Property(h => h.EndDate);

                history.OwnsOne(
                    h => h.Position,
                    position =>
                    {
                        position.Property(p => p.Name)
                            .HasColumnName("Position");

                        position.Property(p => p.Type)
                            .HasColumnName("PositionType");
                    });
            });
    }

    private static void ConfigureManagerAssignments(
    EntityTypeBuilder<Employee> builder)
    {
        builder.OwnsMany(
            employee => employee.ManagerAssignments,
            assignment =>
            {
                assignment.ToTable("EmployeeManagerAssignments");

                assignment.WithOwner();

                assignment.HasKey(a => a.Id);

                assignment.HasKey("Id");

                assignment.Property(a => a.ManagerId)
                    .IsRequired();

                assignment.Property(a => a.StartDate)
                    .IsRequired();

                assignment.Property(a => a.EndDate);
            });
    }
}