using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MandalorianDB.BusinessLayer;
using MandalorianDB.DataLayer;
using System.Collections.Generic;
using MandalorianDB.PresentationLayer.Views;
using MandalorianDB.Models;
using System.Security.Cryptography.X509Certificates;

namespace MandalorianDB.PresentationLayer.ViewModels
{

    class JessViewModel : ObservableObject
    {
        #region ENUMS
        private enum OperationStatus
        {
            NONE,
            VIEW,
            ADD,
            EDIT,
            DELETE
           
        }

      
        #endregion

        #region Commands
        public ICommand EditEpisodeCommand { get; set; }

        public ICommand AddEpisodeCommand { get; set; }

        public ICommand DeleteEpisodeCommand { get; set; }

        public ICommand QuitApplicationCommand { get; set; }

        public ICommand EpisodeFilterListCommand { get; set; }
        // TODO Velis
        public ICommand RadioCommandSortEpisodeList { get; set; }

        public ICommand ButtonSearchCommand { get; set; }

        public ICommand ResetEpisodeListCommand { get; set; }

        public ICommand SaveEpisodeCommand { get; set; }

        public ICommand CancelEpisodeCommand { get; set; }

        public ICommand AddCharCommand { get; set; }

        public ICommand DeleteCharCommand { get; set; }

        public ICommand SaveCharCommand { get; set; }

        #endregion

        #region Field
        
        private OperationStatus _operationStatus = OperationStatus.NONE;
       
        private ObservableCollection<Episode> _episodes;
        private Episode _selectedEpisode;
        private EpisodeBusiness _episodeBusiness;
        private Episode _detailedViewEpisode;
        private string _addNewChar;
        private string _sortType;
        private string _searchText;
        private string _minEpisodeText;
        private string _maxEpisodeText;
        private string _selectedChar;        

