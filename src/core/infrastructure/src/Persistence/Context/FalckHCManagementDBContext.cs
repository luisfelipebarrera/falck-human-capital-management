
using Domain.Aggregates.EmployeeAggregate;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Context;

public class FalckHCManagementDBContext : DbContext
{
    public FalckHCManagementDBContext(DbContextOptions<FalckHCManagementDBContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        base.OnModelCreating(modelBuilder);
    }

}