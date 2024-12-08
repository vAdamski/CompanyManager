using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Primitives;

namespace CompanyManager.Domain.Events;

public record CreateEmployeeInIdsEvent(Guid Id, Employee Employee, List<string>? Roles, List<string> Claims)
	: DomainEvent(Id);