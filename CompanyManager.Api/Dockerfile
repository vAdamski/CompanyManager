FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["CompanyManager.Api/CompanyManager.Api.csproj", "CompanyManager.Api/"]
COPY ["CompanyManager.Infrastructure/CompanyManager.Infrastructure.csproj", "CompanyManager.Infrastructure/"]
COPY ["CompanyManager.Application/CompanyManager.Application.csproj", "CompanyManager.Application/"]
COPY ["CompanyManager.Domain/CompanyManager.Domain.csproj", "CompanyManager.Domain/"]
COPY ["CompanyManager.Persistence/CompanyManager.Persistence.csproj", "CompanyManager.Persistence/"]
RUN dotnet restore "CompanyManager.Api/CompanyManager.Api.csproj"
COPY . .
WORKDIR "/src/CompanyManager.Api"
RUN dotnet build "CompanyManager.Api.csproj" -c Release -o /app/build
RUN dotnet publish "CompanyManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CompanyManager.Api.dll"]