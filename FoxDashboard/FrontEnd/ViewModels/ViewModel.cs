using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxDashboard.FrontEnd.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        #region Notify propert Changed Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Notify propert Changed Members

        public void NotifyPropertyChanged(string propertyName)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
