using CompanyManager.Domain.Primitives;

namespace CompanyManager.Domain.Events;

public record UpdateEmployeeInIdsEvent(Guid Id, string FirstName, string LastName, string UserName, string Email) : DomainEvent(Id);