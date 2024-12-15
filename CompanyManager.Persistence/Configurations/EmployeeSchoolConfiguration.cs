using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Persistence.Configurations;

public class EmployeeSchoolConfiguration : IEntityTypeConfiguration<EmployeeSchool>
{
	public void Configure(EntityTypeBuilder<EmployeeSchool> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedNever();
		builder.Property(x => x.SchoolName).IsRequired();
		builder.Property(x => x.StartDate).IsRequired();
		builder.Property(x => x.EndDate).IsRequired(false);
		builder.Property(x => x.Type).IsRequired();
		builder.Property(x => x.YearsIncluded).IsRequired();
	}
}