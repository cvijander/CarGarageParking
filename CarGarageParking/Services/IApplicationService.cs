using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IApplicationService
    {
        IEnumerable<Application> GetAllApplications();

       Application GetApplicationById(int id);

        void AddApplication(Application application);

        void Update(Application application);

        void Delete(int id);
    }
}
