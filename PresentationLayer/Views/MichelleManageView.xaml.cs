namespace MandalorianDB.PresentationLayer.Views
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    using MandalorianDB.Models;
    using MandalorianDB.PresentationLayer.ViewModels;

    /// <summary>
    /// Interaction logic for MichelleManageView.xaml
    /// </summary>
    public partial class MichelleManageView
    {
        #region CONSTRUCTOR
        public MichelleManageView()
        {
            this.InitializeComponent();
        }
       
        #endregion

        #region EVENTS
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            this.AddCharacterTextBox.Visibility =
                this.AddCharacterTextBox.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }
        #endregion

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as MichelleManageViewModel;
            var characters =  this.CharactersListBox.Items.Cast<string>().ToList();

            //model.Episode.Characters =  characters;
            //this.Close();
        }
    }
}
