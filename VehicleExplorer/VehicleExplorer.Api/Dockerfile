FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["VehicleExplorer.Api/VehicleExplorer.Api.csproj", "VehicleExplorer.Api/"]
RUN dotnet restore "VehicleExplorer.Api/VehicleExplorer.Api.csproj"

COPY . .
WORKDIR "/src/VehicleExplorer.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "VehicleExplorer.Api.dll"]