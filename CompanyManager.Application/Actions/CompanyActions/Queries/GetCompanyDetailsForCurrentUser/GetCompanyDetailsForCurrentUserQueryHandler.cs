using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Api.Services;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.CompanyActions.Queries.GetCompanyDetailsForCurrentUser;

public class GetCompanyDetailsForCurrentUserQueryHandler(ICurrentUserService currentUserService, IAppDbContext ctx)
	: IQueryHandler<GetCompanyDetailsForCurrentUserQuery,
		CompanyDetailsViewModel>
{
	public async Task<Result<CompanyDetailsViewModel>> Handle(GetCompanyDetailsForCurrentUserQuery request,
		CancellationToken cancellationToken)
	{
		var userEmial = currentUserService.Email;
		
		var user = await ctx.Employees
			.Include(x => x.Company)
			.FirstOrDefaultAsync(x => x.Email == userEmial, cancellationToken);
		
		if (user == null)
			Result.Failure<CompanyDetailsViewModel>(DomainErrors.Employee.EmployeeNotFound);
		
		var company = user.Company;
		
		if (company == null)
			Result.Failure<CompanyDetailsViewModel>(DomainErrors.Company.CompanyNotFound);
		
		return Result.Success(new CompanyDetailsViewModel(company.Id, company.CompanyName));
	}
}