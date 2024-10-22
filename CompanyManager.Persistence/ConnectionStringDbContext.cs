namespace CompanyManager.Persistence;

public static class ConnectionStringDbContext
{
	public static string GetConnectionString()
	{
		var connectionString = "Server=localhost,1433;Initial Catalog=CompanyManagerDatabase;Persist Security Info=False;User ID=sa;Password=Pass1234$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
		return connectionString;
	}
}