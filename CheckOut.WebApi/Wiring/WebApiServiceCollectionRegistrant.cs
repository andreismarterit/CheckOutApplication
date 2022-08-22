using CheckOut.BusinsessLogic.Wiring;
using CheckOut.Infrastructure.Wiring;

namespace CheckOut.WebApi.Wiring
{
    public class WebApiServiceCollectionRegistrant : IServiceCollectionRegistrant
    {
        private BusinessLogicServiceCollectionRegistrant _businessLogicServiceCollectionRegistrant;

        private WebApiServiceCollectionRegistrant(BusinessLogicServiceCollectionRegistrant businessLogicServiceCollectionRegistrant)
        {
            _businessLogicServiceCollectionRegistrant = businessLogicServiceCollectionRegistrant;
        }

        public WebApiServiceCollectionRegistrant() :
            this(new BusinessLogicServiceCollectionRegistrant())
        {
        }

        public void Register(IServiceCollection services)
        {
            _businessLogicServiceCollectionRegistrant.Register(services);
        }
    }
}
