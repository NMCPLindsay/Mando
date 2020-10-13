using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

using MandalorianDB.Models;
using MandalorianDB.DataLayer;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using MandalorianDB.BusinessLayer;
using MandalorianDB.PresentationLayer.Views;

namespace MandalorianDB.PresentationLayer
{
    public class PhilEditViewModel : ObservableObject
    {
        public ICommand ButtonSaveCommand { get; set; }
        public ICommand ButtonCancelCommand { get; set; }
        public ICommand ButtonAddCharCommand { get; set; }
        public ICommand ButtonDelCharCommand { get; set; }
        public string NewChar { get; set; }
        public Episode UserEpisode { get; set; }
        private EpisodeOperation _episodeOperation;
        private string _selectedChar;
        private List<string> _chars;

        public List<string> Chars
        {
            get { return _chars; }
            set { _chars = value;
                OnPropertyChanged(nameof(Chars));
            }
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
        public PhilEditViewModel(Episode episode)
        {
            UserEpisode = episode;
            Chars = new List<string>();
            UserEpisode.Characters = Chars;
            



            ButtonSaveCommand = new RelayCommand(new Action<object>(EditEpisode));
            ButtonCancelCommand = new RelayCommand(new Action<object>(CancelEditEpisode));
            ButtonAddCharCommand = new RelayCommand(new Action<object>(AddChar));
            ButtonDelCharCommand = new RelayCommand(new Action<object>(DelChar));
        }
        public PhilEditViewModel(EpisodeOperation episodeOperation)
        {
            UserEpisode = episodeOperation.Episode;
            _episodeOperation = episodeOperation;
            
            
            ButtonSaveCommand = new RelayCommand(new Action<object>(EditEpisode));
            ButtonCancelCommand = new RelayCommand(new Action<object>(CancelEditEpisode));
            ButtonAddCharCommand = new RelayCommand(new Action<object>(AddChar));
            ButtonDelCharCommand = new RelayCommand(new Action<object>(DelChar));
        }

        private void DelChar(object parameter)
        {
            if (parameter != null)
            {
                

                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the {parameter} character from inventory?", "Delete Character", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Chars.Remove(parameter.ToString());
                        UserEpisode.Characters = Chars;
                        MessageBox.Show($"{parameter} character Deleted", "Delete Character");

                        if (UserEpisode.Characters.Any())
                        {
                            SelectedChar = UserEpisode.Characters[0];
                        }
                        break;

                    case MessageBoxResult.No:
                        MessageBox.Show($"{parameter} Character Deletion Canceled", "Delete Character");
                        break;
                }
            }
        }

        private void AddChar(object parameter)
        {
            NewChar = parameter.ToString().Replace("System.Windows.Controls.TextBox: ", "");
            if (NewChar != null)
            {
                Chars = UserEpisode.Characters.ToList();
                Chars.Add(NewChar);
                
                UserEpisode.Characters = Chars;
            }
            else
            {
                MessageBox.Show("Please input a character to add.");
            }
        }

        public void EditEpisode(object parameter)
        {
            //
            // TODO - validate user inputs
            //
            EpisodeBusiness episodeBusiness = new EpisodeBusiness();
            episodeBusiness.UpdateEpisode(UserEpisode);
            _episodeOperation.Status = EpisodeOperation.OperationStatus.OKAY;

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }

        public void CancelEditEpisode(object parameter)
        {
            _episodeOperation.Status = EpisodeOperation.OperationStatus.CANCEL;

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }
    }
}
