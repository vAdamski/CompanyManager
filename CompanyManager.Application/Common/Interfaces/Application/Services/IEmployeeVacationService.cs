using CompanyManager.Domain.Entities;

namespace CompanyManager.Application.Common.Interfaces.Application.Services;

public interface IEmployeeVacationService
{
	int CalculateLeftDaysOff(Employee employee);
}