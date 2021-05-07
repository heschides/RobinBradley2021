using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RobinBradley2021.Other
{
    public class InactiveViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Set<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            property = value;
            var handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

