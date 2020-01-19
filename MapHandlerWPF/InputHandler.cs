using System;
using System.Windows.Input;

namespace MapHandlerWPF
{
    public sealed class InputHandler
    {
        #region Events

        public event EventHandler MapStateChanged;
        private event EventHandler PlayerMoved;

        #endregion


        #region Singleton

        private static readonly InputHandler INSTANCE = new InputHandler();

        private InputHandler()
        {

            itemLayerMapState = new int[,] { { 1, 1, 1, 1, 1 }, { 1, 2, 2, 2, 1 }, { 1, 2, 5, 2, 1 }, { 1, 2, 3, 2, 1 }, { 1, 2, 4, 2, 1 }, { 1, 2, 2, 2, 1 }, { 1, 1, 1, 1, 1 } };
            MapState = (int[,])itemLayerMapState.Clone();
            MapXSize = 5;
            MapYSize = 7;
            playerPos = FindPlayerCurrentPos();
            PlayerMoved += PlayerMoved_EventHandler;
        }

        public static InputHandler Instance
        {
            get
            {
                return INSTANCE;
            }
        }
        #endregion


        #region private variables

        private Tuple<int, int> playerPos;
        private int[,] itemLayerMapState;

        #endregion

        #region Properties

        public int[,] MapState { get; private set; }
        public int MapXSize { get; private set; }
        public int MapYSize { get; private set; }

        public Tuple<int, int> PlayerPos
        {
            get => playerPos;
            private set
            {
                playerPos = value;
                PlayerMoved?.Invoke(PlayerPos, new EventArgs());
            }
        }

        #endregion

        #region Public Methods
        public void HandleKeyPress(Key keyStroke)
        {
            switch (keyStroke)
            {
                case Key.Up:
                    HandleKeyPress_KeyUp();
                    break;
                case Key.Down:
                    HandleKeyPress_KeyDown();
                    break;
                case Key.Right:
                    HandleKeyPress_KeyRight();
                    break;
                case Key.Left:
                    HandleKeyPress_KeyLeft();
                    break;
            }

        }
        #endregion

        #region Private Methods

        private void HandleKeyPress_KeyUp()
        {
            Tuple<int, int> newPlayerPos = new Tuple<int, int>(playerPos.Item1 - 1, playerPos.Item2);
            HandlePlayerMovement(newPlayerPos);

        }

        private void HandleKeyPress_KeyDown()
        {
            Tuple<int, int> newPlayerPos = new Tuple<int, int>(playerPos.Item1 + 1, playerPos.Item2);
            HandlePlayerMovement(newPlayerPos);
        }

        private void HandleKeyPress_KeyRight()
        {
            Tuple<int, int> newPlayerPos = new Tuple<int, int>(playerPos.Item1, playerPos.Item2 + 1);
            HandlePlayerMovement(newPlayerPos);

        }

        private void HandleKeyPress_KeyLeft()
        {
            Tuple<int, int> newPlayerPos = new Tuple<int, int>(playerPos.Item1, playerPos.Item2 - 1);
            HandlePlayerMovement(newPlayerPos);

        }

        private void HandlePlayerMovement(Tuple<int, int> newPlayerPos)
        {
            if (CanPlayerMove(newPlayerPos))
            {
                PlayerPos = newPlayerPos;
            }
        }

        private bool CanPlayerMove(Tuple<int, int> newPlayerPos)
        {
            int newPlayerPosCell = MapState[newPlayerPos.Item1, newPlayerPos.Item2];

            if (newPlayerPosCell == 1)
            {
                return false;
            }
            // Check if the is the box, to handle its movement as well. 
            if (newPlayerPosCell == 5)
            {

                Tuple<int, int> previousPos = FindPlayerCurrentPos();
                //Get future positions x,y coords for the box
                int futureBoxPosY = newPlayerPos.Item1 - previousPos.Item1;
                int futureBoxPosX = previousPos.Item2 - newPlayerPos.Item2;

                Tuple<int, int> futureBoxPos = new Tuple<int, int>(newPlayerPos.Item1 + futureBoxPosY, newPlayerPos.Item2 - futureBoxPosX);

                //if it's a wall, do nothing. 
                if (MapState[futureBoxPos.Item1, futureBoxPos.Item2] == 1)
                {
                    return false;
                }
            }
            return true;
        }

        private void PlayerMoved_EventHandler(object sender, EventArgs e)
        {
            Tuple<int, int> previousPos = FindPlayerCurrentPos();

            if (sender is Tuple<int, int> futurePos)
            {
                int originalCell = itemLayerMapState[previousPos.Item1, previousPos.Item2];

                if (MapState[futurePos.Item1, futurePos.Item2] == 5)
                {
                    //Get future positions x,y coords for the box
                    int futureBoxPosY = futurePos.Item1 - previousPos.Item1;
                    int futureBoxPosX = previousPos.Item2 - futurePos.Item2;

                    Tuple<int, int> futureBoxPos = new Tuple<int, int>(futurePos.Item1 + futureBoxPosY, futurePos.Item2 - futureBoxPosX);
                    SetMapState(futureBoxPos, 5);
                }

                // Set original cell back to its place after player moved, unless it was the player's starting position or a box
                SetMapState(previousPos,
                originalCell == 4 || originalCell == 5 ? 2 : itemLayerMapState[previousPos.Item1, previousPos.Item2]);

                // Move player
                SetMapState(futurePos, 4);

                MapStateChanged?.Invoke(MapState, new EventArgs());
            }
        }


        private void SetMapState(Tuple<int, int> destination, int value)
        {
            MapState[destination.Item1, destination.Item2] = value;
        }

        private Tuple<int, int> FindPlayerCurrentPos()
        {
            Tuple<int, int> pos = new Tuple<int, int>(0, 0);
            for (int i = 0; i < MapYSize; i++)
            {
                for (int j = 0; j < MapXSize; j++)
                {
                    if (MapState[i, j] == 4)
                    {
                        pos = new Tuple<int, int>(i, j);
                    }
                }
            }

            return pos;
        }
        #endregion
    }
}

