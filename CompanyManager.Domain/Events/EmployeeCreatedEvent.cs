using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Primitives;

namespace CompanyManager.Domain.Events;

public record EmployeeCreatedEvent(Guid id, Employee employee) : DomainEvent(id);