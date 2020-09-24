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
using MandalorianDB.Model;

namespace MandalorianDB.ViewModel
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
        private Episode _searchWriter;
        private string _episodeOperationFeedback;
        private string _searchName;
        private Episode _episodeToEdit;


        #region Properties

        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                ;
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

        public Episode SearchWriter
        {
            get { return _searchWriter; }
            set { _searchWriter = value; }

        }

        #endregion

        #region Method 
        public JessViewModel()
        {
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            if (Episodes.Any()) SelectedEpisode = Episodes[0];

            AddEpisodeCommand = new RelayCommand(new Action<object>(OnAddEpisode));
            EditEpisodeCommand = new RelayCommand(new Action<object>(OnEditEpisode));
            RadioCommandSortAsc = new RelayCommand(new Action<object>(OnSortAsc));
            RadioCommandSortDirector = new RelayCommand(new Action<object>(OnSortDirector));
            ButtonSearchCommand = new RelayCommand(new Action<object>(Search));
            QuitApplicationCommand = new RelayCommand(new Action<object>(OnQuitApplication));
            DeleteEpisodeCommand = new RelayCommand(new Action<object>(OnDeleteEpisode));
        }
        #endregion

        private void Search(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            SearchName = parameter.ToString().Replace("System.Windows.Controls.TextBox: ", "");
            string writer = parameter.ToString();
            if (writer != null)
            {
                for (int i = Episodes.Count - 1; i >= 0; i--)
                {
                    Episode episode = Episodes[i];
                    if (episode.Writer != SearchName)
                    {
                        Episodes.RemoveAt(i);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a search criteria.");
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
                EpisodeToEdit = SelectedEpisode.Copy();
                EpisodeOperationFeedback = "Episode Update Canceled";
            }
            else
            {
                throw new ArgumentException($"{commandParameter} is not a valid command parameter for the adding widgets.");
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
                throw new ArgumentException($"{commandParameter} is not a valid command parameter for the adding widgets.");
            }
            EpisodeToAdd = new Episode();
        }

        private void OnDeleteEpisode(object parameter)
        {
            if (SelectedEpisode != null)
            {
                string episodeName = SelectedEpisode.Name;

                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the {episodeName} widgets from inventory?", "Delete Widgets", MessageBoxButton.YesNo);

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
            // call a house keeping method in the business class
            //
            System.Environment.Exit(1);
        }
        private void OnSortAsc(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(_episodes.OrderBy(x => x.EpisodeNumber));
        }
        private void OnSortDirector(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(_episodes.OrderBy(x => x.Director));
        }

        #endregion

    }
}
