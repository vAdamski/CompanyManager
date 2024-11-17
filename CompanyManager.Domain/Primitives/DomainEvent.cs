using MediatR;

namespace CompanyManager.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;