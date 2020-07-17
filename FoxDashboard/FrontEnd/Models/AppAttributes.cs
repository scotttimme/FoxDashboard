using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TabletMock.FrontEnd.Models
{
    public class AppAttributes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Visibility _appVisibility = Visibility.Hidden;
        private String _appIconPath = string.Empty;

        public Visibility AppVisibility 
        { 
            get => _appVisibility;
            set
            {
                if (AppVisibility != value)
                {
                    _appVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AppVisibility)));
                }
                
            }
        }
        
        public string AppIconPath { get => _appIconPath; set => _appIconPath = value; }
    }
}
