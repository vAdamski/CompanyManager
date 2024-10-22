using CompanyManager.Domain.Common;

namespace CompanyManager.Domain.Errors;

public static class DomainErrors
{
	public static class User
	{
		public static readonly Error EmailAlreadyInUse = new(
			"User.EmailAlreadyInUse",
			"The provided email is already in use.");
		
		public static readonly Error RegisterFailed = new(
			"User.RegisterFailed",
			"Failed to register user.");
	}
}