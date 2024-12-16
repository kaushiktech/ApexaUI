using ApexApi.Data.Data;
using ApexApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexApi.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Advisors.AddRange(
                    new Advisor { Id = 1, FullName = "Action", SIN = 123123123 },
                    new Advisor { Id = 2, FullName = "SciFi", SIN = 133123323 },
                    new Advisor { Id = 3, FullName = "History", SIN = 143123143 }
                    );
                context.SaveChanges();
            }
        }
    }
}
