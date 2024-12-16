using ApexApi.Models;

namespace ApexApi.Data.Repository.IRepository
{
    public interface IAdvisorRepository : IRepository<Advisor>
    {
        void Update(Advisor advisor);
        void Save();
    }
}
