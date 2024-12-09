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
		
		var result = company.AddEmployee(owner.Value);
		
		if (result.IsFailure)
			return Result.Failure<Company>(result.Error);
		
		return company;
	}
	
	public Result<Employee> AddEmployee(Employee employee)
	{
		_employees.Add(employee);
		
		return Result.Success(employee);
	}
	
	public Result<Employee> CreateEmployee(string firstName, string lastName, string userName, string email,
		List<Employee>? listOfSupervisors)
	{
		var employeeResult = Employee.Create(firstName, lastName, userName, email,listOfSupervisors, this);
		if (employeeResult.IsFailure)
			return Result.Failure<Employee>(employeeResult.Error);

		var addEmployeeResult = AddEmployee(employeeResult.Value);
		if (addEmployeeResult.IsFailure)
			return Result.Failure<Employee>(addEmployeeResult.Error);

		return Result.Success(employeeResult.Value);
	}
}