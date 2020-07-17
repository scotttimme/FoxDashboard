using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TabletMock.FrontEnd.Models
{
    public class PageAttributes : INotifyPropertyChanged
    {
        private Visibility _pageDecVisibility = Visibility.Hidden;
        private Visibility _pageIncVisibiltiy = Visibility.Hidden;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Visibility PageDecVisibility 
        { 
            get => _pageDecVisibility; 
            set
            {
                if (PageDecVisibility != value)
                {
                    _pageDecVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageDecVisibility)));
                }

            }
        }
       
        public Visibility PageIncVisibiltiy 
        { 
            get => _pageIncVisibiltiy; 
            set
            {
                if (PageIncVisibiltiy != value)
                {
                    _pageIncVisibiltiy = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageIncVisibiltiy)));
                }

            }
        }
    }
}
