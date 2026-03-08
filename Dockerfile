# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files to restore dependencies
COPY ["MediScan.sln", "./"]
COPY ["MediScan.Api/MediScan.Api.csproj", "MediScan.Api/"]
COPY ["MediScan.Application/MediScan.Application.csproj", "MediScan.Application/"]
COPY ["MediScan.Core/MediScan.Core.csproj", "MediScan.Core/"]
COPY ["MediScan.Infrastructure/MediScan.Infrastructure.csproj", "MediScan.Infrastructure/"]

RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR "/src/MediScan.Api"
RUN dotnet build "MediScan.Api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "MediScan.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MediScan.Api.dll"]
