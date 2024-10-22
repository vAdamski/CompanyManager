using CompanyManager.Application.Common.Interfaces.Api.Services;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CompanyManager.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
{
	private readonly IDateTime _dateTime;
	private readonly ICurrentUserService _userService;

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public AppDbContext(DbContextOptions<AppDbContext> options, IDateTime dateTime,
		ICurrentUserService userService) : base(options)
	{
		_dateTime = dateTime;
		_userService = userService;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
	
	public DatabaseFacade Database => base.Database;
	
	public void Dispose()
	{
		base.Dispose();
	}
	
	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
		{
			switch (entry.State)
			{
				case EntityState.Deleted:
					entry.Entity.ModifiedBy = _userService.Email;
					entry.Entity.Modified = _dateTime.Now;
					entry.Entity.Inactivated = _dateTime.Now;
					entry.Entity.InactivatedBy = _userService.Email;
					entry.Entity.Status = AuditableEntityStatus.Inactive;
					entry.State = EntityState.Modified;
					break;
				case EntityState.Modified:
					entry.Entity.ModifiedBy = _userService.Email;
					entry.Entity.Modified = _dateTime.Now;
					break;
				case EntityState.Added:
					entry.Entity.CreatedBy = _userService.Email;
					entry.Entity.Created = _dateTime.Now;
					entry.Entity.Status = AuditableEntityStatus.Active;
					break;
				default:
					break;
			}
		}

		foreach (var entry in ChangeTracker.Entries<ValueObject>())
		{
			switch (entry.State)
			{
				case EntityState.Deleted:
					entry.State = EntityState.Modified;
					break;
				default:
					break;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}