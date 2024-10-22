using CompanyManager.Shared.Common;
using MediatR;

namespace CompanyManager.Application.Common.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}