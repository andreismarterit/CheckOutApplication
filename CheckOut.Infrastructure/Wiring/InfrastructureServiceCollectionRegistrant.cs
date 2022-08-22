using CheckOut.Infrastructure.Executors.WebApi.Executor;
using Microsoft.Extensions.DependencyInjection;

namespace CheckOut.Infrastructure.Wiring
{
    public class InfrastructureServiceCollectionRegistrant : IServiceCollectionRegistrant
    {
        private readonly GenericServiceCollectionRegistrant<InfrastructureServiceCollectionRegistrant> _genericInfrastuctureServiceCollectionRegistrant;

        private InfrastructureServiceCollectionRegistrant(GenericServiceCollectionRegistrant<InfrastructureServiceCollectionRegistrant> genericBusinessLogicServiceCollectionRegistrant)
        {
            _genericInfrastuctureServiceCollectionRegistrant = genericBusinessLogicServiceCollectionRegistrant;
        }

        public InfrastructureServiceCollectionRegistrant()
            : this(new GenericServiceCollectionRegistrant<InfrastructureServiceCollectionRegistrant>())
        {
        }

        public void Register(IServiceCollection services)
        {
            _genericInfrastuctureServiceCollectionRegistrant.Register(services);

            services.AddTransient<WebApiExecutor>();
        }
    }
}
