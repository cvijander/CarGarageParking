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

        }

        public IOwnerService OwnerService { get; private set; }

        public IVehicleService VehicleService { get; private set; }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
