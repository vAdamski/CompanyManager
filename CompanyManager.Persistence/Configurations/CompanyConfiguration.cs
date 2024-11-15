using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedNever();
		builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(256);
		
		builder.HasMany(x => x.LeaveApplications)
			.WithOne(x => x.Company)
			.HasForeignKey(x => x.CompanyId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}