        private bool _isEditingAdding = false;
        private bool _showAddEditDeleteButtons = true;
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
        public string NewChar 
        { get { return _addNewChar; }
            set 
            {
                _addNewChar = value;
                OnPropertyChanged(nameof(NewChar));
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
      
        public ObservableCollection<Episode> Episodes
        {
            get { return _episodes; }
            set
            {
                _episodes = value;
                OnPropertyChanged(nameof(Episodes));
            }
        }

        public Episode SelectedEpisode
        {
            get { return _selectedEpisode; }
            set
            {
                if (value != null)
                {
                    _selectedEpisode = value;
                    OnPropertyChanged(nameof(SelectedEpisode));
                    UpdateDetailedViewEpisodeToSelected();
                }
            }
        }

        public Episode DetailedViewEpisode
        {
            get { return _detailedViewEpisode; }
            set
            {
                if(value != null)
                {
                    _detailedViewEpisode = value;
                    OnPropertyChanged(nameof(DetailedViewEpisode));
                }
            }
        }
        public bool IsEditingAdding
        {
            get { return _isEditingAdding; }
            set
            {
                _isEditingAdding = value;
                OnPropertyChanged(nameof(IsEditingAdding));
            }
        }

        public bool ShowAddEditDeleteButtons
        {
            get { return _showAddEditDeleteButtons; }
            set
            {
                _showAddEditDeleteButtons = value;
                OnPropertyChanged(nameof(ShowAddEditDeleteButtons));
            }
        }
        public string SortType
        {
            get { return _sortType; }
            set { _sortType = value; }
        }

        public string SelectedChar
        {
            get { return _selectedChar; }
            set
            {
                _selectedChar = value;
                OnPropertyChanged(nameof(SelectedChar));
            }
        }
        #endregion

        #region Constructor
        public JessViewModel()
        {

            _episodeBusiness = new EpisodeBusiness();
           Episodes = new ObservableCollection<Episode>(_episodeBusiness.AllEpisodes());

            //
            //start at first in list
            //
            _selectedEpisode = _episodes[0];
            UpdateDetailedViewEpisodeToSelected();

           //Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
           if (Episodes.Any()) SelectedEpisode = Episodes[0];

            AddEpisodeCommand = new RelayCommand(new Action<object>(OnAddEpisode));
            EditEpisodeCommand = new RelayCommand(new Action<object>(OnEditEpisode));
            RadioCommandSortEpisodeList = new RelayCommand(new Action<object>(SortEpisodeList));
            ButtonSearchCommand = new RelayCommand(new Action<object>(Search));
            QuitApplicationCommand = new RelayCommand(new Action<object>(OnQuitApplication));
            DeleteEpisodeCommand = new RelayCommand(new Action<object>(OnDeleteEpisode));
            EpisodeFilterListCommand = new RelayCommand(new Action<object>(OnEpisodeFilterEpisodeList));
            ResetEpisodeListCommand = new RelayCommand(new Action<object>(OnResetList));
            SaveEpisodeCommand = new RelayCommand(new Action<object>(OnSaveEpisode));
            CancelEpisodeCommand = new RelayCommand(new Action<object>(OnCancelEpisode));
            AddCharCommand = new RelayCommand(new Action<object>(OnAddChar));
            DeleteCharCommand = new RelayCommand(new Action<object>(OnDeleteChar));
          
        }

        #endregion

        #region Method 
        private void OnResetList(object parameter)
        {
            // reseting filters
            SearchText = "";
            MinEpisodeText = "";
            MaxEpisodeText = "";
          //_episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            Episodes = new ObservableCollection<Episode>(_episodeBusiness.AllEpisodes());
           
        }
        private void Search(object parameter)
        {
            //reset filter
            MinEpisodeText = "";
            MaxEpisodeText = "";

            //Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            Episodes = new ObservableCollection<Episode>(_episodeBusiness.AllEpisodes());

            Episodes = new ObservableCollection<Episode>(_episodes.Where(x => x.Writer.ToLower().Contains(_searchText)));

        }
        private void OnEpisodeFilterEpisodeList(object parameter)
        {
            //reset search
            SearchText = "";
          // Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
             Episodes = new ObservableCollection<Episode>(_episodeBusiness.AllEpisodes());

            if (int.TryParse(MinEpisodeText, out int minEpisode) && int.TryParse(MaxEpisodeText, out int maxEpisode))
            {
                //
                //reset list
                //
               
                Episodes = new ObservableCollection<Episode>(_episodes.Where(x => x.EpisodeNumber >= minEpisode && x.EpisodeNumber <= maxEpisode));
            }
        }
        private void SortEpisodeList(object parameter)
        {
            string sortType = parameter.ToString();
            switch (sortType)
            {
                case "Director":
                    Episodes = new ObservableCollection<Episode>(Episodes.OrderBy(x => x.Director));
                    break;

                case "Episode":
                    Episodes = new ObservableCollection<Episode>(Episodes.OrderBy(x => x.EpisodeNumber));
                    break;

                default:

                    break;
            }

        }
        private void OnDeleteChar(object parameter)
        {
            if(_selectedChar != null)
            {
               MessageBoxResult result = System.Windows.MessageBox.Show($"Are you sure you want to delete {parameter}?", "Delete Character", System.Windows.MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                   DetailedViewEpisode.Characters.Remove(parameter.ToString());
                    if (DetailedViewEpisode.Characters.Any())
                    {
                        SelectedChar = DetailedViewEpisode.Characters[0];
                    }
                }
                else
                {
                    result = MessageBoxResult.Cancel;
                    _operationStatus = OperationStatus.NONE;
                }
            }
        }
        private void OnAddChar(object parameter)
        {
            IsEditingAdding = true;
            ShowAddEditDeleteButtons = false;
            DetailedViewEpisode.Characters.Add(NewChar);
        }
       
        private void OnAddEpisode(object parameter)
        {
            ResetDetailedViewEpisode();
            IsEditingAdding = true;
            ShowAddEditDeleteButtons = false;
            _operationStatus = OperationStatus.ADD;
        }
        private void OnEditEpisode(object parameter)
        {
            if (_selectedEpisode != null)
            {
                 MessageBoxResult result = System.Windows.MessageBox.Show($"Are you sure you want to edit {_selectedEpisode.Name}?", "Edit epsiode", System.Windows.MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                     _operationStatus = OperationStatus.EDIT;
                     IsEditingAdding = true;
                     ShowAddEditDeleteButtons = false;
                        break;

                    case MessageBoxResult.Cancel:
                    _operationStatus = OperationStatus.NONE;
                        break;
                    default:
                        break;
                }
            }
         }
        private void OnDeleteEpisode(object parameter)
        {
           if(_selectedEpisode != null)
            {
                MessageBoxResult result= System.Windows.MessageBox.Show($"Are you sure you want to delete {_selectedEpisode.Name}?", "Delete epsiode", System.Windows.MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    //
                    //deletes from persistence
                    //
                    _episodeBusiness.DeleteEpisode(SelectedEpisode.Id);
                    //
                    //removes episode from list
                    //
                    
                    _episodes.Remove(_selectedEpisode);
                    //
                    //set selected episode to first on list
                    //
                    SelectedEpisode = _episodes[0];
                }
                else
                {
                    result = MessageBoxResult.Cancel;
                    _operationStatus = OperationStatus.NONE;
                }
            }
        }
        private void OnQuitApplication(object parameter)
        {
            //
            //  house keeping method 
            //
            // System.Environment.Exit(1);
            Application.Current.Shutdown();
        }
        private void OnSaveEpisode(object parameter)
        {
            switch(_operationStatus)
            {
                case OperationStatus.ADD:
                    if(_detailedViewEpisode != null)
                    {
                        //
                        // adding episode in persistence
                        //
                        _episodeBusiness.AddEpisode(_detailedViewEpisode);
                        //
                        // add episode to list
                        //
                        _episodes.Add(DetailedViewEpisode);
                        //
                        // seting selected episode as new episode
                        //
                        SelectedEpisode = DetailedViewEpisode;
                    }
                    break;
                case OperationStatus.EDIT:
                    Episode episodeToUpdate = _episodes.FirstOrDefault(c => c.Id == SelectedEpisode.Id);

                    if(episodeToUpdate!=null)
                    {
                        Episode updatedEpisode = DetailedViewEpisode;
                        //
                        // update episode in persistence
                        //
                        _episodeBusiness.UpdateEpisode(updatedEpisode);
                        //
                        //update episode in list
                        //
                        _episodes.Remove(episodeToUpdate);
                        _episodes.Add(updatedEpisode);
                        //
                        // setting selected episode as property
                        //
                        SelectedEpisode = updatedEpisode;
                    }
                    break;
                default:
                    break;
            }
            IsEditingAdding = false;
            ShowAddEditDeleteButtons = true;
            _operationStatus = OperationStatus.NONE;
        }
        private void UpdateDetailedViewEpisodeToSelected()
        {
            Episode tempEpisode = new Episode();
            tempEpisode.Id = _selectedEpisode.Id;
            tempEpisode.EpisodeNumber = _selectedEpisode.EpisodeNumber;
            tempEpisode.Name = _selectedEpisode.Name;
            tempEpisode.SeasonNumber = _selectedEpisode.SeasonNumber;
            tempEpisode.Characters = _selectedEpisode.Characters;
            tempEpisode.EpisodeDetails = _selectedEpisode.EpisodeDetails;
            tempEpisode.Director = _selectedEpisode.Director;
            tempEpisode.Writer = _selectedEpisode.Writer;
            DetailedViewEpisode = tempEpisode;
        }
        private void ResetDetailedViewEpisode()
        {
            DetailedViewEpisode = new Episode();
            DetailedViewEpisode.Characters = new ObservableCollection<string>();
        }
        private void OnCancelEpisode(object parameter)
        {
            if (_operationStatus == OperationStatus.ADD)
            {
                SelectedEpisode = _episodes[0];
            }
            _operationStatus = OperationStatus.NONE;
            IsEditingAdding = false;
            ShowAddEditDeleteButtons = true;
        }
        #endregion

        #region Events

        #endregion
    }
}
