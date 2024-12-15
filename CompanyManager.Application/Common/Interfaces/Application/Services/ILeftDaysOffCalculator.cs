namespace CompanyManager.Application.Common.Interfaces.Application.Services;

public interface ILeftDaysOffCalculator
{
	int CalculateRemainingVacationDays(DateOnly employmentStartDate, double workFraction, int yearsOfWork,
		int usedVacationDays);
}