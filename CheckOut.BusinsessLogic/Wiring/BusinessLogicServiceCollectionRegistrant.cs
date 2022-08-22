using CheckOut.DataAccess.Wiring;
using CheckOut.Infrastructure.Wiring;
using Microsoft.Extensions.DependencyInjection;

namespace CheckOut.BusinsessLogic.Wiring
{
    public class BusinessLogicServiceCollectionRegistrant : IServiceCollectionRegistrant
    {
        private readonly GenericServiceCollectionRegistrant<BusinessLogicServiceCollectionRegistrant> _genericBusinessLogicServiceCollectionRegistrant;
        private readonly InfrastructureServiceCollectionRegistrant _infrastructureServiceCollectionRegistrant;
        private readonly DataAccessServiceCollectionRegistrant _dataAccessServiceCollectionRegistrant;

        private BusinessLogicServiceCollectionRegistrant(
            GenericServiceCollectionRegistrant<BusinessLogicServiceCollectionRegistrant> genericBusinessLogicServiceCollectionRegistrant,
            InfrastructureServiceCollectionRegistrant infrastructureServiceCollectionRegistrant,
            DataAccessServiceCollectionRegistrant dataAccessServiceCollectionRegistrant)
        {
            _genericBusinessLogicServiceCollectionRegistrant = genericBusinessLogicServiceCollectionRegistrant;
            _infrastructureServiceCollectionRegistrant = infrastructureServiceCollectionRegistrant;
            _dataAccessServiceCollectionRegistrant = dataAccessServiceCollectionRegistrant;
        }

        public BusinessLogicServiceCollectionRegistrant()
            : this(new GenericServiceCollectionRegistrant<BusinessLogicServiceCollectionRegistrant>(),
                  new InfrastructureServiceCollectionRegistrant(),
                  new DataAccessServiceCollectionRegistrant())
        {
        }

        public void Register(IServiceCollection services)
        {
            _infrastructureServiceCollectionRegistrant.Register(services);
            _genericBusinessLogicServiceCollectionRegistrant.Register(services);
            _dataAccessServiceCollectionRegistrant.Register(services);
        }
    }
}
