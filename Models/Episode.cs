using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MandalorianDB.BusinessLayer;
using MandalorianDB.DataLayer;

namespace MandalorianDB.Models

{
    public class Episode : ObservableObject
    {
        private int _episodeNumber;
        private string _name;
        private int _seasonNumber;
        private List<string> _characters;
        private string _episodeDetails;
        private string _director;
        private string _writer;
        public int Id { get; set; }
        public string Writer
        {
            get { return _writer; }
            set { _writer = value; }
        }


        public string Director
        {
            get { return _director; }
            set { _director = value; }
        }


        public string EpisodeDetails
        {
            get { return _episodeDetails; }
            set { _episodeDetails = value; }
        }


        public List<string> Characters
        {
            get { return _characters; }
            set { _characters = value; }
        }

        public int SeasonNumber
        {
            get { return _seasonNumber; }
            set { _seasonNumber = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public int EpisodeNumber
        {
            get { return _episodeNumber; }
            set { _episodeNumber = value; }
        }

        public Episode()
        {

        }

        public Episode(int epNum, string name, int seaNum, List<string> chars, string epDets, string director, string writer) 
        {
            EpisodeNumber=epNum;
            Name=name;
            SeasonNumber=seaNum;
            Characters = chars;
            EpisodeDetails = epDets;
            Director = director;
            Writer = writer;
        }
        
    }
}
