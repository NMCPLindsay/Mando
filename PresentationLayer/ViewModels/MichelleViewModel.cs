namespace MandalorianDB.PresentationLayer
{
    #region USINGS
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using MandalorianDB.BusinessLayer;
    using MandalorianDB.DataLayer;
    using MandalorianDB.Models;
    using MandalorianDB.PresentationLayer.ViewModels;
    using MandalorianDB.PresentationLayer.Views;
    #endregion

    public class MichelleViewModel : ObservableObject
    {
        #region CONSTRUCTOR
        private EpisodeOperation _episodeOperation;
        public MichelleViewModel()
        {
            // this.Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            EpisodeBusiness episodeBusiness = new EpisodeBusiness();
            this.Episodes = new ObservableCollection<Episode>(episodeBusiness.AllEpisodes());

            this.ButtonAddCommand = new RelayCommand(this.AddEpisode);
            this.ButtonEditCommand = new RelayCommand(this.EditEpisode);
            this.ButtonDeleteCommand = new RelayCommand(this.DeleteEpisode);
            this.SelectedFilterCommand = new RelayCommand(this.SetFilter);
            this.RadioCommandSortAsc = new RelayCommand(this.SortAsc);
            this.RadioCommandSortDesc = new RelayCommand(this.SortDesc);
            this.RadioCommandSearchCrit = new RelayCommand(this.SetSearchCriteria);
            this.ButtonSearchCommand = new RelayCommand(this.Search);

        }
        #endregion

        #region INTERFACES
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonEditCommand { get; set; }
        public ICommand ButtonDeleteCommand { get; set; }
        public ICommand SelectedFilterCommand { get; set; }
        public ICommand ButtonSearchCommand { get; set; }
        public ICommand RadioCommandSortAsc { get; set; }
        public ICommand RadioCommandSortDesc { get; set; }
        public ICommand RadioCommandSearchCrit { get; set; }
        #endregion

        private ObservableCollection<Episode> _episodes;

        private string _criteriaFilter;
        private ComboBoxItem _selectedFilter;
        private Episode _selectedEpisode;

        public ObservableCollection<Episode> Episodes
        {
            get { return _episodes; }
            set
            {
                this._episodes = value;
                this.OnPropertyChanged(nameof(Episodes));
            }
        }

        public string CriteriaFilter
        {
            get { return _criteriaFilter; }
            set { _criteriaFilter = value; }
        }
        public string SearchName { get; set; }
        public System.Windows.Controls.ComboBoxItem SelectedFilter
        {
            get => this._selectedFilter;
            set
            {
                this._selectedFilter = value;
                this.SetFilter(value);
            }
        }

        public Episode SelectedEpisode
        {
            get => this._selectedEpisode;
            set
            {
                this._selectedEpisode = value;
                this.OnPropertyChanged(nameof(this.SelectedEpisode));
            }
        }

        #region EVENTS
        private void AddEpisode(object parameter)
        {
            Episode newEpisode = new Episode();
            var mmvm = new MichelleManageViewModel(newEpisode);
            var win = new MichelleManageView { DataContext = mmvm };
            {
                win.Closed += (s, eventarg) =>
                {
                    Episodes.Add(newEpisode);
                    EpisodeBusiness episodeBusiness = new EpisodeBusiness();
                    episodeBusiness.AddEpisode(newEpisode);
                };
                // win.DataChanged += AddWindow_DataChanged;
                win.Show();
            }
        }

        //private void AddWindow_DataChanged(object sender, EventArgs e)
        //{
        //    EpisodeBusiness episodeBusiness = new EpisodeBusiness();
        //    this.Episodes = new ObservableCollection<Episode>(episodeBusiness.AllEpisodes());
        //}

        private void EditEpisode(object parameter)
        {
            if (this.SelectedEpisode == null)
            {
                MessageBox.Show(
                    "Please select an episode.",
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            else
            {
                var mmvm = new MichelleManageViewModel(this.SelectedEpisode);
                var win = new MichelleManageView { DataContext = mmvm };

                win.Show();
            }
        }

        private void DeleteEpisode(object parameter)
        {
            if (parameter == "We made a mistake")
            {
                MessageBox.Show("Please select an episode.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (MessageBox.Show("Are you sure that you want to delete this episode?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    EpisodeBusiness episodeBusiness = new EpisodeBusiness();
                    var episode = parameter as Episode;
                    episodeBusiness.DeleteEpisode(episode.Id);
                    this.Episodes.Remove(parameter as Episode);
                }
            }
        }

        private void Search(object parameter)
        {
            // this.Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            EpisodeBusiness episodeBusiness = new EpisodeBusiness();
            this.Episodes = new ObservableCollection<Episode>(episodeBusiness.AllEpisodes());
            this.SearchName = (parameter as TextBox).Text;

            if (!(this.CriteriaFilter is null))
            {
                switch (this.CriteriaFilter.ToUpper())
                {
                    case "DIRECTOR":
                        for (var i = this.Episodes.Count - 1; i >= 0; i--)
                        {
                            var episode = this.Episodes[i];

                            if (!string.Equals(episode.Director.Trim().ToUpper(), this.SearchName.Trim().ToUpper(), StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.Episodes.RemoveAt(i);
                            }
                        }

                        break;
                    case "WRITER":
                        for (var i = this.Episodes.Count - 1; i >= 0; i--)
                        {
                            var episode = this.Episodes[i];

                            if (!string.Equals(episode.Writer.Trim(), this.SearchName.Trim(), StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.Episodes.RemoveAt(i);
                            }
                        }

                        break;
                    case "CHARACTER":
                        for (var i = this.Episodes.Count - 1; i >= 0; i--)
                        {
                            var episode = this.Episodes[i];

                            if (!episode.Characters.Contains(this.SearchName))
                            {
                                this.Episodes.RemoveAt(i);
                            }
                        }

                        break;
                }
            }
        }

        private void SetFilter(object parameter)
        {
            switch (this.SelectedFilter.Content)
            {
                case "Director":
                    this.Episodes = new ObservableCollection<Episode>(this.Episodes.OrderBy(e => e.Director));

                    break;
                case "Writer":
                    this.Episodes = new ObservableCollection<Episode>(this.Episodes.OrderBy(e => e.Writer));

                    break;
                case "Characters":
                    // NB: It is very odd to order by a list in this way.
                    // Because you can't order a by a list, convert each list of chars to a csv.
                    this.Episodes = new ObservableCollection<Episode>(this.Episodes.OrderBy(e => string.Join(",", e.Characters)));

                    break;
            }
        }

        private void SetSearchCriteria(object parameter)
        {
            this.CriteriaFilter = parameter.ToString();
        }

        private void SortAsc(object parameter)
        {
            this.Episodes = new ObservableCollection<Episode>(Episodes.OrderBy(x => x.EpisodeNumber).ToList());
        }

        private void SortDesc(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(Episodes.OrderByDescending(x => x.EpisodeNumber).ToList());
        }
        #endregion
    }
}
