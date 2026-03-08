# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MediScan.sln", "./"]
COPY ["MediScan.Api/MediScan.Api.csproj", "MediScan.Api/"]
COPY ["MediScan.Application/MediScan.Application.csproj", "MediScan.Application/"]
COPY ["MediScan.Core/MediScan.Core.csproj", "MediScan.Core/"]
COPY ["MediScan.Infrastructure/MediScan.Infrastructure.csproj", "MediScan.Infrastructure/"]

RUN dotnet restore

COPY . .
WORKDIR "/src/MediScan.Api"
RUN dotnet build "MediScan.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "MediScan.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MediScan.Api.dll"]
