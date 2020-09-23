using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using MandalorianDB.BusinessLayer;
using MandalorianDB.DataLayer;
using MandalorianDB.Models;

namespace MandalorianDB.PresentationLayer.ViewModels
{

    public class JessViewModel : ObservableObject
    {
        #region Commands
        //public ICommand SearchEpisodeCommand
        //{
        //    get { return new DelegateCommand(OnSearchEpisode); }
        //}
        //public ICommand AddEpisodeCommand
        //{
        //    get { return new DelegateCommand(OnAddEpisode); }
        //}
        //public ICommand DeleteEpisodeCommand
        //{
        //    get { return new DelegateCommand(OnDeleteEpisode); }
        //}

        //public ICommand QuitApplicationCommand
        //{
        //    get { return new DelegateCommand(OnQuitApplication); }
        //}

        //public ICommand SortListByDirectorCommand
        //{
        //    get { return new DelegateCommand(OnSortListByDirector); }
        //}
        #endregion

        #region Field
        private ObservableCollection<Episode> _episodes;
        private Episode _selectedEpisode;
        private Episode _episodeToAdd;
        private Episode _episodeToSearch;

        #endregion

        #region Properties
        public ObservableCollection<Episode> Episodes
        {
            get { return _episodes; }
            set { _episodes = value; }
        }

        public Episode SelectedEpisode
        {
            get { return _selectedEpisode; }
            set
            {
                if (_selectedEpisode == value) { return; }
                _selectedEpisode = value;
                OnPropertyChanged(nameof(SelectedEpisode));
            }
        }
        public Episode EpisodeToAdd
        {
            get { return _episodeToAdd; }
            set
            {
                _episodeToAdd = value;
                OnPropertyChanged(nameof(EpisodeToAdd));
            }

        }
        public Episode EpisodeToSearch
        {
            get { return _episodeToSearch; }
            set { _episodeToSearch = value; }
        }

        #endregion

        #region Method
        private void OnSearchEpisode()
        {
           
          
        }
        private void OnAddEpisode()
        {

        }
        private void OnDeleteEpisode()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Are you Sure you want to delete {_selectedEpisode}", "Deleted Episode", System.Windows.MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                _episodes.Remove(_selectedEpisode);
            }
        }
        private void OnQuitApplication()
        {
            //
            // call a house keeping method in the business class
            //
            System.Environment.Exit(1);
        }
        private void OnSortListByDirector()
        {
            _episodes = new ObservableCollection<Episode>(_episodes.OrderBy(c => c.Director));
        }
        #endregion
        public  JessViewModel()
        {
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            if (Episodes.Any()) SelectedEpisode = Episodes[0];
       }
       
 
    }
}
