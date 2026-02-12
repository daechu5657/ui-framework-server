using Microsoft.AspNetCore.Mvc;
using UiFrameworkServer.Databases;

namespace UiFrameworkServer.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public IServiceProvider ServiceProvider { get; }
        public MongoContext MongoContext { get; }

        // public UserAuthorizeService UserAuthorizeService { get; }
        // public string? Identity => UserAuthorizeService.Identity;
        // public Databases.Models.User CurrentUser => UserAuthorizeService.CurrentUser;

        public BaseController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            MongoContext = ServiceProvider.GetRequiredService<MongoContext>();
            // UserAuthorizeService = ServiceProvider.GetRequiredService<UserAuthorizeService>();
        }
    }
}
