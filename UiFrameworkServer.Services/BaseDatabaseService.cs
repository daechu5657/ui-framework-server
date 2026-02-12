using UiFrameworkServer.Databases;

namespace UiFrameworkServer.Services
{
    public abstract class BaseDatabaseService : BaseService
    {
        public MongoContext MongoContext { get; }

        public BaseDatabaseService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            MongoContext = ServiceProvider.GetRequiredService<MongoContext>();
        }
    }
}
