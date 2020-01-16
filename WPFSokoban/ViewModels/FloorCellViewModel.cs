using System;
using System.Collections.Generic;
using System.Text;

namespace WPFSokoban.ViewModels
{
    public class FloorCellViewModel : ICellsViewModel
    {
        /// <summary>
        /// Return the path to correct image corresponding to wall
        /// </summary>
        public string DisplayedImagePath { get; }

        public FloorCellViewModel()
        {
            this.DisplayedImagePath = "";
        }
    }
}
