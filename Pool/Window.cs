﻿using Pool.Graphics;
using Pool.Scenes;
using SFML.Graphics;
using SFML.Window;
using Utility;

namespace Pool
{
    public static class Window
    {
        public static RenderWindow RenderWindow { get; private set; }

        public static uint WINDOW_HEIGHT = 560;
        public static uint WINDOW_WIDTH = 960;

        public static GameScene GameScene = new GameScene();

        static Window()
        {
            RenderWindow = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), " ", Styles.Titlebar | Styles.Close);
            RenderWindow.Closed += Close;
        }

        private static void Close(object sender, EventArgs e)
        {
            RenderWindow.Close();
        }

        public static bool IsWithinBounds(Vector2i pos)
        {
            if (pos.x >= 0 && pos.y >= 0)
            {
                return (pos.x <= WINDOW_WIDTH && pos.y <= WINDOW_HEIGHT);
            }
            return false;
        }
    }
}
