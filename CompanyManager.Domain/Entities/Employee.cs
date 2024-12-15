using CompanyManager.Domain.Common;
using CompanyManager.Domain.Events;

namespace CompanyManager.Domain.Entities;

public class Employee : AuditableEntity
{
	private List<EmployeeSupervisor> _supervisors = new();
	private List<EmployeeSupervisor> _subordinates = new();
	private List<EmployeeEmploymentContract> _employmentContracts = new();

	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string UserName { get; private set; }
	public string Email { get; private set; }
	public Guid CompanyId { get; private set; }
	public Company? Company { get; private set; }
	public IReadOnlyCollection<EmployeeSupervisor> Supervisors => _supervisors.AsReadOnly();
	public IReadOnlyCollection<EmployeeSupervisor> Subordinates => _subordinates.AsReadOnly();
	public IReadOnlyCollection<EmployeeEmploymentContract> EmploymentContracts => _employmentContracts.AsReadOnly();

	private Employee()
	{
	}

	private Employee(string firstName, string lastName, string userName, string email, Company company,
		List<Employee> supervisors)
	{
		FirstName = firstName;
		LastName = lastName;
		UserName = userName;
		Email = email;
		CompanyId = company.Id;
		Company = company;
		_supervisors = supervisors.Select(s => EmployeeSupervisor.Create(this, s).Value).ToList();
	}

	public static Result<Employee> Create(string firstName, string lastName, string userName, string email,
		List<Employee>? listOfSupervisors, Company company)
	{
		return CreateEmployee(
			firstName,
			lastName,
			userName,
			email,
			listOfSupervisors,
			company,
			new List<string> { "User" });
	}

	public static Result<Employee> CreateCompanyOwner(string firstName, string lastName, string userName, string email,
		Company company)
	{
		return CreateEmployee(firstName, lastName, userName, email, null, company,
			new List<string> { "User", "CompanyOwner" });
	}

	private static Result<Employee> CreateEmployee(string firstName, string lastName, string userName, string email,
		List<Employee>? listOfSupervisors,
		Company company, List<string>? roles)
	{
		var employee = new Employee(firstName, lastName, userName, email, company, new List<Employee>());
		
		listOfSupervisors?.ForEach(s => employee.AddSupervisor(s));
		
		var eventId = Guid.NewGuid();
		var claims = new List<string>();

		employee.Raise(new CreateEmployeeInIdsEvent(eventId, employee, roles, claims));

		return employee;
	}

	public Result AddSupervisor(Employee supervisor)
	{
		var result = EmployeeSupervisor.Create(this, supervisor);
		if (result.IsFailure)
			return Result.Failure(result.Error);

		_supervisors.Add(result.Value);

		return Result.Success();
	}

	public Result UpdateEmployee(string firstName, string lastName, string userName, string email, List<Guid> supervisors)
	{
		FirstName = firstName;
		LastName = lastName;
		UserName = userName;
		// Email is not updated, it is needed to find user in IDS
		
		_supervisors = supervisors.Select(s => EmployeeSupervisor.Create(Id, s).Value).ToList();
		
		Raise(new UpdateEmployeeInIdsEvent(Guid.NewGuid(), firstName, lastName, userName, email));
		
		return Result.Success();
	}
	
	public Result<EmployeeEmploymentContract> AddEmploymentContract(string companyName, DateOnly startDate, DateOnly? endDate)
	{
		var result = EmployeeEmploymentContract.Create(companyName, startDate, endDate, this);
		
		if (result.IsFailure)
			return Result.Failure<EmployeeEmploymentContract>(result.Error);
		
		_employmentContracts.Add(result.Value);
		
		return result;
	}
}