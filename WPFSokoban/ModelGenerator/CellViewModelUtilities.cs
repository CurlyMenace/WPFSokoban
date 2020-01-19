using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WPFSokoban.Models;

namespace WPFSokoban.ModelGenerator
{
    public class CellViewModelUtilities
    {

        /// <summary>
        /// Maybe will be needed in future.
        /// </summary>
        public CellViewModelUtilities() { }

        /// <summary>
        /// Since there isn't much logic involved in generating the cells, there is no point
        /// to have an instance based factory.
        /// </summary>
        /// <param name="cellType"></param>
        /// <returns></returns>
        private ICellsModel CellsViewModelFactory(int cellType)
        {
            ICellsModel cellViewModel;

            switch (cellType)
            {
                case 1:
                    cellViewModel = new WallCellModel();
                    break;
                case 2:
                    cellViewModel = new FloorCellModel();
                    break;
                case 3:
                    cellViewModel = new GoalCellModel();
                    break;
                case 4:
                    cellViewModel = new PlayerCellModel();
                    break;
                case 5:
                    cellViewModel = new BoxCellModel();
                    break;
                default:
                    cellViewModel = new FloorCellModel();
                    break;
            }

            return cellViewModel;
        }

        /// <summary>
        /// Generate a map view model for the updated map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="xLength"></param>
        /// <param name="yLength"></param>
        /// <returns></returns>
        public ObservableCollection<ObservableCollection<ICellsModel>> MapGenerator(int[,] map, int xLength, int yLength)
        {
            ObservableCollection<ObservableCollection<ICellsModel>> cellMap = new ObservableCollection<ObservableCollection<ICellsModel>>();
            for (int i = 0; i < yLength; i++)
            {
                ObservableCollection<ICellsModel> cellRow = new ObservableCollection<ICellsModel>();
                for (int j = 0; j < xLength; j++)
                {
                    cellRow.Add(this.CellsViewModelFactory(map[i, j]));
                }
                cellMap.Add(cellRow);
            }

            return cellMap;
        }
    }
}
