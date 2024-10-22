using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Persistence;

public class BudgetPlanDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
{
	protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
	{
		return new AppDbContext(options);
	}
}