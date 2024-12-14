using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.EmployeeActions.Commands.UpdateCompanyEmployee;

public class UpdateCompanyEmployeeCommandHandler(IAppDbContext ctx, IUnitOfWork unitOfWork)
	: ICommandHandler<UpdateCompanyEmployeeCommand>
{
	public async Task<Result> Handle(UpdateCompanyEmployeeCommand request,
		CancellationToken cancellationToken)
	{
		await unitOfWork.BeginTransactionAsync();
		
		var employee = await ctx.Employees
			.Include(x => x.Supervisors)
			.FirstOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken);

		if (employee == null)
			return Result.Failure(DomainErrors.Employee.EmployeeNotFound);

		var result = employee.UpdateEmployee(request.FirstName, request.LastName, request.UserName, request.Email,
			request.Supervisors);

		if (result.IsFailure)
		{
			await unitOfWork.RollbackTransactionAsync();
			return Result.Failure(result.Error);
		}
		
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}