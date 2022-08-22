using CheckOut.Infrastructure.Wiring;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckOut.DataAccess.Wiring
{
    public class DataAccessServiceCollectionRegistrant : IServiceCollectionRegistrant
    {
        public DataAccessServiceCollectionRegistrant()
        {
        }

        public void Register(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddDbContext<CheckOutDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WebApiDatabase"));
            });
        }
    }
}
