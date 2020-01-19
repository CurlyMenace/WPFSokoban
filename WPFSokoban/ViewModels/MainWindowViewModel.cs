using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using WPFSokoban.Models;

namespace WPFSokoban.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<ObservableCollection<ICellsModel>> cellsCollection;
        public ObservableCollection<ObservableCollection<ICellsModel>> CellsCollection
        {
            get => cellsCollection;
            set
            {
                cellsCollection = value;
                NotifyPropertyChanged("CellsCollection");
            }
        }

        public MainWindowViewModel()
        {

        }
    }
}
