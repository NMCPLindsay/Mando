using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MandalorianDB.DataLayer;
using MandalorianDB.Models;

namespace MandalorianDB.BusinessLayer
{
    /// <summary>
    /// Repository for CRUD
    /// Note: the _dataService object is instantiated with DataConfig class
    /// </summary>
    class EpisodeRepository : IEpisodeRepository, IDisposable
    {
        private IDataService _dataService;
        List<Episode> _episodes;

        /// <summary>
        /// set the correct data service and read in a list of all widgets
        /// </summary>
        public EpisodeRepository()
        {

            _dataService = new DataServiceMongoDb();

            try
            {
                _episodes = _dataService.GetAll() as List<Episode>;
            }
            catch (Exception e)
            {
                string message = e.Message;
                throw;
            }
        }

        /// <summary>
        /// retrieve all widgets
        /// </summary>
        /// <returns>all widgets</returns>
        public IEnumerable<Episode> GetAll()
        {
            return _episodes;
        }

        /// <summary>
        /// retrieve a widget by the id
        /// </summary>
        /// <param name="name">widget name</param>
        /// <returns></returns>
        public Episode GetById(int id)
        {
            return _episodes.FirstOrDefault(w => w.Id == id);
        }

        /// <summary>
        /// add a new widget
        /// </summary>
        /// <param name="widget">widget</param>
        public void Add(Episode episode)
        {
            try
            {
                _dataService.Add(episode);
            }
            catch (Exception e)
            {
                string message = e.Message;
                throw;
            }
        }

        /// <summary>
        /// delete a widget
        /// </summary>
        /// <param name="id">widget id</param>
        public void Delete(int id)
        {
            try
            {
                _dataService.Delete(id);
            }
            catch (Exception e)
            {
                string message = e.Message;
                throw;
            }
        }

        /// <summary>
        /// update a widget
        /// </summary>
        /// <param name="widget">widget</param>
        public void Update(Episode episode)
        {
            try
            {
                _dataService.Update(episode);
            }
            catch (Exception e)
            {
                string message = e.Message;
                throw;
            }
        }

        /// <summary>
        /// required if class will be use in a 'using" block
        /// </summary>
        public void Dispose()
        {
            _dataService = null;
            _episodes = null;
        }
    }
}

