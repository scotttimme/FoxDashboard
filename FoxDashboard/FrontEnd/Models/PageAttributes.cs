using FoxDashboard.FrontEnd.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FoxDashboard.FrontEnd.Common;

namespace FoxDashboard.FrontEnd.Models
{
    public class PageAttributes : INotifyPropertyChanged
    {
        #region private attributes
        private bool _displayAppList = true;
        private bool _displayAppExecution = false;
        private bool _pageDecrementable = false;
        private bool _pageIncrementable = false;
        private string _pageTitle = Constants.DEFAULTTITLE;
        private string _url;
        #endregion

        #region public attributes
        public event PropertyChangedEventHandler PropertyChanged;

        public bool DisplayAppList
        {
            get => _displayAppList;
            set
            {
                if (DisplayAppList != value)
                {
                    _displayAppList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayAppList)));
                }

            }
        }

        public bool DisplayAppExecution
        {
            get => _displayAppExecution;
            set
            {
                if (DisplayAppExecution != value)
                {
                    _displayAppExecution = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayAppExecution)));
                }

            }
        }

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

        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                if (PageTitle != value)
                {
                    _pageTitle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageTitle)));
                }

            }
        }

        public string Url
        {
            get => _url;
            set
            {
                if (Url != value)
                {
                    _url = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Url)));
                }

            }
        }
        #endregion
    }
}
