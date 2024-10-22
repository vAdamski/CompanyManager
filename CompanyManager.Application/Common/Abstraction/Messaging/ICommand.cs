using CompanyManager.Shared.Common;
using MediatR;

namespace CompanyManager.Application.Common.Abstraction.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}