using CompanyManager.Domain.Common;

namespace CompanyManager.Domain.Entities;

public class EmployeeSupervisor
{
    private EmployeeSupervisor() { }
    
    private EmployeeSupervisor(Employee employee, Employee supervisor)
    {
        EmployeeId = employee.Id;
        Employee = employee;
        SupervisorId = supervisor.Id;
        Supervisor = supervisor;
    }

    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; }

    public Guid SupervisorId { get; private set; }
    public Employee Supervisor { get; private set; }
    
    public static Result<EmployeeSupervisor> Create(Employee employee, Employee supervisor)
    {
        return Result.Success(new EmployeeSupervisor(employee, supervisor));
    }
}