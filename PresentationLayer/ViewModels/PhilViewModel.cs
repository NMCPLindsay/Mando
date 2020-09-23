using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

using MandalorianDB.DataLayer;
using MandalorianDB.Models;
using MandalorianDB.BusinessLayer;

using System.Windows.Controls;
using System.Collections.ObjectModel;


using System.Net.NetworkInformation;

namespace MandalorianDB.PresentationLayer
{

    public class PhilViewModel : ObservableObject
    {
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonEditCommand { get; set; }
        public ICommand ButtonDeleteCommand { get; set; }
        public ICommand ButtonSearchCommand { get; set; }
        public ICommand ButtonQuitCommmand { get; set; }
        public ICommand RadioCommandSortAsc { get; set; }
        public ICommand RadioCommandSortDesc { get; set; }
        public ICommand RadioCommandSearchCrit { get; set; }

        private ObservableCollection<Episode> _episodes;
        private string _criteriaFilter;

        public string CriteriaFilter
        {
            get { return _criteriaFilter; }
            set { _criteriaFilter = value; }
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




        private Episode _selectedEpisode;
        private string _searchName;

        public string SearchName
        {
            get { return _searchName; }
            set 
            { 
                _searchName = value;
                ;
            }
        }



        public Episode SelectedEpisode
        {
            get { return _selectedEpisode; }
            set
            { 
                _selectedEpisode = value;
                OnPropertyChanged(nameof(SelectedEpisode));
            }
        }
        public PhilViewModel()
        {
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            
            if (Episodes.Any()) SelectedEpisode = Episodes[0];

            ButtonAddCommand = new RelayCommand(new Action<object>(AddEpisode));
           // ButtonEditCommand = new RelayCommand(new Action<object>(EditEpisode));
            RadioCommandSortAsc = new RelayCommand(new Action<object>(SortAsc));
            RadioCommandSortDesc = new RelayCommand(new Action<object>(SortDesc));
            RadioCommandSearchCrit = new RelayCommand(new Action<object>(SetSearchCriteria));
            ButtonSearchCommand = new RelayCommand(new Action<object>(Search));
            ButtonQuitCommmand = new RelayCommand(new Action<object>(QuitApp));
        }

        private void AddEpisode(object parameter)
        {
            Episode newEpisode = new Episode();
            Window window = new PhilAddView();
            window.Show();
           
        }

        private void SetSearchCriteria(object parameter)
        {
            CriteriaFilter = parameter.ToString();
            

            
        }

        private void Search(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());
            SearchName = parameter.ToString().Replace("System.Windows.Controls.TextBox: ", "");
            if (!(CriteriaFilter is null))
            {
                switch (CriteriaFilter.ToUpper())
                {
                    case "DIRECTOR":
                        for (int i = Episodes.Count - 1; i >= 0; i--)
                        {
                            Episode episode = Episodes[i];
                            if (episode.Director!=SearchName)
                            {
                                Episodes.RemoveAt(i);
                            }
                        }
                        break;
                    case "WRITER":
                        for (int i = Episodes.Count - 1; i >= 0; i--)
                        {
                            Episode episode = Episodes[i];
                            if (episode.Writer!=SearchName)
                            {
                                Episodes.RemoveAt(i);
                            }
                        }
                        break;
                    case "CHARACTER":
                        for (int i = Episodes.Count - 1; i >= 0; i--)
                        {
                            Episode episode = Episodes[i];
                            if (!(episode.Characters.Contains(SearchName)))
                            {
                                Episodes.RemoveAt(i);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a search criteria.");
            }
            
            

         
        }

        private void SortAsc(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(Episodes.OrderBy(x => x.EpisodeNumber).ToList());
            
            
        }

        private void QuitApp(object parameter)
        {
            Application.Current.Shutdown();
        }

        public void SortDesc(object parameter)
        {
            Episodes = new ObservableCollection<Episode>(Episodes.OrderByDescending(x => x.EpisodeNumber).ToList());

           



        }
    }
}
