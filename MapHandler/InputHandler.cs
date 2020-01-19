using System;
using System.Windows.Input;
using System.Windows;
namespace MapHandler
{
    /// <summary>
    /// Very simple singleton, but the project will not use threading.
    /// </summary>
    public sealed class InputHandler
    {
        private static readonly InputHandler INSTANCE = new InputHandler();

        private InputHandler() { }

        public static InputHandler Instance
        {
            get
            {
                return INSTANCE;
            }
        }

        public void HandleKeyPress(KeyStrokes keyStroke)
        {

        }


    }

    public enum KeyStrokes
    {
        up,
        down,
        left,
        right
    }
}
