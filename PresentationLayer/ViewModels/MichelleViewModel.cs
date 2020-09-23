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

namespace MandalorianDB.PresentationLayer
{
    public class MichelleViewModel : ObservableObject
    {
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
        public MichelleViewModel()
        {
            Episodes = new ObservableCollection<Episode>(SessionData.GetEpisodeList());

            if (Episodes.Any()) SelectedEpisode = Episodes[0];
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
                            if (episode.Director != SearchName)
                            {
                                Episodes.RemoveAt(i);
                            }
                        }
                        break;
                    case "WRITER":
                        for (int i = Episodes.Count - 1; i >= 0; i--)
                        {
                            Episode episode = Episodes[i];
                            if (episode.Writer != SearchName)
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
