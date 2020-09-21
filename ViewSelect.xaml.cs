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
using MandalorianDB.PresentationLayer;

namespace MandalorianDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ViewSelect : Window
    {
        public ViewSelect()
        {
            InitializeComponent();
        }

        private void Phil_Click(object sender, RoutedEventArgs e)
        {
            Window window = new PhilView();
            window.Show();
                   
        }

        private void Jess_Click(object sender, RoutedEventArgs e)
        {
           // Window window = new JessView();
           // window.Show();
        }

        private void Michelle_Click(object sender, RoutedEventArgs e)
        {
           // Window window = new MichelleView();
           // window.Show();
        }
    }
}
