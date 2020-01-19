using System;
using System.Collections.Generic;
using System.Text;

namespace WPFSokoban.Models
{
    public class FloorCellModel : ICellsModel
    {
        /// <summary>
        /// Return the path to correct image corresponding to wall
        /// </summary>
        public string DisplayedImagePath { get; }

        public FloorCellModel()
        {
            this.DisplayedImagePath = "Assets/Floor.png";
        }
    }
}
