using CompanyManager.Application.Common.Interfaces.Application.Helpers;
using Microsoft.Extensions.Configuration;

namespace CompanyManager.Infrastructure.Helpers;

public class VariablesGetter(IConfiguration configuration) : IVariablesGetter
{
	public string GetVariable(string appsettingsKey, string environmentVariableKey)
	{
		var environmentVariable = Environment.GetEnvironmentVariable(environmentVariableKey);
		
		if (!string.IsNullOrEmpty(environmentVariable))
		{
			return environmentVariable;
		}

		return configuration[appsettingsKey];
	}
}