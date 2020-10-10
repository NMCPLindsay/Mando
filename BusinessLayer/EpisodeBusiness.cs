using MandalorianDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MandalorianDB.DataLayer;


namespace MandalorianDB.BusinessLayer
{
    class EpisodeBusiness
    {
        public FileIoMessage FileIoStatus { get; set; }

        public EpisodeBusiness()
        {
            //
            // TODO (Demo Mode) - load seed data to database
            //
            MongoDbUtilities.WriteSeedDataToDatabase();
            //SqlUtilities.WriteSeedDataToDatabase();
        }

        /// <summary>
        /// retrieve a widget using the repository
        /// </summary>
        /// <returns>widget</returns>
        private Episode GetEpisode(int id)
        {
            Episode episode = null;
            FileIoStatus = FileIoMessage.None;

            try
            {
                using (EpisodeRepository eRepository = new EpisodeRepository())
                {
                    episode = eRepository.GetById(id);
                };

                if (episode != null)
                {
                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }

            return episode;
        }

        /// <summary>
        /// retrieve a list of all widgets using the repository
        /// </summary>
        /// <returns>all widgets</returns>
        private List<Episode> GetAllEpisodes()
        {
            List<Episode> episodes = null;
            FileIoStatus = FileIoMessage.None;

            try
            {
                using (EpisodeRepository eRepository = new EpisodeRepository())
                {
                    episodes = eRepository.GetAll() as List<Episode>;
                };

                if (episodes != null)
                {
                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.NoRecordsFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }

            return episodes;
        }

        /// <summary>
        /// provide a list of all widgets
        /// </summary>
        /// <returns>list of all widgets</returns>
        public List<Episode> AllEpisodes()
        {
            //
            // TODO (Demo Mode) - switch between seed data and persistence
            // Note: disable the business layer and run the method below

            //return SeedData.GetAllWidgets();


            return GetAllEpisodes() as List<Episode>;
        }

        /// <summary>
        /// retrieve a widget by id 
        /// </summary>
        /// <param name="id">widget id</param>
        /// <returns>widget</returns>
        public Episode EpisodeById(int id)
        {
            return GetEpisode(id);
        }

        /// <summary>
        /// add a new widget
        /// </summary>
        /// <param name="widget">widget to add</param>
        public void AddEpisode(Episode episode)
        {
            try
            {
                if (episode != null)
                {
                    using (EpisodeRepository eRepository = new EpisodeRepository())
                    {
                        eRepository.Add(episode);
                    };

                    FileIoStatus = FileIoMessage.Complete;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }

        /// <summary>
        /// update a widget
        /// </summary>
        /// <param name="updatedWidget">updated widget</param>
        public void UpdateEpisode(Episode updatedEpisode)
        {
            try
            {
                if (GetEpisode(updatedEpisode.Id) != null)
                {
                    using (EpisodeRepository repo = new EpisodeRepository())
                    {
                        repo.Update(updatedEpisode);
                    }

                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }

        /// <summary>
        /// retrieve a widget by id 
        /// </summary>
        /// <param name="id">widget id</param>
        public void DeleteEpisode(int id)
        {
            try
            {
                if (GetEpisode(id) != null)
                {
                    using (EpisodeRepository eRepository = new EpisodeRepository())
                    {
                        eRepository.Delete(id);
                    }

                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }
    }
}
