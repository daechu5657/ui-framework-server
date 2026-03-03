// using MongoDB.Driver;

// namespace UiFrameworkServer.Services.User
// {
//     public class UserTestService : BaseDatabaseService
//     {
//         public UserTestService(IServiceProvider serviceProvider)
//             : base(serviceProvider) { }

//         public Databases.Models.User Single(string id)
//         {
//             var item = MongoContext.User.AsQueryable().SingleOrDefault(a => a.Id == id);

//             if (item == null)
//             {
//                 throw new NotFoundException();
//             }

//             return item;
//         }

//         public Databases.Models.User Create(string name)
//         {
//             var item = MongoContext.User.AsQueryable().SingleOrDefault(a => a.Name == name);
//             if (item != null)
//             {
//                 throw new ConflictException();
//             }

//             item = new Databases.Models.User
//             {
//                 Name = name,
//                 CreatedTime = DateTime.UtcNow,
//                 UpdatedTime = DateTime.UtcNow,
//             };

//             MongoContext.InsertOne(item);

//             return Single(item.Id);
//         }
//     }
// }
