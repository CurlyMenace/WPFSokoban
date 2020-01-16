using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFSokoban.ViewModels;

namespace WPFSokoban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ObservableCollection<ICellsViewModel>> CellsCollection;
        int[,] intmap = { { 1, 1, 1, 1, 1 }, { 1, 2, 5, 2, 1 }, { 1, 2, 3, 2, 1 }, { 1, 2, 4, 2, 1 }, { 1, 1, 1, 1, 1 } };
        public MainWindow()
        {

            InitializeComponent();

            this.CellsCollection = generateMapViewModel();
            map.ItemsSource = this.CellsCollection;
        }

        private ObservableCollection<ObservableCollection<ICellsViewModel>> generateMapViewModel()
        {
            ObservableCollection<ObservableCollection<ICellsViewModel>> cellMap = new ObservableCollection<ObservableCollection<ICellsViewModel>>();
            for (int i = 0; i < 5; i++)
            {
                ObservableCollection<ICellsViewModel> cellRow = new ObservableCollection<ICellsViewModel>();
                for (int j = 0; j < 5; j++)
                {
                    ICellsViewModel cell; 
                    switch(intmap[i, j])
                    {
                        case 1:
                            cell = new WallCellViewModel();
                            break;
                        case 2:
                            cell = new FloorCellViewModel();
                            break;
                        case 3:
                            cell = new GoalCellViewModel();
                            break;
                        case 4:
                            cell = new PlayerCellViewModel();
                            break;
                        case 5:
                            cell = new BoxCellViewModel();
                            break;
                        default:
                            cell = new FloorCellViewModel();
                            break;
                    }
                    cellRow.Add(cell);
                }
                cellMap.Add(cellRow);
            }

            return cellMap;
        }
    }
}
