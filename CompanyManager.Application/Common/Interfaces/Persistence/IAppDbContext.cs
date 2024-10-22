using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CompanyManager.Application.Common.Interfaces.Persistence;

public interface IAppDbContext
{
	DatabaseFacade Database { get; }
	
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
	void Dispose();
}