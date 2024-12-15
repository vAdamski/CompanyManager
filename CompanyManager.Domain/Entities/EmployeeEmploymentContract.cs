using CompanyManager.Domain.Common;

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
	
	public static EmployeeEmploymentContract Create(string companyName, DateOnly startDate, DateOnly? endDate, Employee employee)
	{
		return new EmployeeEmploymentContract(companyName, startDate, endDate, employee);
	}
}