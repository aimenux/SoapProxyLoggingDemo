using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lib;

public static class Extensions
{
    private static readonly object Locker = new();

    public static void AddSoapDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SoapSettings>(configuration.GetSection("SoapSettings"));
        services.AddTransient<SoapLoggingClientMessageInspector>();
        services.AddTransient<SoapLoggingBehaviour>();
        services.AddTransient<ISoapLogger, SoapLogger>();
        services.AddTransient<ISoapProxy, SoapProxy>();
        services.AddTransient(serviceProvider =>
        {
            var loggingBehaviour = serviceProvider.GetRequiredService<SoapLoggingBehaviour>();
            var options = serviceProvider.GetRequiredService<IOptions<SoapSettings>>();
            return CreateSoapClient(options.Value, loggingBehaviour);
        });
    }

    public static void WriteLine(this ConsoleColor color, object value)
    {
        lock (Locker)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }

    private static ISoapClient CreateSoapClient(SoapSettings settings, IEndpointBehavior loggingBehaviour)
    {
        var binding = GetSoapBinding(settings);
        var endpointAddress = new EndpointAddress(settings.Endpoint);
        var client = new SoapClient(binding, endpointAddress);
        client.Endpoint.EndpointBehaviors.Add(loggingBehaviour);
        return client;
    }

    private static HttpBindingBase GetSoapBinding(SoapSettings settings)
    {
        var timeout = TimeSpan.FromMinutes(1);
        var isHttpsBinding = settings.Endpoint.StartsWith("https", StringComparison.OrdinalIgnoreCase);
        HttpBindingBase binding = isHttpsBinding ? new BasicHttpsBinding() : new BasicHttpBinding();
        binding.ReceiveTimeout = timeout;
        binding.SendTimeout = timeout;
        return binding;
    }
}