namespace CompanyManager.Application.Common.Helpers;

public static class DateOnlyExtension
{
	public static DateOnly Now => DateOnly.FromDateTime(DateTime.Now);
}