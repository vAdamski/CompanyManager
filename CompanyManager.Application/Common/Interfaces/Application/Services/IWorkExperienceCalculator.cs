using CompanyManager.Domain.Entities;

namespace CompanyManager.Application.Common.Interfaces.Application.Services;

public interface IWorkExperienceCalculator
{
	int CalculateTotalYearsOfWork(List<EmployeeEmploymentContract> contracts, List<EmployeeSchool> schools);
}