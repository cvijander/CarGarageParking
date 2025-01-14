using CarGarageParking.Models;
using System.Reflection.Metadata.Ecma335;

namespace CarGarageParking.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly CarGarageParkingDBContext _dbContext;

        public ApplicationService(CarGarageParkingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddApplication(Application application)
        {
            _dbContext.Applications.Add(application);
        }

        public void Delete(int id)
        {
            var application = _dbContext.Applications.Find(id);
            if (application != null)
            {
                _dbContext.Applications.Remove(application);
            }

        }

        public IEnumerable<Application> GetAllApplications()
        {
           return _dbContext.Applications.ToList();
        }

        public Application GetApplicationById(int id)
        {
            return  _dbContext.Applications.Find(id);
        }

        public void Update(Application application)
        {
            _dbContext.Applications.Update(application);
        }
    }
}
