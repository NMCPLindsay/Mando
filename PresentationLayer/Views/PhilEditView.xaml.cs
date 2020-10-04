using MandalorianDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MandalorianDB.PresentationLayer
{
    /// <summary>
    /// Interaction logic for PhilAddView.xaml
    /// </summary>
    public partial class PhilEditView : Window
    {
        public PhilEditView(EpisodeOperation episodeOperation)
        {
            InitializeComponent();
            PhilEditViewModel philEditViewModel = new PhilEditViewModel(episodeOperation);
            DataContext = philEditViewModel;
        }
    }
}
