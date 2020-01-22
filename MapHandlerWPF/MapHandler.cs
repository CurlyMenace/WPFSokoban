using System;
using System.Collections.Generic;
using System.Text;

namespace MapHandlerWPF
{
    public sealed class MapHandler
    {
        private static readonly MapHandler INSTANCE = new MapHandler();
        private MapHandler()
        {
            ItemLayerMapState = new int[,] { { 1, 1, 1, 1, 1 }, { 1, 2, 2, 2, 1 }, { 1, 2, 5, 2, 1 }, { 1, 2, 3, 2, 1 }, { 1, 2, 4, 2, 1 }, { 1, 2, 2, 2, 1 }, { 1, 1, 1, 1, 1 } };
            MapState = (int[,])ItemLayerMapState.Clone();
            MapXSize = 5;
            MapYSize = 7;
        }

        public static MapHandler Instance
        {
            get
            {
                return INSTANCE;
            }
        }

        public int[,] ItemLayerMapState { get; private set; }

        public int[,] MapState { get; private set; }
        public int MapXSize { get; private set; }
        public int MapYSize { get; private set; }

        public void SetMapState(Tuple<int, int> destination, int value)
        {
            MapState[destination.Item1, destination.Item2] = value;
        }
    }
}
