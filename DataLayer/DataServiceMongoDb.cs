using MandalorianDB.DataLayer;
using MandalorianDB.Models;
using MandalorianDB.BusinessLayer;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MandalorianDB.DataLayer
{
    public class DataServiceMongoDb : IDataService

    {
        private List<Episode> _episodes;
        private IMongoCollection<Episode> _collection;

        public DataServiceMongoDb()
        {
            Connection();
        }

        /// <summary>
        /// connect to online MongoDb database
        /// </summary>
        /// <returns>true if connected</returns>
        private bool Connection()
        {
            try
            {
                MongoClient dbClient = new MongoClient(MongoDbDataSettings.connectionString);
                IMongoDatabase database = dbClient.GetDatabase(MongoDbDataSettings.databaseName);
                _collection = database.GetCollection<Episode>(MongoDbDataSettings.collectionName);

                _episodes = _collection.Find(Builders<Episode>.Filter.Empty).ToList();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// get all widgets
        /// </summary>
        /// <returns>IEnumerable of widgets</returns>
        public IEnumerable<Episode> GetAll()
        {
            return _episodes;
        }

        /// <summary>
        /// add a new widget
        /// </summary>
        /// <param name="widget">widget to add</param>
        public void Add(Episode episode)
        {
            episode.Id = NextIdNumber();
            _collection.InsertOne(episode);
        }

        /// <summary>
        /// delete a widget
        /// </summary>
        /// <param name="id">widget to delete</param>
        public void Delete(int id)
        {
            Episode episodeToDelete = _episodes.FirstOrDefault(e => e.Id == id);

            if (episodeToDelete != null)
            {
                var deleteFilter = Builders<Episode>.Filter.Eq("Id", id);
                _collection.DeleteOne(deleteFilter);
            }
        }

        /// <summary>
        /// get a widget by id
        /// </summary>
        /// <param name="id">widget id</param>
        /// <returns>widget</returns>
        public Episode GetById(int id)
        {
            var getFilter = Builders<Episode>.Filter.Eq("Id", id);
            return _collection.Find(getFilter).FirstOrDefault();
        }

        /// <summary>
        /// update a widget
        /// </summary>
        /// <param name="widget">widget to update</param>
        public void Update(Episode episode)
        {
            var updateFilter = Builders<Episode>.Filter.Eq("Id", episode.Id);
            var deleteResult = _collection.DeleteOne(updateFilter);
            _collection.InsertOne(episode);
        }

        /// <summary>
        /// get the next highest id number from the list of widgets
        /// </summary>
        /// <returns>next id number</returns>
        private int NextIdNumber()
        {
            return _episodes.Max(w => w.Id) + 1;
        }

        IEnumerable<Episode> IDataService.GetAll()
        {
            return _episodes;
        }

        Episode IDataService.GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IDataService.Add(Episode character)
        {
            throw new NotImplementedException();
        }

        void IDataService.Update(Episode character)
        {
            throw new NotImplementedException();
        }
    }

}
