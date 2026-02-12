using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UiFrameworkServer.Models;
using UiFrameworkServer.Services.Modeling;
using UiFrameworkServer.Services.User;

namespace UiFrameworkServer.Controllers.User
{
    [ApiController]
    [Route("Profile")]
    public class ProfileController : BaseController
    {
        public UserTestService UserTestService { get; }
        public ProfileModelingService ProfileModelingService { get; }

        public ProfileController(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            ProfileModelingService = serviceProvider.GetRequiredService<ProfileModelingService>();
            UserTestService = serviceProvider.GetRequiredService<UserTestService>();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ProfileModel Single([FromRoute] string id)
        {
            var item = UserTestService.Single(id);

            return ProfileModelingService.Single(item);
        }

        [AllowAnonymous]
        [HttpPut("")]
        public ProfileModel Create([FromBody] string name)
        {
            var item = UserTestService.Create(name);

            return ProfileModelingService.Single(item);
        }
    }
}
