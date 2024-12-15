using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Persistence.Configurations;

public class LeaveApplicationConfiguration : IEntityTypeConfiguration<LeaveApplication>
{
	public void Configure(EntityTypeBuilder<LeaveApplication> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedNever();
		builder.Property(x => x.EmployeeId).IsRequired();
		builder.Property(x => x.FreeFrom).IsRequired();
		builder.Property(x => x.FreeTo).IsRequired();
		builder.Property(x => x.WorkDaysCount).IsRequired();
		builder.Property(x => x.LeaveApplicationType).IsRequired();
		builder.Property(x => x.LeaveApplicationStatus).IsRequired();
		
		builder.HasMany(x => x.Comments)
			.WithOne(x => x.LeaveApplication)
			.HasForeignKey(x => x.LeaveApplicationId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}