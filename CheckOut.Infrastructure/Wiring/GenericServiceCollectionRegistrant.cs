using CheckOut.Infrastructure.Executors.WebApi.Command;
using CheckOut.Infrastructure.DesignServices;
using Microsoft.Extensions.DependencyInjection;

namespace CheckOut.Infrastructure.Wiring
{
    public interface IServiceCollectionRegistrant
    {
        void Register(IServiceCollection services);
    }

    public class GenericServiceCollectionRegistrant<T> : IServiceCollectionRegistrant
    {
        public void Register(IServiceCollection services)
        {
            RegisterLoaders(services);
            RegisterCommands(services);
            RegisterBuilders(services);
            RegisterWebApiCommands(services);
        }

        private void RegisterLoaders(IServiceCollection services)
        {
            RegisterInterfaceImplementation(services, typeof(ILoaderService<,>));
        }

        private void RegisterCommands(IServiceCollection services)
        {
            RegisterInterfaceImplementation(services, typeof(ICommandService<,>));
        }

        private void RegisterBuilders(IServiceCollection services)
        {
            RegisterInterfaceImplementation(services, typeof(IBuilderService<,>));
        }

        private void RegisterWebApiCommands(IServiceCollection services)
        {
            RegisterInterfaceImplementation(services, typeof(IWebApiCommandService<>));
            RegisterInterfaceImplementation(services, typeof(IWebApiCommandService<,>));
            RegisterInterfaceImplementation(services, typeof(IWebApiCommandService<,,>));
        }

        private void RegisterInterfaceImplementation(IServiceCollection services, Type interfaceType)
        {
            services.Scan(x =>
            {
                x.FromAssemblies(typeof(T).Assembly)
                    .AddClasses(c => c.AssignableTo(interfaceType))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });
        }
    }
}
