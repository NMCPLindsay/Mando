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

        private ObservableCollection<Episode> _episodes;

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
        private string _charName;

        public string CharacterName
        {
            get { return _charName; }
            set { _charName = value; }
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

           //ButtonAddCommand = new RelayCommand(new Action<object>(AddEpisode()));
           // ButtonEditCommand = new RelayCommand(new Action<object>(EditEpisode()));
            RadioCommandSortAsc = new RelayCommand(new Action<object>(SortAsc));
            RadioCommandSortDesc = new RelayCommand(new Action<object>(SortDesc));
            //ButtonSearchCommand = new RelayCommand(new Action<object>(Search));
            ButtonQuitCommmand = new RelayCommand(new Action<object>(QuitApp));
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
