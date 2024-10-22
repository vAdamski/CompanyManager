using CompanyManager.Shared.Common;
using MediatR;

namespace CompanyManager.Application.Common.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse>
	: IRequestHandler<TQuery, Result<TResponse>>
	where TQuery : IQuery<TResponse>
{
}