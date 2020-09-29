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

        public ICommand CommandAddChar { get; set; }
        public ICommand CommandDelChar { get; set; }
        public ICommand CommandSaveEpisode { get; set; }
        public ICommand CommandCancel { get; set; }
        private Episode _newEpisode;
        private ObservableObject _selectedChar;
        private ObservableCollection<ObservableObject> _newChars;

        public ObservableCollection<ObservableObject> NewChars
        {
            get { return _newChars; }
            set 
            { 
                _newChars = value;
                OnPropertyChanged(nameof(NewChars));
            }
        }

        public ObservableObject SelectedChar
        {
            get { return _selectedChar; }
            set 
            { 
                _selectedChar = value; 
                OnPropertyChanged(nameof(SelectedChar));
            }
        }


        public Episode NewEpisode
        {
            get { return _newEpisode; }
            set { _newEpisode = value; }
        }

        public PhilAddViewModel() 
        {
            NewChars = new ObservableCollection<ObservableObject>(); 
            if (NewChars.Any()) SelectedChar = NewChars[0];
            CommandAddChar = new RelayCommand(new Action<object>(AddChar));
           // CommandDelChar = new RelayCommand(new Action<object>(DelChar));
            CommandSaveEpisode = new RelayCommand(new Action<object>(SaveEpisode));
           // CommandCancel = new RelayCommand(new Action<object>(Cancel));
        }

        private void SaveEpisode(object parameter)
        {
            
        }

        private void AddChar(object parameter)
        {
            
            Window window = new PhilAddCharView();
            window.Show();

        }

        
    }
}
