using ApexApi.Data.Data;
using ApexApi.Models;
using ApexApi.Utility;
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
                    new Advisor { Id = 1, fullName = "Rocco Whitaker", sin = "123123123", healthStatus = 1 },
                    new Advisor { Id = 2, fullName = "Melina Tapia", sin = "133123323", healthStatus = 2 },
                    new Advisor { Id = 3, fullName = "Tanner Aguilar", sin = "143123143", healthStatus = 3 }
                    );
                context.SaveChanges();
            }
        }
    }
}
