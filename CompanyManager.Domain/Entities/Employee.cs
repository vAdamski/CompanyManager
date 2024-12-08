using CompanyManager.Domain.Common;
using CompanyManager.Domain.Events;

namespace CompanyManager.Domain.Entities;

public class Employee : AuditableEntity
{
	private List<EmployeeSupervisor> _supervisors = new();
	private List<EmployeeSupervisor> _subordinates = new();

	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string UserName { get; private set; }
	public string Email { get; private set; }

	public Guid CompanyId { get; private set; }
	public Company? Company { get; private set; }

	public IReadOnlyCollection<EmployeeSupervisor> Supervisors => _supervisors.AsReadOnly();
	public IReadOnlyCollection<EmployeeSupervisor> Subordinates => _subordinates.AsReadOnly();

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
		_supervisors = supervisors.Select(s => EmployeeSupervisor.Create(this, s)).ToList();
	}

	public static Result<Employee> Create(string firstName, string lastName, string userName, string email,
		Company company)
	{
		return CreateEmployee(firstName, lastName, userName, email, company, new List<string> { "User" });
	}

	public static Result<Employee> CreateCompanyOwner(string firstName, string lastName, string userName, string email,
		Company company)
	{
		return CreateEmployee(firstName, lastName, userName, email, company,
			new List<string> { "User", "CompanyOwner" });
	}

	private static Result<Employee> CreateEmployee(string firstName, string lastName, string userName, string email,
		Company company, List<string>? roles)
	{
		var employee = new Employee(firstName, lastName, userName, email, company, new List<Employee>());
		var eventId = Guid.NewGuid();
		var claims = new List<string>();

		employee.Raise(new CreateEmployeeInIdsEvent(eventId, employee, roles, claims));

		return employee;
	}
}