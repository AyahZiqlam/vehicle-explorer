# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copy project files first
COPY VehicleExplorer.Api/VehicleExplorer.Api.csproj VehicleExplorer.Api/
COPY VehicleExplorer.Application/VehicleExplorer.Application.csproj VehicleExplorer.Application/
COPY VehicleExplorer.Domain/VehicleExplorer.Domain.csproj VehicleExplorer.Domain/
COPY VehicleExplorer.Infrastructure/VehicleExplorer.Infrastructure.csproj VehicleExplorer.Infrastructure/

# Restore dependencies
RUN dotnet restore VehicleExplorer.Api/VehicleExplorer.Api.csproj

# Copy full source code
COPY . .

# Publish API
RUN dotnet publish VehicleExplorer.Api/VehicleExplorer.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "VehicleExplorer.Api.dll"]