using Hydra.Voucher.API.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Hydra.Voucher.API.Setup
{
    public static class MongoConfig
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
             var connectionString = configuration.GetSection("MongoDBConnection");
            services.Configure<MongoSettings>(connectionString);
            
            var settings = connectionString.Get<MongoSettings>();
            
            services.AddSingleton((s) =>
            {
                IMongoClient client = new MongoClient(settings.ConnectionString);
                return client;
            });
        }
    }
}