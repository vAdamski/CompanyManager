using CompanyManager.Domain.Common;
using CompanyManager.Domain.Enums;
using CompanyManager.Domain.Errors;

namespace CompanyManager.Domain.Entities;

public class LeaveApplication : AuditableEntity
{
	private List<LeaveApplicationComment> _comments = new();
	
	public Guid EmployeeId { get; private set; }
	public Employee? Employee { get; private set; }

	public DateOnly FreeFrom { get; private set; }
	public DateOnly FreeTo { get; private set; }
	public int WorkDaysCount { get; private set; }
	public IReadOnlyCollection<LeaveApplicationComment> Comments => _comments;
	public LeaveApplicationType LeaveApplicationType { get; private set; }
	public LeaveApplicationStatus LeaveApplicationStatus { get; private set; }
	
	private LeaveApplication()
	{
	}

	private LeaveApplication(Employee employee, DateOnly freeFrom, DateOnly freeTo, int workDaysCount, LeaveApplicationType leaveApplicationType)
	{
		EmployeeId = employee.Id;
		Employee = employee;
		FreeFrom = freeFrom;
		FreeTo = freeTo;
		WorkDaysCount = workDaysCount;
		LeaveApplicationType = leaveApplicationType;
		LeaveApplicationStatus = LeaveApplicationStatus.Pending;
	}

	public static Result<LeaveApplication> Create(Employee employee, DateOnly freeFrom, DateOnly freeTo, int workDaysCount,
		LeaveApplicationType type)
	{
		if (freeFrom >= freeTo)
			return Result.Failure<LeaveApplication>(DomainErrors.LeaveApplication.StartDateMustBeBeforeEndDate);
		
		if (workDaysCount <= 0)
			return Result.Failure<LeaveApplication>(DomainErrors.LeaveApplication.WorkDaysCountMustBeGreaterThanZero);

		return new LeaveApplication(employee, freeFrom, freeTo, workDaysCount, type);
	}

	public Result<LeaveApplicationComment> AddComment(string comment)
	{
		if (string.IsNullOrWhiteSpace(comment))
		{
			return Result.Failure<LeaveApplicationComment>(
				DomainErrors.LeaveApplicationComment.CommentBodyCannotBeEmpty);
		}

		LeaveApplicationComment leaveApplicationComment = new LeaveApplicationComment
		{
			Comment = comment,
			LeaveApplicationId = Id,
			LeaveApplication = this
		};

		_comments.Add(leaveApplicationComment);

		return Result.Success(leaveApplicationComment);
	}
	
	public Result Accept()
	{
		if (LeaveApplicationStatus != LeaveApplicationStatus.Pending)
			return Result.Failure(DomainErrors.LeaveApplication.OnlyPendingLeaveApplicationsCanBeApproved);

		LeaveApplicationStatus = LeaveApplicationStatus.Approved;
		
		return Result.Success();
	}
	
	public Result Reject()
	{
		if (LeaveApplicationStatus != LeaveApplicationStatus.Pending)
			return Result.Failure(DomainErrors.LeaveApplication.OnlyPendingLeaveApplicationsCanBeRejected);

		LeaveApplicationStatus = LeaveApplicationStatus.Rejected;
		
		return Result.Success();
	}
}