using MandalorianDB.Models;
using MandalorianDB.DataLayer;
using MandalorianDB.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using MandalorianDB.PresentationLayer.Views;

namespace MandalorianDB.PresentationLayer
{
    public class PhilAddViewModel : ObservableObject
    {

        public ICommand ButtonAddCharCommand { get; set; }
        public ICommand ButtonDelCharCommand { get; set; }
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonCancelCommand { get; set; }
        public Episode NewEpisode { get; set; }
        private EpisodeOperation _episodeOperation;
        private string _selectedChar;

        public string SelectedChar
        {
            get { return _selectedChar; }
            set { _selectedChar = value;
                OnPropertyChanged(nameof(SelectedChar)); }
        }

        public PhilAddViewModel(EpisodeOperation episodeOperation)
        {
            NewEpisode = episodeOperation.Episode;
            _episodeOperation = episodeOperation;
            ButtonAddCommand = new RelayCommand(new Action<object>(AddEpisode));
            ButtonCancelCommand = new RelayCommand(new Action<object>(CancelAddEpisode));
        }

        private void CancelAddEpisode(object parameter)
        {
            _episodeOperation.Status = EpisodeOperation.OperationStatus.CANCEL;

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }

        private void AddEpisode(object parameter)
        {
            _episodeOperation.Status = EpisodeOperation.OperationStatus.OKAY;

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }


    }
}
