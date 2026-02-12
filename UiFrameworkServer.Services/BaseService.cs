using System.Reflection;

namespace UiFrameworkServer.Services
{
    public abstract class BaseService
    {
        public IServiceProvider ServiceProvider { get; }

        public IConfiguration Configuration { get; }

        public BaseService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
        }
    }

    public static class BaseServiceExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var customServices = types.Where(a =>
                a.IsSubclassOf(typeof(BaseService)) && !a.IsAbstract
            );

            foreach (var item in customServices)
            {
                services.AddScoped(item);

                if (item.IsSubclassOf(typeof(BaseHttpService)))
                {
                    services.AddHttpClient(item.Name);
                }
            }

            return services;
        }
    }
}
