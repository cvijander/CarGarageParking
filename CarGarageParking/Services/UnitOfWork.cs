namespace CarGarageParking.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarGarageParkingDBContext _context;

        public UnitOfWork(CarGarageParkingDBContext context)
        {
            _context = context;

            OwnerService = new OwnerService(context);

            VehicleService = new VehicleService(context);

            GarageService = new GarageService(context);

            VehicleInGarageService = new VehicleInGarageService(context);

            ApplicationService = new ApplicationService(context);

        }

        public IOwnerService OwnerService { get; private set; }

        public IVehicleService VehicleService { get; private set; }

        public IGarageService GarageService { get; private set; }
        public IVehicleInGarageService VehicleInGarageService { get; set; }

        public IApplicationService ApplicationService { get; set; }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
