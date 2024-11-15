using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Persistence.Configurations;

public class LeaveApplicationCommentConfiguration : IEntityTypeConfiguration<LeaveApplicationComment>
{
	public void Configure(EntityTypeBuilder<LeaveApplicationComment> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedNever();
		builder.Property(x => x.Comment).IsRequired();
		builder.Property(x => x.LeaveApplicationId).IsRequired();
	}
}