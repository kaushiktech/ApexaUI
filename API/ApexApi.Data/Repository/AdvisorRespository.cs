using ApexApi.Data.Data;
using ApexApi.Data.Repository.IRepository;
using ApexApi.Models;
using ApexApi.Utility;
using Microsoft.EntityFrameworkCore;

namespace ApexApi.Data.Repository
{
    public class AdvisorRepository : Repository<Advisor>, IAdvisorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdvisorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Advisor entity)
        {
            entity.HealthStatus = ModelHelper.GenerateHealthStatus();
            dbSet.Add(entity);
        }
        public void Update(Advisor entity)
        {
            Advisor obj =_dbContext.Advisors.FirstOrDefault(a => a.Id == entity.Id);
            UpdateAdvisor(obj, entity);
            dbSet.Add(entity);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        private void UpdateAdvisor(Advisor source,Advisor target)
        {
            target.HealthStatus = source.HealthStatus;
            //If masked charachter detected reset phone number to source
            if(target.PhoneNumber.Contains("X"))
                target.PhoneNumber = source.PhoneNumber;
            //If masked charachter detected reset SIN to source
            if (target.SIN.Contains("X"))
                target.SIN = source.SIN;
        }
    }
}
