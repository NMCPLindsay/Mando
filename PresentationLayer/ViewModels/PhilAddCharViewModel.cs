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


using System.Net.NetworkInformation;

namespace MandalorianDB.PresentationLayer.ViewModels
{
    public class PhilAddCharViewModel : ObservableObject
    {
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonCancelCommand { get; set; }
       


        public string NewChar { get; set; }
        
        private CharacterOperation _charOperation;
        public PhilAddCharViewModel(CharacterOperation charOp)
        {
            NewChar = charOp.NewChar;
           
            _charOperation = charOp;
            ButtonAddCommand = new RelayCommand(new Action<object>(AddChar));
            ButtonCancelCommand = new RelayCommand(new Action<object>(CancelAddChar));
        }

        private void CancelAddChar(object parameter)
        {
            _charOperation.Status = CharacterOperation.OperationStatus.CANCEL;

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }

        private void AddChar(object parameter)
        {
            _charOperation.Status = CharacterOperation.OperationStatus.OKAY;

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }
    }
}
