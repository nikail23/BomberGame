using BomberGame.Classes;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Threading;
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
        private static readonly Color ClearColor = Color.Green;

        private const int GameBoardSize = 25;

        public static RenderWindow Window;

        private static EventHandler eventHandler;
        private static GameHandler gameHandler;

        private Thread gameThread;

        static void Main()
        {
            Window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), WindowName);
            Window.SetVerticalSyncEnabled(VerticalSyncEnabled);
            Window.KeyPressed += Window_KeyPressed;

            ContentHandler.Load();

            eventHandler = new EventHandler(Window);

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
            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                gameHandler.CloseGame();
                gameHandler = new GameHandler(25);
            }
        }
    }
}
