using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using VehicleExplorer.Infrastructure.Models;

namespace VehicleExplorer.Infrastructure.Services;

public class VehicleApiClient : IVehicleService
{
    private readonly HttpClient _client;
    private readonly IMemoryCache _cache;

    public VehicleApiClient(HttpClient client, IMemoryCache cache)
    {
        _client = client;
        _cache = cache;
    }

    public async Task<IEnumerable<VehicleMakeDto>> GetMakes()
    {
        const string cacheKey = "vehicle_makes";

        if (_cache.TryGetValue(cacheKey, out IEnumerable<VehicleMakeDto> cachedMakes))
        {
            return cachedMakes;
        }

        var result = await _client.GetFromJsonAsync<NhtsaResponse<MakeResult>>(
            "vehicles/getallmakes?format=json");

        if (result?.Results == null)
            return Enumerable.Empty<VehicleMakeDto>();

        var makes = result.Results
            .Select(m => new VehicleMakeDto(
                m.Make_ID,
                m.Make_Name
            ))
            .ToList();

        _cache.Set(cacheKey, makes, TimeSpan.FromHours(6));

        return makes;
    }

    public async Task<IEnumerable<VehicleTypeDto>> GetVehicleTypes(int makeId)
    {
        var result = await _client.GetFromJsonAsync<NhtsaResponse<TypeResult>>(
            $"vehicles/GetVehicleTypesForMakeId/{makeId}?format=json");

        if (result?.Results == null)
            return Enumerable.Empty<VehicleTypeDto>();

        return result.Results.Select(x =>
            new VehicleTypeDto(x.VehicleTypeName));
    }

    public async Task<IEnumerable<VehicleModelDto>> GetModels(int makeId, int year)
    {
        var result = await _client.GetFromJsonAsync<NhtsaResponse<ModelResult>>(
            $"vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json");

        if (result?.Results == null)
            return Enumerable.Empty<VehicleModelDto>();

        return result.Results.Select(x =>
            new VehicleModelDto(x.Model_Name));
    }
}