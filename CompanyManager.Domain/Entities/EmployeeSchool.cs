using CompanyManager.Domain.Common;
using CompanyManager.Domain.Enums;
using CompanyManager.Domain.Errors;

namespace CompanyManager.Domain.Entities;

public class EmployeeSchool : AuditableEntity
{
	public Guid EmployeeId { get; private set; } // Klucz obcy do encji Employee
	public Employee Employee { get; private set; } // Nawigacja do pracownika
	public string SchoolName { get; private set; } // Nazwa szkoły
	public DateOnly StartDate { get; private set; } // Data rozpoczęcia
	public DateOnly? EndDate { get; private set; } // Data zakończenia
	public SchoolType Type { get; private set; } // Rodzaj szkoły
	public int YearsIncluded { get; private set; } // Wliczane lata

	private EmployeeSchool()
	{
		
	}

	private EmployeeSchool(Employee employee, string schoolName, DateOnly startDate, DateOnly? endDate, SchoolType type,
		int yearsIncluded)
	{
		EmployeeId = employee.Id;
		Employee = employee;
		SchoolName = schoolName;
		StartDate = startDate;
		EndDate = endDate;
		Type = type;
		YearsIncluded = yearsIncluded;
	}

	public static Result<EmployeeSchool> Create(Employee employee, string schoolName, DateOnly startDate, DateOnly? endDate,
		SchoolType type)
	{
		if (endDate.HasValue && startDate > endDate)
			return Result.Failure<EmployeeSchool>(DomainErrors.EmployeeSchool.StartDateCannotBeLaterThanEndDate);
		
		int yearsIncluded = type switch
		{
			SchoolType.BasicVocational => 3,
			SchoolType.SecondaryVocational => 5,
			SchoolType.SecondaryVocationalForGraduates => 5,
			SchoolType.GeneralSecondary => 4,
			SchoolType.PostSecondary => 6,
			SchoolType.HigherEducation => 8,
			_ => 0
		};
		
		return Result.Success(new EmployeeSchool(employee, schoolName, startDate, endDate, type, yearsIncluded));
	}
		
}