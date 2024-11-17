namespace CompanyManager.Application.Common.Interfaces.Application.Helpers;

public interface IVariablesGetter
{
	string GetVariable(string appsettingsKey, string environmentVariableKey);
}