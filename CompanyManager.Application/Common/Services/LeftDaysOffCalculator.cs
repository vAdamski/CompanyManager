namespace CompanyManager.Application.Common.Services;

public class LeftDaysOffCalculator
{
	private const int MinimumVacationDays = 20;
	private const int MaximumVacationDays = 26;
	
	public int CalculateRemainingVacationDays(DateOnly employmentStartDate, double workFraction, int yearsOfWork, int usedVacationDays)
	{
		// Claculate the number of vacation days allowed
		int allowedVacationDays = yearsOfWork >= 10 ? MaximumVacationDays : MinimumVacationDays;
        
		// Handle the case when the employee works part-time
		allowedVacationDays = (int)(allowedVacationDays * workFraction);

		// Calculate proportional vacation for new employees
		int workedMonths = DateTime.Now.Year == employmentStartDate.Year
			? DateTime.Now.Month - employmentStartDate.Month + 1
			: 12;
        
		if (workedMonths < 12)
		{
			allowedVacationDays = (allowedVacationDays * workedMonths) / 12;
		}

		// Calculate the remaining vacation days
		int remainingVacationDays = allowedVacationDays - usedVacationDays;
		
		return Math.Max(remainingVacationDays, 0);
	}
}