using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGame.Classes
{
    public class WindowEventHandler
    {
        private RenderWindow window;

        public WindowEventHandler(RenderWindow window)
        {
            this.window = window;
            window.KeyPressed += HandleKeyPressing;
            window.Closed += HandleWindowClosing;
            window.Resized += HandleWindowResizing;
        }

        private void HandleWindowResizing(object sender, SizeEventArgs e)
        {
            window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private void HandleWindowClosing(object sender, EventArgs e)
        {
            window.Close();
        }

        private void HandleKeyPressing(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        } 
    }
}
