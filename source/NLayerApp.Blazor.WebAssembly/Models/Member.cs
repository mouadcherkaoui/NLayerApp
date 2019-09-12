using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayerApp.Blazor.WebAssembly.Models 
{

    public class Member: INotifyPropertyChanged
    {
        private int _id;
        private string _firstName;
        private string _lastName;

        [DisplayName(displayName: "Id")]
        public int Id { get=>_id; set=> RaisePropertyChanged(ref _id, value); }

        [DisplayName(displayName: "First Name")]
        public string firstName { get=>_firstName; set=>RaisePropertyChanged(ref _firstName, value); }

        [DisplayName(displayName: "Last Name")]
        public string lastName { get=>_lastName; set=>RaisePropertyChanged(ref _lastName, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<TValue>(ref TValue backingField, TValue value, [CallerMemberName] string propertyName = "") 
        {
            backingField = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}