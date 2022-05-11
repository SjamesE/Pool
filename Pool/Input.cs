using SFML.Graphics;
using SFML.Window;
using Utility;

namespace Pool
{
    internal static class Input
    {
        private class Key
        {
            public Keyboard.Key name;
            public KeyState state;

            public Key(Keyboard.Key name, KeyState state = KeyState.up)
            {
                this.name = name;
                this.state = state;
            }
        }

        public enum KeyState
        {
            downFrame0,
            down,
            upFrame0,
            up
        }

        private static List<Key> keyStates;
        private static List<KeyState> mouseStates;


        static Input()
        {
            keyStates = new List<Key>();
            foreach (Keyboard.Key key in Enum.GetValues(typeof(Keyboard.Key)))
            {
                if (keyStates.Count > 0)
                {
                    if (keyStates[keyStates.Count - 1].name == key)
                        continue;
                }
                keyStates.Add(new Key(key));
            }

            mouseStates = new List<KeyState>();
            mouseStates.Add(KeyState.up);
            mouseStates.Add(KeyState.up);
            mouseStates.Add(KeyState.up);
        }


        /// <summary>
        /// Update KeyStates
        /// </summary>
        public static void Update()
        {
            //Keys
            for (int i = 0; i <= keyStates.Count - 1; i++)
            {
                //Console.WriteLine($"i: {i} -> {(Keyboard.Key)i} | {keyStates[i].name}");
                if (Keyboard.IsKeyPressed(keyStates[i].name))
                {
                    if (keyStates[i].state == KeyState.up || keyStates[i].state == KeyState.upFrame0)
                        keyStates[i].state = KeyState.downFrame0;
                    else if (keyStates[i].state == KeyState.downFrame0)
                        keyStates[i].state = KeyState.down;
                    //Console.WriteLine($"i: {i} -> {(Keyboard.Key)i} | {keyStates[i].name}, {keyStates[i].state}");
                }
                else
                {
                    if (keyStates[i].state == KeyState.down || keyStates[i].state == KeyState.downFrame0)
                        keyStates[i].state = KeyState.upFrame0;
                    else if (keyStates[i].state == KeyState.upFrame0)
                        keyStates[i].state = KeyState.up;
                }
            }

            //Mouse
            for (int i = 0; i < mouseStates.Count - 1; i++)
            {
                if (Mouse.IsButtonPressed((Mouse.Button)i))
                {
                    if (mouseStates[i] == KeyState.up || mouseStates[i] == KeyState.upFrame0)
                        mouseStates[i] = KeyState.downFrame0;
                    else if (mouseStates[i] == KeyState.downFrame0)
                        mouseStates[i] = KeyState.down;
                }
                else
                {
                    if (mouseStates[i] == KeyState.down || mouseStates[i] == KeyState.downFrame0)
                        mouseStates[i] = KeyState.upFrame0;
                    else if (mouseStates[i] == KeyState.upFrame0)
                        mouseStates[i] = KeyState.up;
                }
            }
        }

        /// <summary>
        /// Get the state of a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static KeyState GetKeyState(Keyboard.Key key)
        {
            return keyStates.Find(x => x.name == key).state;
        }

        public static KeyState GetMouseState(Mouse.Button button)
        {
            return mouseStates[(int)button];
        }

        public static KeyState GetMouseState(int button)
        {
            return mouseStates[button];
        }

        /// <summary>
        /// Get the mouse position
        /// </summary>
        /// <returns></returns>
        public static Vector2i GetMousePos()
        {
            return new Vector2i(Mouse.GetPosition(Window.RenderWindow));
        }
    }
}
