using System.Security.Claims;
using CompanyManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.Application.Common.Interfaces.Application.Services;

public interface IJwtTokenGenerator
{
	Task<string> GenerateToken(ApplicationUser user, List<string> roles = null, List<Claim> claims = null);
}