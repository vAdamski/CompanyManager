using System.Text.Json;
using CompanyManager.Domain.Events;

namespace CompanyManager.Shared.ServiceBusDtos;

public class CreateUserInIdsRequest
{
	public CreateUserInIdsRequest()
	{
		
	}
	
	public CreateUserInIdsRequest(CreateEmployeeInIdsEvent notification)
	{
		FirstName = notification.Employee.FirstName;
		LastName = notification.Employee.LastName;
		Email = notification.Employee.Email;
		UserName = notification.Employee.UserName;
		Roles = notification.Roles;
		Claims = notification.Claims;
	}

	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string UserName { get; set; }
	public List<string> Roles { get; set; }
	public List<string> Claims { get; set; }
	
	public string ToJson()
	{
		return JsonSerializer.Serialize(this);
	}
}