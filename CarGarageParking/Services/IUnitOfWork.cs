namespace CarGarageParking.Services
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        IOwnerService OwnerService { get; }

        IVehicleService VehicleService { get; }

        IGarageService GarageService { get; }
    }
}
