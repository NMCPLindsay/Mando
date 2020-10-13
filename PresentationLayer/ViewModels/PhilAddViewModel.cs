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
        public string NewChar { get; set; }
        private List<string> _chars;

        public List<string> Chars
        {
            get { return _chars; }
            set
            {
                _chars = value;
                OnPropertyChanged(nameof(Chars));
            }
        }

        public string SelectedChar
        {
            get { return _selectedChar; }
            set { _selectedChar = value;
                OnPropertyChanged(nameof(SelectedChar)); }
        }

        public PhilAddViewModel(EpisodeOperation episodeOp)
        {
            NewEpisode = episodeOp.Episode;
            _episodeOperation = episodeOp;
            NewEpisode.Characters = new ObservableCollection<string>();
            
            ButtonAddCommand = new RelayCommand(new Action<object>(AddEpisode));
            ButtonCancelCommand = new RelayCommand(new Action<object>(CancelAddEpisode));
            ButtonAddCharCommand = new RelayCommand(new Action<object>(AddChar));
            ButtonDelCharCommand = new RelayCommand(new Action<object>(DelChar));
        }
        private void AddChar(object parameter)
        {
            NewChar = parameter.ToString().Replace("System.Windows.Controls.TextBox: ", "");
            if (NewChar != "")
            {

                NewEpisode.Characters.Add(NewChar);
                
            }
            else
            {
                MessageBox.Show("Please input a character to add.");
            }
        }
        private void DelChar(object parameter)
        {
            if (parameter != null)
            {


                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the {parameter} character from inventory?", "Delete Character", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        NewEpisode.Characters.Remove(parameter.ToString());
                        MessageBox.Show($"{parameter} character Deleted", "Delete Character");

                        if (NewEpisode.Characters.Any())
                        {
                            SelectedChar = NewEpisode.Characters[0];
                        }
                        break;

                    case MessageBoxResult.No:
                        MessageBox.Show($"{parameter} Character Deletion Canceled", "Delete Character");
                        break;
                }
            }
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
            EpisodeRepository episodeRepository = new EpisodeRepository();
            List<Episode> episodes = new List<Episode>(episodeRepository.GetAll());
            NewEpisode.Id = (episodes.Count() + 1);
            EpisodeBusiness episodeBusiness = new EpisodeBusiness();
            episodeBusiness.AddEpisode(NewEpisode);

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }


    }
}
