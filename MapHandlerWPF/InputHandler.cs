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

        #endregion

        #region Properties



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
            MapHandler map = MapHandler.Instance;
            int newPlayerPosCell = map.MapState[newPlayerPos.Item1, newPlayerPos.Item2];

            if (newPlayerPosCell == 1)
            {
                return false;
            }

            // Check if the is the box, to handle its movement as well. 
            if (newPlayerPosCell == 5)
            {
                Tuple<int, int> previousPos = FindPlayerCurrentPos();
                //Get future positions x,y coords for the box
                Tuple<int, int> futureBoxPos = GetFutureBoxPos(previousPos, newPlayerPos);

                //if it's a wall, do nothing. 
                if (map.MapState[futureBoxPos.Item1, futureBoxPos.Item2] == 1)
                {
                    return false;
                }
            }
            return true;
        }

        private void PlayerMoved_EventHandler(object sender, EventArgs e)
        {
            MapHandler map = MapHandler.Instance;

            if (sender is Tuple<int, int> futurePos)
            {
                Tuple<int, int> previousPos = FindPlayerCurrentPos();
                int originalCell = map.ItemLayerMapState[previousPos.Item1, previousPos.Item2];

                if (map.MapState[futurePos.Item1, futurePos.Item2] == 5)
                {

                    Tuple<int, int> futureBoxPos = GetFutureBoxPos(previousPos, futurePos);
                    map.SetMapState(futureBoxPos, 5);
                }

                // Set original cell back to its place after player moved, unless it was the player's starting position or a box
                map.SetMapState(previousPos,
                originalCell == 4 || originalCell == 5 ? 2 : map.ItemLayerMapState[previousPos.Item1, previousPos.Item2]);

                // Move player
                map.SetMapState(futurePos, 4);

                MapStateChanged?.Invoke(map.MapState, new EventArgs());
            }
        }

        private Tuple<int, int> GetFutureBoxPos(Tuple<int, int> previousPos, Tuple<int, int> futurePos)
        {
            //Get future positions x,y coords for the box
            int futureBoxPosY = futurePos.Item1 - previousPos.Item1;
            int futureBoxPosX = futurePos.Item2 - previousPos.Item2;
            return new Tuple<int, int>(futurePos.Item1 + futureBoxPosY, futurePos.Item2 + futureBoxPosX);
        }


        private Tuple<int, int> FindPlayerCurrentPos()
        {
            MapHandler map = MapHandler.Instance;

            Tuple<int, int> pos = new Tuple<int, int>(0, 0);
            for (int i = 0; i < map.MapYSize; i++)
            {
                for (int j = 0; j < map.MapXSize; j++)
                {
                    if (map.MapState[i, j] == 4)
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

