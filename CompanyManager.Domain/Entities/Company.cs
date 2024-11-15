using CompanyManager.Domain.Common;
using CompanyManager.Domain.Enums;
using CompanyManager.Domain.Errors;

namespace CompanyManager.Domain.Entities;

public class Company : AuditableEntity
{
	private List<LeaveApplication> _leaveApplications = new();
	
	public string CompanyName { get; private set; } = null!;
	public IReadOnlyCollection<LeaveApplication> LeaveApplications => _leaveApplications;
	
	private Company()
	{
	}
	
	private Company(string companyName)
	{
		CompanyName = companyName;
	}
	
	public static Result<Company> Create(string companyName)
	{
		if (string.IsNullOrWhiteSpace(companyName))
			return Result.Failure<Company>(DomainErrors.Company.CompanyNameCannotBeEmpty);
		
		return new Company(companyName);
	}
	
	public Result<LeaveApplication> AddLeaveApplication(DateOnly freeFrom, DateOnly freeTo, int workDaysCount,
		LeaveApplicationType type)
	{
		Result<LeaveApplication> leaveApplicationResult = LeaveApplication.Create(this, freeFrom, freeTo, workDaysCount, type);
		
		if (leaveApplicationResult.IsFailure)
			return Result.Failure<LeaveApplication>(leaveApplicationResult.Error);
		
		LeaveApplication leaveApplication = leaveApplicationResult.Value;
		_leaveApplications.Add(leaveApplication);
		
		return Result.Success(leaveApplication);
	}
}