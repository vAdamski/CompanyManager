using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedNever();

		builder.Property(x => x.FirstName).IsRequired().HasMaxLength(256);
		builder.Property(x => x.LastName).IsRequired().HasMaxLength(256);
		builder.Property(x => x.UserName).IsRequired().HasMaxLength(256);
		builder.Property(x => x.Email).IsRequired().HasMaxLength(256);

		builder.HasMany(e => e.Supervisors)
			.WithOne(es => es.Employee)
			.HasForeignKey(es => es.EmployeeId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasMany(e => e.Subordinates)
			.WithOne(es => es.Supervisor)
			.HasForeignKey(es => es.SupervisorId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}