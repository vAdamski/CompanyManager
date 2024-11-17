using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Primitives;

namespace CompanyManager.Domain.Events;

public record CreateEmployeeInIdsEvent(Guid id, Employee employee, List<string> roles, List<string> claims)
	: DomainEvent(id);