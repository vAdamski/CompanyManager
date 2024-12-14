namespace CompanyManager.Application.Actions.CompanyActions.Queries.GetCompanyDetailsForCurrentUser;

public class CompanyDetailsViewModel(Guid id, string companyName)
{
	public Guid Id { get; set; } = id;
	public string CompanyName { get; set; } = companyName;
}