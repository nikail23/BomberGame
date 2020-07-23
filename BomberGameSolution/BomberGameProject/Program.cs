﻿using BomberGame.Classes;
using SFML.Graphics;
using System;
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

        private const int GameBoardSize = 10;

        public static RenderWindow Window;

        private static WindowEventHandler windowEventHandler;
        private static GameHandler gameHandler;

        static void Main()
        {
            Window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), WindowName);
            Window.SetVerticalSyncEnabled(VerticalSyncEnabled);

            ContentHandler.Load();

            windowEventHandler = new WindowEventHandler(Window);

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
    }
}