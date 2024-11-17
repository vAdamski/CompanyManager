using System.Text.Json;
using CompanyManager.Domain.Events;

namespace CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;

public class CreateUserInIdsRequest(CreateEmployeeInIdsEvent notification)
{
	public string FirstName { get; init; } = notification.Employee.FirstName;
	public string LastName { get; init; } = notification.Employee.LastName;
	public string Email { get; init; } = notification.Employee.Email;
	public string UserName { get; init; } = notification.Employee.UserName;
	public List<string> Roles { get; init; } = notification.Roles;
	public List<string> Claims { get; init; } = notification.Claims;
	
	public string ToJson()
	{
		return JsonSerializer.Serialize(this);
	}
}