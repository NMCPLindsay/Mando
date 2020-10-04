using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MandalorianDB.DataLayer
{
    /// <summary>
    /// Database configurations: user name, password, database name, collection name, connection string
    /// </summary>
    public static class MongoDbDataSettings
    {
        private static string userName = "Admin";
        private static string password = "Aa123456";

        public static string connectionString = $"mongodb+srv://{userName}:{password}@mandaloriandb.1sbfk.mongodb.net/<{databaseName}>?retryWrites=true&w=majority";

        public static string collectionName = "CIT255";
        public static string databaseName = "MandoEpisodes";
    }
}
