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

namespace MandalorianDB.PresentationLayer.ViewModels
{
    public class PhilAddCharViewModel : ObservableObject
    {
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonCancelCommand { get; set; }
        public Episode Episode { get; set; }
        private EpisodeOperation _episodeOperation;
        public PhilAddCharViewModel(EpisodeOperation episodeOp)
        {
            Episode = episodeOp.Episode;
            _episodeOperation = episodeOp;
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
