using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayer.Blazor.WebAssembly.Models 
{
    public class Member: INotifyPropertyChanged
    {
        private int _id;
        public int Id { get=>_id; set=> RaisePropertyChanged(ref _id, value); }
        private string _firstName;
        public string firstName { get=>_firstName; set=>RaisePropertyChanged(ref _firstName, value); }
        private string _lastName;    
        public string lastName { get=>_lastName; set=>RaisePropertyChanged(ref _lastName, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<TValue>(ref TValue backingField, TValue value, [CallerMemberName] string propertyName = "") 
        {
            backingField = value; 
            Console.WriteLine(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}