using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Enums;

namespace CompanyManager.Application.Common.Services;

public class EmployeeVacationService(
	ILeftDaysOffCalculator leftDaysOffCalculator,
	IWorkExperienceCalculator workExperienceCalculator)
	: IEmployeeVacationService
{
	public int CalculateLeftDaysOff(Employee employee)
	{
		DateOnly employmentStartDate = employee.EmploymentContracts.Where(x => x.EndDate == null)
			.Select(x => x.StartDate).FirstOrDefault();

		double workFraction = 1.0;

		var workExperience = workExperienceCalculator.CalculateTotalYearsOfWork(
			employee.EmploymentContracts.ToList(),
			employee.Schools.ToList());

		var usedVacationDays = employee.LeaveApplications
			.Where(x => x.LeaveApplicationStatus == LeaveApplicationStatus.Approved)
			.Select(x => x.WorkDaysCount)
			.Sum();

		return leftDaysOffCalculator.CalculateRemainingVacationDays(
			employmentStartDate, workFraction, workExperience, usedVacationDays);
	}
}