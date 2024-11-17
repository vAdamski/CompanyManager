using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Persistence.Configurations;

public class EmployeeSupervisorConfiguration : IEntityTypeConfiguration<EmployeeSupervisor>
{
	public void Configure(EntityTypeBuilder<EmployeeSupervisor> builder)
	{
		builder
			.HasKey(es => new { es.EmployeeId, es.SupervisorId });

		builder
			.HasOne(es => es.Employee)
			.WithMany(e => e.Supervisors)
			.HasForeignKey(es => es.EmployeeId);

		builder
			.HasOne(es => es.Supervisor)
			.WithMany(e => e.Subordinates)
			.HasForeignKey(es => es.SupervisorId);
	}
}