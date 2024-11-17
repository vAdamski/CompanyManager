using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.EntityHelpers;

namespace CompanyManager.Application.Actions.CompanyActions.Commands.CreateCompany;

public class CreateCompanyCommandHandler(IAppDbContext ctx, IUnitOfWork unitOfWork)
	: ICommandHandler<CreateCompanyCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
	{
		var result = Company.Create(
			request.CompanyName,
			new CompanyOwner(
				request.CompanyOwnerFirstName,
				request.CompanyOwnerLastName,
				request.CompanyOwnerEmail,
				request.CompanyOwnerUserName
			)
		);

		if (result.IsFailure)
			return Result.Failure<Guid>(result.Error);
		
		await ctx.Companies.AddAsync(result.Value, cancellationToken);
		
		await unitOfWork.SaveChangesAsync(cancellationToken);
		
		return Result.Success(result.Value.Id);
	}
}