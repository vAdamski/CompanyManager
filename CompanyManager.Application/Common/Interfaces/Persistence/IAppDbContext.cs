using CompanyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CompanyManager.Application.Common.Interfaces.Persistence;

public interface IAppDbContext
{
	DbSet<Company> Companies { get; set; }
	DbSet<LeaveApplication> LeaveApplications { get; set; }
	DbSet<LeaveApplicationComment> LeaveApplicationComments { get; set; }
	DbSet<Employee> Employees { get; set; }
	DbSet<EmployeeSupervisor> EmployeeSupervisors { get; set; }
	DbSet<EmployeeEmploymentContract> EmployeeEmploymentContracts { get; set; }
	DbSet<EmployeeSchool> EmployeeSchools { get; set; }
	
	DatabaseFacade Database { get; }
	
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
	void Dispose();
}