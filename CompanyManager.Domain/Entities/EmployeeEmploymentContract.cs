using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;

namespace CompanyManager.Domain.Entities;

public class EmployeeEmploymentContract : AuditableEntity
{
	private EmployeeEmploymentContract()
	{
		
	}
	
	private EmployeeEmploymentContract(string companyName, DateOnly startDate, DateOnly? endDate, Employee employee)
	{
		CompanyName = companyName;
		StartDate = startDate;
		EndDate = endDate;
		EmployeeId = employee.Id;
		Employee = employee;
	}
	
	public string CompanyName { get; private set; } = string.Empty;
	public DateOnly StartDate { get; private set; }
	public DateOnly? EndDate { get; private set; }
	
	public Guid EmployeeId { get; private set; }
	
	public Employee? Employee { get; private set; }
	
	public static Result<EmployeeEmploymentContract> Create(string companyName, DateOnly startDate, DateOnly? endDate, Employee employee)
	{
		if (string.IsNullOrWhiteSpace(companyName))
			return Result.Failure<EmployeeEmploymentContract>(DomainErrors.EmployeeEmploymentContract.CompanyNameCannotBeEmpty);
		
		if (endDate.HasValue && startDate > endDate)
			return Result.Failure<EmployeeEmploymentContract>(DomainErrors.EmployeeEmploymentContract.StartDateCannotBeLaterThanEndDate);
		
		return new EmployeeEmploymentContract(companyName, startDate, endDate, employee);
	}
}