using CompanyManager.Application.Common.Helpers;
using CompanyManager.Domain.Entities;

namespace CompanyManager.Application.Common.Services;

public class WorkExperienceCalculator
{
	/// <summary>
	/// Calculates the total number of years worked by an employee based on the list of contracts and schools attended.
	/// </summary>
	/// <param name="contracts">List of employment contracts.</param>
	/// <param name="schools">List of schools attended.</param>
	/// <returns>Total number of years worked.</returns>
	public int CalculateTotalYearsOfWork(List<EmployeeEmploymentContract> contracts, List<EmployeeSchool> schools)
	{
		int workYears = CalculateWorkYearsByContract(contracts);
		
		int schoolAdditionalYearsExperience = CalculateSchoolAdditionalYearsExperience(schools);
		
		return workYears + schoolAdditionalYearsExperience;
	}
	
	private int CalculateWorkYearsByContract(List<EmployeeEmploymentContract> contracts)
	{
		int totalDaysWorked = 0;

		foreach (var contract in contracts)
		{
			// If the contract is still active, use the current date as the end date
			DateOnly endDate = contract.EndDate ?? DateOnlyExtension.Now;
			
			if (endDate < contract.StartDate)
			{
				throw new ArgumentException("End date cannot be earlier than start date!");
			}

			// Calculate the total number of days worked
			totalDaysWorked += CalculateDaysBetweenDates(contract.StartDate, endDate);
		}

		return totalDaysWorked / 365;
	}

	private int CalculateSchoolAdditionalYearsExperience(List<EmployeeSchool> schools)
	{
		return schools.Select(x => x.YearsIncluded).Max();
	}

	private int CalculateDaysBetweenDates(DateOnly startDate, DateOnly endDate)
	{
		return (int)(endDate.ToDateTime() - startDate.ToDateTime()).TotalDays;
	}
}