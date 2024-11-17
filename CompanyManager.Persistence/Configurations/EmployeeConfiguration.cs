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
		
		builder.HasOne(x => x.Company)
			.WithMany(x => x.Employees)
			.HasForeignKey(x => x.CompanyId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}