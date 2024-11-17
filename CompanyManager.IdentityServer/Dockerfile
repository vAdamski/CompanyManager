FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["CompanyManager.IdentityServer/CompanyManager.IdentityServer.csproj", "CompanyManager.IdentityServer/"]
RUN dotnet restore "CompanyManager.Api/CompanyManager.IdentityServer.csproj"
COPY . .
WORKDIR "/src/CompanyManager.IdentityServer"
RUN dotnet build "CompanyManager.IdentityServer.csproj" -c Release -o /app/build
RUN dotnet publish "CompanyManager.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CompanyManager.IdentityServer.dll"]
