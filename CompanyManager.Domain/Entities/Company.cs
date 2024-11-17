using CompanyManager.Domain.Common;
using CompanyManager.Domain.EntityHelpers;
using CompanyManager.Domain.Errors;

namespace CompanyManager.Domain.Entities;

public class Company : AuditableEntity
{
	private List<LeaveApplication> _leaveApplications = new();
	private List<Employee> _employees = new();
	
	public string CompanyName { get; private set; } = null!;
	public IReadOnlyCollection<LeaveApplication> LeaveApplications => _leaveApplications;
	public IReadOnlyCollection<Employee> Employees => _employees;
	
	private Company()
	{
	}
	
	private Company(string companyName)
	{
		CompanyName = companyName;
	}
	
	public static Result<Company> Create(string companyName, CompanyOwner companyOwner)
	{
		if (string.IsNullOrWhiteSpace(companyName))
			return Result.Failure<Company>(DomainErrors.Company.CompanyNameCannotBeEmpty);
		
		var company = new Company(companyName);
		
		var owner = Employee.CreateCompanyOwner(companyOwner.FirstName, companyOwner.LastName,
			companyOwner.UserName, companyOwner.Email, company);
		
		if (owner.IsFailure)
			return Result.Failure<Company>(owner.Error);
		
		company.AddEmployee(owner.Value);
		
		return company;
	}
	
	public Result AddEmployee(Employee employee)
	{
		// if (_employees.Contains(employee))
		// 	return Result.Failure(DomainErrors.Company.EmployeeAlreadyExists);
		
		_employees.Add(employee);
		
		return Result.Success();
	}
}