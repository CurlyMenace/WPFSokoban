using System;
using System.Collections.Generic;
using System.Text;

namespace WPFSokoban.Models
{
    class PlayerCellModel : ICellsModel
    {
        /// <summary>
        /// Return the path to correct image corresponding to wall
        /// </summary>
        public string DisplayedImagePath { get; }

        public PlayerCellModel()
        {
            this.DisplayedImagePath = "Assets/player.png";
        }
    }
}
