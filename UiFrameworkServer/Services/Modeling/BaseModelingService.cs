namespace UiFrameworkServer.Services.Modeling
{
    public abstract class BaseModelingService : BaseDatabaseService
    {
        public BaseModelingService(IServiceProvider serviceProvider)
            : base(serviceProvider) { }
    }
}
