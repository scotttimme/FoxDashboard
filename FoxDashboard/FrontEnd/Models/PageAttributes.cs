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
        private bool _pageDecrementable = false;
        private bool _pageIncrementable = false;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public bool PageDecrementable 
        { 
            get => _pageDecrementable; 
            set
            {
                if (PageDecrementable != value)
                {
                    _pageDecrementable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageDecrementable)));
                }

            }
        }
       
        public bool PageIncrementable 
        { 
            get => _pageIncrementable; 
            set
            {
                if (PageIncrementable != value)
                {
                    _pageIncrementable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageIncrementable)));
                }

            }
        }
    }
}
