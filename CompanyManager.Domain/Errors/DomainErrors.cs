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

		public static readonly Error UserNotFound = new(
			"User.UserNotFound",
			"User not found.");

		public static readonly Error InvalidPassword = new(
			"User.InvalidPassword",
			"Invalid password.");
	}

	public static class Role
	{
		public static readonly Error NotFound = new(
			"Role.NotFound",
			"Role not found.");
	}
}