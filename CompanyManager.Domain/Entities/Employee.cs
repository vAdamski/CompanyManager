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

	private Employee() { }

	private Employee(string firstName, string lastName, string userName, string email, Company company)
	{
		FirstName = firstName;
		LastName = lastName;
		UserName = userName;
		Email = email;
		CompanyId = company.Id;
		Company = company;
	}

	public static Employee Create(string firstName, string lastName, string userName, string email, Company company)
	{
		var employee = new Employee(firstName, lastName, userName, email, company);
		employee.Raise(new EmployeeCreatedEvent(Guid.NewGuid(), employee));
		return employee;
	}
}