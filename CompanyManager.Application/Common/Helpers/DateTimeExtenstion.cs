namespace CompanyManager.Application.Common.Helpers;

public static class DateTimeExtenstion
{
	public static DateTime ToDateTime(this DateOnly date)
	{
		return new DateTime(date.Year, date.Month, date.Day);
	}
}