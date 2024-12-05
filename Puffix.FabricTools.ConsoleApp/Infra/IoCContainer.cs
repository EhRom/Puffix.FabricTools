using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Puffix.IoC;
using Puffix.IoC.Configuration;

namespace Puffix.FabricTools.ConsoleApp.Infra;

public class IoCContainer : IIoCContainerWithConfiguration
{
    private readonly IContainer container;

    public IConfiguration Configuration { get; }

    public IoCContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
    {
        // Self-register the container.
        containerBuilder.Register(_ => this).As<IIoCContainerWithConfiguration>().SingleInstance();
        containerBuilder.Register(_ => this).As<IIoCContainer>().SingleInstance();

        container = containerBuilder.Build();
        Configuration = configuration;
    }

    public static IIoCContainerWithConfiguration BuildContainer(IConfiguration configuration)
    {
        // Register HttpClientMessageFactory
        IServiceCollection services = new ServiceCollection()
                .AddHttpClient()
                .AddMemoryCache();

        // https://nodogmablog.bryanhogan.net/2023/03/polly-with-net-6-part-8-policy-registry-with-minimal-apis-and-httpclientfactory/

        AutofacServiceProviderFactory providerFactory = new AutofacServiceProviderFactory();
        ContainerBuilder containerBuilder = providerFactory.CreateBuilder(services);

        containerBuilder.RegisterAssemblyTypes
                        (
                            typeof(IoCContainer).Assembly // Current Assembly.
                        )
                        .AsSelf()
                        .AsImplementedInterfaces();

        containerBuilder.RegisterInstance(configuration).SingleInstance();

        return new IoCContainer(containerBuilder, configuration);
    }

    public ObjectT Resolve<ObjectT>(params IoCNamedParameter[] parameters)
        where ObjectT : class
    {
        ObjectT resolvedObject;
        if (parameters != null && parameters.Length > 0)
            resolvedObject = container.Resolve<ObjectT>(ConvertIoCNamedParametersToAutfac(parameters));
        else
            resolvedObject = container.Resolve<ObjectT>();

        return resolvedObject;
    }

    public object Resolve(Type objectType, params IoCNamedParameter[] parameters)
    {
        object resolvedObject;
        if (parameters != null && parameters.Length > 0)
            resolvedObject = container.Resolve(objectType, ConvertIoCNamedParametersToAutfac(parameters));
        else
            resolvedObject = container.Resolve(objectType);

        return resolvedObject;
    }

    private IEnumerable<NamedParameter> ConvertIoCNamedParametersToAutfac(IEnumerable<IoCNamedParameter> parameters)
    {
        foreach (var parameter in parameters)
        {
            if (parameter != null)
                yield return new NamedParameter(parameter.Name, parameter.Value);
        }
    }
}