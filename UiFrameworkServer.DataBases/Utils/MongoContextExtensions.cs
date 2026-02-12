using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UiFrameworkServer.Databases.Utils;

namespace UiFrameworkServer.Databases
{
    public static class MongoContextExtensions
    {
        private static bool _initialized;

        public static IServiceCollection AddMongoContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // ---- (1) 전역 설정들: 1번만 실행되게 가드 ----
            if (!_initialized)
            {
                _initialized = true;

                JsonConvert.DefaultSettings = () =>
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    };

                ConventionRegistry.Register(
                    nameof(IgnoreExtraElementsConvention),
                    new ConventionPack { new IgnoreExtraElementsConvention(true) },
                    _ => true
                );

                ConventionRegistry.Register(
                    nameof(DictionaryRepresentationConvention),
                    new ConventionPack
                    {
                        new DictionaryRepresentationConvention(DictionaryRepresentation.Document),
                    },
                    _ => true
                );
            }

            // ---- (2) Settings 바인딩 (Options 패턴) ----
            services.Configure<MongoContextSettings>(
                configuration.GetSection(nameof(MongoContextSettings))
            );

            // ---- (3) MongoClient Singleton 등록 ----
            services.TryAddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoContextSettings>>().Value;

                if (string.IsNullOrWhiteSpace(settings.ConnectionString))
                    throw new InvalidOperationException(
                        "MongoContextSettings:ConnectionString is empty."
                    );

                var mongoClientSettings = MongoClientSettings.FromConnectionString(
                    settings.ConnectionString
                );

                // 필요하면 여기서 튜닝 가능:
                // mongoClientSettings.RetryWrites = true;
                // mongoClientSettings.RetryReads = true;

                return new MongoClient(mongoClientSettings);
            });

            // ---- (4) MongoContext는 Scoped 유지 ----
            services.TryAddScoped<MongoContext>();

            return services;
        }
    }
}
