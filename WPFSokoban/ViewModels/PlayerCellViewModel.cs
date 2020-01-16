using System;
using System.Collections.Generic;
using System.Text;

namespace WPFSokoban.ViewModels
{
    class PlayerCellViewModel : ICellsViewModel
    {
        /// <summary>
        /// Return the path to correct image corresponding to wall
        /// </summary>
        public string DisplayedImagePath { get; }

        public PlayerCellViewModel()
        {
            this.DisplayedImagePath = "";
        }
    }
}
