using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MandalorianDB.BusinessLayer;
using MandalorianDB.DataLayer;
using System.Collections.Generic;
using MandalorianDB.PresentationLayer.Views;

namespace MandalorianDB.PresentationLayerViewModel
{

    class JessViewModel : ObservableObject
    {

        #region Commands
        public ICommand EditEpisodeCommand { get; set; }

        public ICommand AddEpisodeCommand { get; set; }

        public ICommand DeleteEpisodeCommand { get; set; }

        public ICommand QuitApplicationCommand { get; set; }

        public ICommand RadioCommandSortAsc { get; set; }

        public ICommand RadioCommandSortDirector { get; set; }

        public ICommand ButtonSearchCommand { get; set; }

        #endregion

        #region Field
        private ObservableCollection<Episode> _episodes;
        private Episode _selectedEpisode;
        private Episode _episodeToAdd;
       
        private string _episodeOperationFeedback;
        
        private Episode _episodeToEdit;
        private string _searchText;
        private string _minEpisodeText;
        private string _maxEpisodeText;

        #endregion
        
        #region Properties

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public string MaxEpisodeText
        {
            get { return _maxEpisodeText; }
            set
            {
                _maxEpisodeText = value;
                OnPropertyChanged(nameof(MaxEpisodeText));
            }
        }


        public string MinEpisodeText
        {
            get { return _minEpisodeText; }
            set
            {
                _minEpisodeText = value;
                OnPropertyChanged(nameof(MinEpisodeText));
            }
        }
        public string EpisodeOperationFeedback
        {
            get { return _episodeOperationFeedback; }
            set
            {
                _episodeOperationFeedback = value;
                OnPropertyChanged(nameof(EpisodeOperationFeedback));
            }
        }
        public Episode EpisodeToEdit
        {
            get { return _episodeToEdit; }
            set
            {
                _episodeToEdit = value;
                OnPropertyChanged(nameof(EpisodeToEdit));
            }
        }
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
        #endregion

        #region Method 
        public JessViewModel()
        {
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            if (Episodes.Any()) SelectedEpisode = Episodes[0];

            AddEpisodeCommand = new RelayCommand(new Action<object>(OnAddEpisode));
            EditEpisodeCommand  = new RelayCommand(new Action<object>(OnEditEpisode));
            RadioCommandSortAsc = new RelayCommand(new Action<object>(OnSortAsc));
            RadioCommandSortDirector = new RelayCommand(new Action<object>(OnSortDirector));
            ButtonSearchCommand = new RelayCommand(new Action<object>(Search));
            QuitApplicationCommand = new RelayCommand(new Action<object>(OnQuitApplication));
            DeleteEpisodeCommand = new RelayCommand(new Action<object>(OnDeleteEpisode));
        }
         
        #endregion
       
       
        private void Search(object parameter)
        {
            //reset filter
            MinEpisodeText = "";
            MaxEpisodeText = "";

            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());

            _episodes = new ObservableCollection<Episode>(_episodes.Where(x => x.Writer.ToLower().Contains(_searchText)));

            }
           
        private void OnEpisodeFilterEpisodeList()
        {
            //reset search
            SearchText = "";
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            if(int.TryParse(MinEpisodeText, out int minEpisode) && int.TryParse(MaxEpisodeText,out int maxEpisode))
            {
                

                Episodes = new ObservableCollection<Episode>(Episodes.Where(x => x.EpisodeNumber >= minEpisode && x.EpisodeNumber <= maxEpisode));
            }
        }
        private void OnEditEpisode(object parameter)
        {
            string commandParameter = parameter.ToString();

            if (commandParameter == "SAVE")
            {
                if (EpisodeToEdit != null)
                {
                    Episode episodeToDelete = SelectedEpisode;
                    Episodes.Add(EpisodeToEdit);
                    SelectedEpisode = EpisodeToEdit;
                    Episodes.Remove(episodeToDelete);

                    EpisodeOperationFeedback = "Episode Updated";
                }
            }
            else if (commandParameter == "CANCEL")
            {
               // EpisodeToEdit = SelectedEpisode.Copy();
                EpisodeOperationFeedback = "Episode Update Canceled";
            }
            else
            {
                throw new ArgumentException($"{commandParameter} is not a valid command parameter for the adding Episodes.");
            }
        }
        private void OnAddEpisode(object parameter)
        {
            string commandParameter = parameter.ToString();

            if (commandParameter == "SAVE")
            {
                if (EpisodeToAdd != null)
                {
                   Episodes.Add(EpisodeToAdd);
                    EpisodeOperationFeedback = "New Episode Added";
                    SelectedEpisode = EpisodeToAdd;
                }
            }
            else if (commandParameter == "CANCEL")
            {
                EpisodeOperationFeedback = "New Episode Canceled";
            }
            else
            {
                throw new ArgumentException($"{commandParameter} is not a valid command parameter for the adding Episodes.");
            }
            EpisodeToAdd = new Episode();
        }
    
        private void OnDeleteEpisode(object parameter)
        {
            if ( SelectedEpisode != null)
            {
                string episodeName = SelectedEpisode.Name;

                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the {episodeName} episode from series?", "Delete Episode", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Episodes.Remove(SelectedEpisode);
                        EpisodeOperationFeedback = "Episode Deleted";

                        if (Episodes.Any()) SelectedEpisode = Episodes[0];
                        break;

                    case MessageBoxResult.No:
                        EpisodeOperationFeedback = "Episode Deletion Canceled";
                        break;
                }
            }
        }
        private void OnQuitApplication(object parameter)
        {
            //
            //  house keeping method 
            //
            System.Environment.Exit(1);
        }
        private void OnSortAsc(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(Episodes.OrderBy(x => x.EpisodeNumber));
        }
        private void OnSortDirector(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(Episodes.OrderBy(x => x.Director));
        }

    }
}
