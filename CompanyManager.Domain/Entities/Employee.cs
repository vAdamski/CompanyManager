using CompanyManager.Domain.Common;
using CompanyManager.Domain.Events;

namespace CompanyManager.Domain.Entities;

public class Employee : AuditableEntity
{
	private List<Employee> _superiors = new();
	
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string UserName { get; private set; }
	public string Email { get; private set; }

	public Guid CompanyId { get; private set; }
	public Company? Company { get; private set; }
	
	public IReadOnlyCollection<Employee> Superiors => _superiors.AsReadOnly();

	private Employee()
	{
		
	}

	private Employee(string firstName, string lastName, string userName, string email, Company company, List<Employee> superiors)
	{
		FirstName = firstName;
		LastName = lastName;
		UserName = userName;
		Email = email;
		CompanyId = company.Id;
		Company = company;
		_superiors = superiors;
	}
	
	public static Employee Create(string firstName, string lastName, string userName, string email, Company company, List<Employee> superiors)
	{
		var employee = new Employee(firstName, lastName, userName, email, company, superiors);
		
		employee.Raise(new EmployeeCreatedEvent(Guid.NewGuid(), employee));
		
		return employee;
	}
}