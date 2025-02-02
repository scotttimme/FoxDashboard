﻿using FoxDashboard.FrontEnd.Common;
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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FoxDashboard.FrontEnd.Common;
using FoxDashboard.FrontEnd.Models;

namespace FoxDashboard.FrontEnd.ViewModels
{

    public class MainWindowViewModel : ViewModel
    {
        #region data
        const int _appsPerPage = (int)ApplicationIndices.MaxApplications;
        private DataStore _dataStore = new DataStore();
        private int _pageCount;
        private int _pageNumber;
        
        public ObservableCollection<AppAttributes> ApplicationAttributes { get; } = new ObservableCollection<AppAttributes>();
        public PageAttributes PageAttributes { get; } = new PageAttributes();

        private ICommand _appButtonCommand;
        public ICommand AppButtonCommand { get => _appButtonCommand; set => _appButtonCommand = value; }
        
        private ICommand _homeButtonCommand;
        public ICommand HomeButtonCommand { get => _homeButtonCommand; set => _homeButtonCommand = value; }

        private ICommand _pageDecCommand;
        public ICommand PageDecCommand { get => _pageDecCommand; set => _pageDecCommand = value; }

        private ICommand _pageIncCommand;
        public ICommand PageIncCommand { get => _pageIncCommand; set => _pageIncCommand = value; }
        #endregion 

        #region constructor
        public MainWindowViewModel()
        {
            _pageNumber = 1;

            ApplicationAttributes.CollectionChanged += MyCollectionChanged;

            CountApps();

            _pageCount = DeterminePageCount();
            UpdatePageDisplay();

            AppButtonCommand = new RelayCommand(new Action<object>(ProcessApp));
            HomeButtonCommand = new RelayCommand(new Action<object>(HomeButton_Click));
            PageDecCommand = new RelayCommand(new Action<object>(DecPageButton_Click));
            PageIncCommand = new RelayCommand(new Action<object>(IncPageButton_Click));
        }
        #endregion 

        #region private Methods
        private void CountApps()
        {
            string appName = String.Empty;
            string executableName = String.Empty;          
            _dataStore.AppCount = 0;

            string myDirectory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            _dataStore.AppPath = myDirectory+"\\..\\Apps";
            DirectoryInfo d = new DirectoryInfo(_dataStore.AppPath);

            foreach (DirectoryInfo dir in d.GetDirectories())
            {
                appName = dir.Name;
                foreach (FileInfo file in dir.GetFiles())
                {
                    if ( (file.Extension.Equals(".html")) || (file.Extension.Equals(".exe")) )
                    {
                        executableName = file.Name;
                    }
                }

                _dataStore.Apps.Add(new Common.App() { AppName = appName, ExecutableName = executableName });
            }

            _dataStore.AppCount = _dataStore.Apps.Count;
        }

        private void GetAppExecutables()
        {

        }

        private int DeterminePageCount()
        {
            int pageCount = (int)(Math.Ceiling(Convert.ToDouble(_dataStore.AppCount) / Convert.ToDouble(_appsPerPage)));

            return pageCount;
        }

        private void UpdatePageButtons()
        {
            PageAttributes.PageDecrementable = (_pageNumber > 1) ? true : false;
            PageAttributes.PageIncrementable = (_pageNumber < _pageCount) ? true : false;
        }

        private void UpdateAppList()
        {
            int pageOffset = (_pageNumber - 1) * _appsPerPage;
            int appNumber = 0;
            string imageNumberPath = String.Empty;
            bool endOfApps = false;

            for (int i = 0; (i < _appsPerPage) && !endOfApps; i++)
            {
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

        private void ToggleView()
        {
            PageAttributes.DisplayAppList = !PageAttributes.DisplayAppList;
            PageAttributes.DisplayAppExecution = !PageAttributes.DisplayAppExecution;
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
            if (PageAttributes.DisplayAppExecution)
            {
                ToggleView();
                PageAttributes.PageTitle = Constants.DEFAULTTITLE;
            }
        }

        public void DecPageButton_Click(object obj)
        {
            if (_pageNumber > 1)
            {
                _pageNumber--;
                UpdatePageDisplay();
            }
        }

        public void IncPageButton_Click(object obj)
        {
            if (_pageNumber < _pageCount)
            {
                _pageNumber++;
                UpdatePageDisplay();
            }
        }

        public void ProcessApp(object obj)
        {
            if (obj.GetType() != null)
            {
                int pageOffset = (_pageNumber - 1) * _appsPerPage;
                Button myButton = obj as Button;
                string myButtonUid = myButton.Uid.ToString();
                int appIndex = (int)EnumUtility.GetEnumValueFromDescription<ApplicationIndices>(myButtonUid) + pageOffset;
/*
                try
                {
                    foreach (string file in )
                }*/


                
                PageAttributes.Url = _dataStore.AppPath + "\\" + _dataStore.Apps[appIndex].AppName.ToString() + "\\" + _dataStore.Apps[appIndex].ExecutableName.ToString();
                PageAttributes.PageTitle = _dataStore.Apps[appIndex].AppName.ToString(); 
                ToggleView();
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
        #endregion 
    }
}
