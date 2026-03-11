public interface IVehicleService
{
    Task<IEnumerable<VehicleMakeDto>> GetMakes();

    Task<IEnumerable<VehicleTypeDto>> GetVehicleTypes(int makeId);

    Task<IEnumerable<VehicleModelDto>> GetModels(int makeId, int year);
}