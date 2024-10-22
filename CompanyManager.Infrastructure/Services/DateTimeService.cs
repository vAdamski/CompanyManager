using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;

namespace CompanyManager.Infrastructure.Services;

public class DateTimeService : IDateTime
{
	public DateTime Now => DateTime.Now;
}