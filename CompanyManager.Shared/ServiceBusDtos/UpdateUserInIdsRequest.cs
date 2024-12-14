using CompanyManager.Domain.Events;

namespace CompanyManager.Shared.ServiceBusDtos;

public class UpdateUserInIdsRequest
{
	public UpdateUserInIdsRequest()
	{
		
	}

	public UpdateUserInIdsRequest(UpdateEmployeeInIdsEvent notification)
	{
		FirstName = notification.FirstName;
		LastName = notification.LastName;
		UserName = notification.UserName;
		Email = notification.Email;
	}

	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
}