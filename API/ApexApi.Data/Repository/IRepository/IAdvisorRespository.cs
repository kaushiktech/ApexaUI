using ApexApi.Models;

namespace ApexApi.Data.Repository.IRepository
{
    public interface IAdvisorRepository : IRepository<Advisor>
    {
        void Add(Advisor entity);
        void Update(Advisor entity);
        void Save();
    }
}
