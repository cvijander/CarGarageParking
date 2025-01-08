using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public class GarageService : IGarageService
    {

        private readonly CarGarageParkingDBContext _dbContext;

        public GarageService(CarGarageParkingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddGarage(Garage garage)
        {
            _dbContext.Garages.Add(garage);
            
        }

        public void Delete(int id)
        {
            Garage garage = _dbContext.Garages.Find(id);
            if(garage != null)
            {
                _dbContext.Garages.Remove(garage);
            }
        }

        public IEnumerable<Garage> GetAllGarages()
        {
            return _dbContext.Garages.ToList();
        }

        public Garage GetGarageById(int id)
        {
            return _dbContext.Garages.Find(id);
        }

        public void Update(Garage garage)
        {
            _dbContext.Garages.Update(garage);
        }
    }
}
