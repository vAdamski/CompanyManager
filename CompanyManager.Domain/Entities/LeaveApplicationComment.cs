using CompanyManager.Domain.Common;

namespace CompanyManager.Domain.Entities;

public class LeaveApplicationComment : AuditableEntity
{
	public string Comment { get; set; } = null!;
	
	public Guid LeaveApplicationId { get; set; }
	public LeaveApplication? LeaveApplication { get; set; } = null!;
}