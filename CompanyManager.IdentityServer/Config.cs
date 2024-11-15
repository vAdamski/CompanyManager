using Duende.IdentityServer.Models;

namespace CompanyManager.IdentityServer;

public static class Config
{
	public static IEnumerable<IdentityResource> IdentityResources =>
		new IdentityResource[]
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile(),
		};

	public static IEnumerable<ApiScope> ApiScopes =>
		new ApiScope[]
		{
			new ApiScope("api1")
		};

	public static IEnumerable<Client> Clients =>
		new Client[]
		{
			new Client
			{
				ClientId = "swagger",
				ClientName = "Client for Swagger use",

				AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
				ClientSecrets = { new Secret("secret".Sha256()) },
				AllowedScopes =
				{
					"api1",
					"openid",
					"profile"
				},
				AlwaysSendClientClaims = true,
				AlwaysIncludeUserClaimsInIdToken = true,
				AllowAccessTokensViaBrowser = true,
				RedirectUris = { "https://localhost:5001/swagger/oauth2-redirect.html" },
				AllowedCorsOrigins = { "https://localhost:5001" },
				Enabled = true
			}
		};
}