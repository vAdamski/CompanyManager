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
}