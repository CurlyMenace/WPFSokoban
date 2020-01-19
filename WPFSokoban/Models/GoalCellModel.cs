using System;
using System.Collections.Generic;
using System.Text;

namespace WPFSokoban.Models
{
    public class GoalCellModel : ICellsModel
    {
        /// <summary>
        /// Return the path to correct image corresponding to wall
        /// </summary>
        public string DisplayedImagePath { get; }

        public GoalCellModel()
        {
            this.DisplayedImagePath = "Assets/goal.png";
        }
    }
}
