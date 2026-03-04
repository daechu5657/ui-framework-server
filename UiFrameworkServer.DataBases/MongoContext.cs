using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UiFrameworkServer.Databases
{
    public class MongoContextSettings
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        // public string Secret { get; set; } = string.Empty;
    }

    public class MongoContextSession : IDisposable
    {
        public MongoContext MongoContext { get; }

        public IClientSessionHandle OriginalSession { get; }

        public bool IsDisposed { get; private set; } = false;

        public bool IsCommited { get; private set; } = false;

        public MongoContextSession(MongoContext mongoContext)
        {
            MongoContext = mongoContext;
            OriginalSession = mongoContext.Client.StartSession(
                new ClientSessionOptions()
                {
                    DefaultTransactionOptions = new TransactionOptions(
                        readConcern: ReadConcern.Snapshot,
                        writeConcern: WriteConcern.WMajority
                    ),
                }
            );
            OriginalSession.StartTransaction();
        }

        public void CommitTransaction()
        {
            OriginalSession.CommitTransaction();
            IsCommited = true;
        }

        public void AbortTransaction()
        {
            OriginalSession.AbortTransaction();
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            if (OriginalSession.IsInTransaction && !IsCommited)
            {
                OriginalSession.AbortTransaction();
            }

            OriginalSession.Dispose();
            IsDisposed = true;
        }
    }

    public class MongoContext
    {
        public static MongoClientSettings? MongoClientSettings;
        public MongoContextSettings? Settings { get; }
        public IMongoClient Client { get; }
        public MongoContextSession? Session { get; private set; }
        private bool HasSession => Session is not null && !Session.IsDisposed;
        public IMongoDatabase Database =>
            Client.GetDatabase(Settings?.DatabaseName ?? nameof(UiFrameworkServer));

        // public IMongoCollection<User> User => Database.GetCollection<User>(nameof(User));

        public MongoContext(IMongoClient client, IOptionsSnapshot<MongoContextSettings> options)
        {
            Client = client;
            Settings = options.Value;

            if (string.IsNullOrWhiteSpace(Settings.DatabaseName))
            {
                throw new InvalidOperationException("MongoContextSettings:DatabaseName is empty.");
            }
        }

        public MongoContextSession CreateSession()
        {
            if (HasSession)
            {
                throw new Exception("Session is already existed.");
            }

            Session = new MongoContextSession(this);

            return Session;
        }

        public MongoContextSession? GetCurrentSession()
        {
            return Session;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionProperty = GetType()
                .GetProperties()
                .SingleOrDefault(a => a.PropertyType == typeof(IMongoCollection<T>));
            if (collectionProperty == null)
            {
                throw new NullReferenceException(nameof(collectionProperty));
            }

            var collection = collectionProperty.GetValue(this) as IMongoCollection<T>;
            if (collection == null)
            {
                throw new NullReferenceException(nameof(collection));
            }

            return collection;
        }

        public void InsertOne<T>(T item)
        {
            var collection = GetCollection<T>();

            if (HasSession)
            {
                collection.InsertOne(Session!.OriginalSession, item);
            }
            else
            {
                collection.InsertOne(item);
            }
        }

        public void InsertMany<T>(IEnumerable<T> list)
        {
            var collection = GetCollection<T>();

            if (HasSession)
            {
                collection.InsertMany(Session!.OriginalSession, list);
            }
            else
            {
                collection.InsertMany(list);
            }
        }

        public UpdateResult UpdateOne<T>(Expression<Func<T, bool>> item, UpdateDefinition<T> update)
        {
            var collection = GetCollection<T>();

            return HasSession
                ? collection.UpdateOne(Session!.OriginalSession, item, update)
                : collection.UpdateOne(item, update);
        }

        public UpdateResult UpdateMany<T>(
            Expression<Func<T, bool>> list,
            UpdateDefinition<T> update
        )
        {
            var collection = GetCollection<T>();

            return HasSession
                ? collection.UpdateMany(Session!.OriginalSession, list, update)
                : collection.UpdateMany(list, update);
        }

        public void DeleteMany<T>(Expression<Func<T, bool>> list)
        {
            var collection = GetCollection<T>();

            if (HasSession)
            {
                collection.DeleteMany(Session!.OriginalSession, list);
            }
            else
            {
                collection.DeleteMany(list);
            }
        }

        public void DeleteOne<T>(Expression<Func<T, bool>> item)
        {
            var collection = GetCollection<T>();

            if (HasSession)
            {
                collection.DeleteOne(Session!.OriginalSession, item);
            }
            else
            {
                collection.DeleteOne(item);
            }
        }

        // public T RetryableTransaction<T>(Func<MongoContextSession, T> action)
        //     where T : class?
        // {
        //     var maxCount = 10;
        //     var count = 0;

        //     T? result = null;

        //     while (count < maxCount)
        //     {
        //         using var session = CreateSession();

        //         try
        //         {
        //             result = action(session);
        //         }
        //         catch (MongoCommandException ex)
        //         {
        //             if (ex.Code == 112)
        //             {
        //                 count += 1;
        //                 Task.Delay(TimeSpan.FromSeconds(1)).GetAwaiter().GetResult();

        //                 continue;
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }

        //         session.CommitTransaction();
        //         break;
        //     }

        //     return result!;
        // }

        // public void RetryableTransaction(Action<MongoContextSession> action)
        // {
        //     RetryableTransaction<object?>(
        //         (session) =>
        //         {
        //             action(session);

        //             return null;
        //         }
        //     );
        // }
    }
}

