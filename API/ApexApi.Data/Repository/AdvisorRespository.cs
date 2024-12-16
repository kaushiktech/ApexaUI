using ApexApi.Data.Data;
using ApexApi.Data.Repository.IRepository;
using ApexApi.Models;

namespace ApexApi.Data.Repository
{
    public class AdvisorRepository : Repository<Advisor>, IAdvisorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdvisorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Advisor product)
        {
            _dbContext.Advisors.Update(product);
            Save();
        }
    }
}
