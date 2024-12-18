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
            entity.healthStatus = ModelHelper.GenerateHealthStatus();
            dbSet.Add(entity);
        }
        public void Update(Advisor entity)
        {
            Advisor obj =_dbContext.Advisors.FirstOrDefault(a => a.Id == entity.Id);
            UpdateAdvisor(entity, obj);
            dbSet.Update(obj);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        private void UpdateAdvisor(Advisor entity,Advisor obj)
        {
            //obj.healthStatus = source.healthStatus;
            obj.address = entity.address;
            obj.fullName = entity.fullName;

            //If masked charachter detected reset phone number to source
            if (!string.IsNullOrEmpty(entity.phoneNumber) && !entity.phoneNumber.Contains("X"))
                obj.phoneNumber = entity.phoneNumber;
            //If masked charachter detected reset SIN to source
            if (!string.IsNullOrEmpty(entity.sin) && !entity.sin.Contains("X"))
                obj.sin = entity.sin;
        }
    }
}
