namespace MandalorianDB.PresentationLayer.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
       // public string Value { get; set; }


        #region CONSTRUCTORS
        public MichelleManageViewModel(Episode episode)
        {
            this.Episode = episode;
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
                _episodeBusiness.UpdateEpisode(this.Episode);
                MessageBox.Show("Data saved successfully.");
                //var win = Application.Current.Windows[0];
                //win.Close();
                
            }
        }

        private void AddCharacter(object parameter)
        {
            var textbox = parameter as TextBox;

            if (textbox.Text != string.Empty)
            {
               // this.Episode.Characters = new ObservableCollection<string>();
                this.Episode.Characters.Add(textbox.Text);
                textbox.Text = string.Empty;
                MessageBox.Show("Characters added successfully.");
                // TODO UpdateSource
            }
            else
            {
                // MessageBox.Show("All fields are required.");
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
