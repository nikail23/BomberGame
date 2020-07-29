using BomberGame.Classes;
using SFML.Graphics;
using SFML.Window;
using System;
using EventHandler = BomberGame.Classes.EventHandler;
using VideoMode = SFML.Window.VideoMode;

namespace BomberGame
{
    class Program
    {
        private const string WindowName = "BomberGame";
        private const int WindowWidth = 800;
        private const int WindowHeight = 600;
        private const bool VerticalSyncEnabled = true;
        private static readonly Color ClearColor = Color.White;

        private const int GameBoardSize = 25;

        public static RenderWindow Window;

        private static EventHandler eventHandler;
        private static GameHandler gameHandler;

        static void Main()
        {
            Window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), WindowName);
            Window.SetVerticalSyncEnabled(VerticalSyncEnabled);

            ContentHandler.Load();

            eventHandler = new EventHandler(Window);
            Window.KeyPressed += Window_KeyPressed;

            gameHandler = new GameHandler(GameBoardSize);

            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                gameHandler.Update();
                Window.Clear(ClearColor);
                gameHandler.Draw();
                Window.Display();
            }
        }

        private static void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.R)
            {
                gameHandler = new GameHandler(GameBoardSize);
            }
        }
    }
}
