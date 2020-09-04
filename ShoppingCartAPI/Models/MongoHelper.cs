using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ShoppingCartAPI.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoDatabase = "ShoppingListDB";
        public static string MongoConnection = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build().GetSection("connectionString").Value;

        public static IMongoCollection<Models.Product> products_collection { get; set; }

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        internal static void ConnectToMongoService()
            
        {
            
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    
}
