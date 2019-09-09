using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NLayerApp.Blazor.WebAssembly.Models
{
    public class ObservableObject: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<TValue>(ref TValue backingField, TValue value, [CallerMemberName] string propertyName = "")
        {
            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
