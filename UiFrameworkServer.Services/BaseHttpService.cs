namespace UiFrameworkServer.Services
{
    public abstract class BaseHttpService : BaseService
    {
        public IHttpClientFactory HttpFactory { get; }

        public BaseHttpService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            HttpFactory = ServiceProvider.GetRequiredService<IHttpClientFactory>();
        }

        protected virtual HttpClient CreateClient()
        {
            var client = HttpFactory.CreateClient(GetType().Name);
            return client;
        }
    }
}
