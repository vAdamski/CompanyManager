namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployee;

public class EmployeeSupervisorDetailsDto(Guid id, string firstName, string lastName)
{
	public Guid Id { get; } = id;
	public string FullName => $"{firstName} {lastName}";
}