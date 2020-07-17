using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TabletMock.FrontEnd.Common;
using TabletMock.FrontEnd.Models;

namespace TabletMock.FrontEnd.ViewModels
{

    public class MainWindowViewModel : ViewModel
    {
        #region data
        const int _appsPerPage = (int)ApplicationIndices.MaxApplications;
        private int _pageCount;
        private int _pageNumber;

        private DataStore _dataStore = new DataStore();
        public ObservableCollection<AppAttributes> ApplicationAttributes { get; } = new ObservableCollection<AppAttributes>();

        public PageAttributes PageAttributes { get; } = new PageAttributes();
        //public ObservableCollection<PageAttributes> PageAttributes { get; } = new ObservableCollection<PageAttributes>();

        private ICommand _appButtonCommand;
        public ICommand AppButtonCommand { get => _appButtonCommand; set => _appButtonCommand = value; }
        
        private ICommand _homeButtonCommand;
        public ICommand HomeButtonCommand { get => _homeButtonCommand; set => _homeButtonCommand = value; }

        public ObservableCollection<AppAttributes> TestAppListAttributes { get; } = new ObservableCollection<AppAttributes>();
        public AppAttributes TestAppAttributes { get; } = new AppAttributes();
        #endregion 

        #region constructor
        public MainWindowViewModel()
        {
            _pageNumber = 1;

            ApplicationAttributes.CollectionChanged += MyCollectionChanged;
            //PageAttributes += Page_PropertyChanged;

            CountApps();
            _pageCount = DeterminePageCount();
            UpdatePageDisplay();

            AppButtonCommand = new RelayCommand(new Action<object>(ProcessApp));
            HomeButtonCommand = new RelayCommand(new Action<object>(HomeButton_Click));
        }
        #endregion 

        #region private Methods
        private void CountApps()
        {
            _dataStore.AppCount = 0;
            string myDirectory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            _dataStore.AppPath = myDirectory+"\\..\\Apps";
            _dataStore.AppCount =  Directory.GetDirectories(_dataStore.AppPath).Length;
        }

        private int DeterminePageCount()
        {
            //This is done awfully to show C#'s built in libraries. Casting and convertring example
            int pageCount = (int)(Math.Ceiling(Convert.ToDouble(_dataStore.AppCount) / Convert.ToDouble(_appsPerPage)));

            return pageCount;
        }

        private void UpdatePageButtons()
        {
            PageAttributes.PageDecVisibility = (_pageNumber > 0) ? Visibility.Visible : Visibility.Hidden;
            PageAttributes.PageIncVisibiltiy = (_pageNumber < _pageCount) ? Visibility.Visible : Visibility.Hidden;

           // Page_PropertyChanged();
        }

        private void UpdateAppList()
        {
            int pageOffset = (_pageNumber - 1) * _appsPerPage;
            int appNumber = 0;
            string imageNumberPath = String.Empty;
            bool endOfApps = false;

            for (int i = 0; (i < _appsPerPage) && !endOfApps; i++)
            {
                // I wanted to show here the ability to convert simple with C#
                appNumber = pageOffset + i + 1;
                imageNumberPath = appNumber.ToString();

                if (appNumber <= _dataStore.AppCount)
                {
                    ApplicationAttributes.Add(new AppAttributes());
                    ApplicationAttributes[i].AppVisibility = true;
                    ApplicationAttributes[i].AppIconPath = _dataStore.AppPath + "\\Images\\" + imageNumberPath + ".jpg";

                }
                else
                {
                    ApplicationAttributes.Add(new AppAttributes());
                    ApplicationAttributes[i].AppVisibility = false;
                }
            }
        }
        #endregion


        #region public methods
        public void UpdatePageDisplay()
        {
            ApplicationAttributes.Clear();

            UpdateAppList();
            UpdatePageButtons();
        }

        public void HomeButton_Click(object obj)

        {
            //UpdatePageDisplay();
            //toggle app 1 for testing
            bool temp = (ApplicationAttributes[0].AppVisibility == true) ? false : true;
            ApplicationAttributes[0].AppVisibility = temp;
        }

        public void ProcessApp(object obj)
        {
            int appIndex;

            if (obj.GetType() != null)
            {
                Button myButton = obj as Button;
                string myButtonUid = myButton.Uid.ToString();
                appIndex = (int)EnumUtility.GetEnumValueFromDescription<ApplicationIndices>(myButtonUid);

                MessageBox.Show("Botton: " + myButtonUid + " Visibility: " + ApplicationAttributes[appIndex].AppVisibility.ToString());
            }
            else
            {
                MessageBox.Show("Bad Object");
            }
             
        }
        #endregion

        #region helper methods
        private void MyCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= Item_PropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += Item_PropertyChanged;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if( e.PropertyName == "AppVisibility")
            {
                NotifyPropertyChanged("ApplicationAttributes");
            }
        }

/*        private void Page_PropertyChanged(object sendder, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "fds")
            {
                NotifyPropertyChanged("PageAttributes");
            }
        }*/
        #endregion 
    }
}
