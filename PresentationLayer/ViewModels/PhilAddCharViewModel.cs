﻿using System;
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
        public ICommand AddToList { get; set; }
        public PhilAddCharViewModel()
        {
            AddToList = new RelayCommand(Action<object>(AddToList));
        }

        private void AddToList(object parameter)
        {

        }
    }
}
