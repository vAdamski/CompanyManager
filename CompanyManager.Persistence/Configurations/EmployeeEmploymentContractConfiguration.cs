using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Persistence.Configurations;

public class EmployeeEmploymentContractConfiguration : IEntityTypeConfiguration<EmployeeEmploymentContract>
{
	public void Configure(EntityTypeBuilder<EmployeeEmploymentContract> builder)
	{
		builder.HasKey(eec => eec.Id);
		builder.Property(x => x.Id).ValueGeneratedNever();
		
		builder.Property(eec => eec.CompanyName).IsRequired();
		builder.Property(eec => eec.StartDate).IsRequired();
		builder.Property(eec => eec.EndDate).IsRequired(false);
	}
}