namespace MandalorianDB.PresentationLayer.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using MandalorianDB.BusinessLayer;
    using MandalorianDB.Models;
    using MandalorianDB.PresentationLayer.Views;

    public class MichelleManageViewModel : ObservableObject
    {
        private Episode _episode;
        private MichelleView _parentWindow;
        private EpisodeOperation _episodeOperation;
        public string NewChar { get; set; }
        private ObservableCollection<string> _chars;


        public ObservableCollection<string> Chars
        {
            get { return _chars; }
            set
            {
                _chars = value;
                OnPropertyChanged(nameof(Chars));
            }
        }



        // public string Value { get; set; }


        #region CONSTRUCTORS
        public MichelleManageViewModel(Episode episode)
        {
            this.Episode = episode;
            this.Episode.Characters = new List<string>();
            this.Chars = new ObservableCollection<string>();
            this.CommandAddCharacter = new RelayCommand(this.AddCharacter);
            this.CommandSaveData = new RelayCommand(this.SaveEpisode);
            this.CommandRemoveCharacter = new RelayCommand(this.RemoveCharacter);
        }

        public MichelleManageViewModel()
        {
            // if (this.Episode == null) this.Episode = new Episode();

            this.CommandAddCharacter = new RelayCommand(this.AddCharacter);
            this.CommandSaveData = new RelayCommand(this.SaveEpisode);
            this.CommandRemoveCharacter = new RelayCommand(this.RemoveCharacter);
        }
        #endregion

        public Episode Episode
        {
            get => this._episode;
            set
            {
                this._episode = value;
                this.OnPropertyChanged(nameof(this.Episode));
            }
        }

        public ICommand CommandSaveData { get; set; }
        public ICommand CommandAddCharacter { get; set; }
        public ICommand CommandRemoveCharacter { get; set; }

        private void SaveEpisode(object parameter)
        {
            if (this.Episode.Characters.Count < 1
                || this.Episode.EpisodeNumber == 0
                || this.Episode.SeasonNumber == 0
                || string.IsNullOrEmpty(this.Episode.Director)
                || string.IsNullOrEmpty(this.Episode.Writer)
                || string.IsNullOrEmpty(this.Episode.EpisodeDetails)
                || string.IsNullOrEmpty(this.Episode.Name))
            {
                MessageBox.Show("All fields are required.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                EpisodeBusiness _episodeBusiness = new EpisodeBusiness();
                if (this.Episode.Id == 0)
                    _episodeBusiness.AddEpisode(this.Episode);
                else
                    _episodeBusiness.UpdateEpisode(this.Episode);

                MessageBox.Show("Data saved successfully.");


                var win = Application.Current.Windows[0];
                win.Close();

            }
        }

        private void AddCharacter(object parameter)

        {
            var textbox = parameter as TextBox;
            EpisodeOperation episodeOp = _episodeOperation;

            if (textbox.Text != string.Empty)
            {

                NewChar = parameter.ToString().Replace("System.Windows.Controls.TextBox: ", "");
                if (NewChar != "")
                {

                    Chars.Add(NewChar);
                    OnPropertyChanged(nameof(Chars));
                    this.Episode.Characters = Chars.ToList();
                }
                MessageBox.Show("Characters added successfully.");
            }
            else
            {
                MessageBox.Show("All fields are required.");
            }
        }

        private void RemoveCharacter(object parameter)
        {
            var listbox = parameter as ListBox;

            if (listbox.SelectedItems.Count < 1)
            {
                MessageBox.Show(
                    "Please select a character to delete.",
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            else
            {
                this.Episode.Characters.Remove(listbox.SelectedItems[0].ToString());
                listbox.Items.Refresh();
            }
        }
    }
}
