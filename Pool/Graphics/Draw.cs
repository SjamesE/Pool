using Pool.Scenes;
using SFML.Graphics;
using Utility;

namespace Pool.Graphics
{
    internal class Draw
    {
        private static Color[,] LineArr;

        static Draw()
        {
            LineArr = new Color[Window.WINDOW_WIDTH, Window.WINDOW_HEIGHT];
        }

        public static void Update()
        {
            foreach (var gameObject in GameScene.gameObjects)
            {
                Draw.Line((Vector2i)(gameObject.Transform.Position + gameObject.Transform.Size * gameObject.Transform.Scale / 2),
                          (Vector2i)(gameObject.Transform.Position + gameObject.Transform.Size * gameObject.Transform.Scale / 2 + gameObject.Transform.Velocity * App.FRAME_TIME * 5),
                          Color.Red);
            }

            Window.RenderWindow.Clear(new Color(60, 60, 60));

            foreach (var gameObject in GameScene.gameObjects)
            {
                Window.RenderWindow.Draw(gameObject.Sprite);
            }

            Image image = new Image(LineArr);
            Texture texture = new Texture(image);
            Sprite sprite = new Sprite(texture);
            Window.RenderWindow.Draw(sprite);

            Window.RenderWindow.Display();

            texture.Dispose();
            LineArr = new Color[Window.WINDOW_WIDTH, Window.WINDOW_HEIGHT];
        }

        public static void Line(Vector2i pos1, Vector2i pos2, Color color)
        {
            bool xdir = (pos1.x > pos2.x);
            bool ydir = (pos1.y > pos2.y);

            int w = Math.Abs(pos1.x - pos2.x);
            int h = Math.Abs(pos1.y - pos2.y);

            w = (w <= 1) ? 1 : w;
            h = (h <= 1) ? 1 : h;

            //Color[,] clrArr = new Color[w, h];

            bool horizontal = w > h;
            float d = (horizontal) ? (float)h / (float)w : (float)w / (float)h;
            int l = (horizontal) ? w : h;

            for (int i = 0; i < l; i++)
            {
                int c = (int)Math.Round(i * d - .5f);

                if (horizontal)
                {
                    int x = (xdir) ? pos1.x - i : pos1.x + i;
                    int y = (ydir) ? pos1.y - c : pos1.y + c;

                    if (x > Window.WINDOW_WIDTH - 1 || y > Window.WINDOW_HEIGHT - 1) continue;
                    if (x < 0 || y < 0) continue;

                    LineArr[x, y] = color;
                }
                else
                {
                    int x = (xdir) ? pos1.x - c : pos1.x + c;
                    int y = (ydir) ? pos1.y - i : pos1.y + i;

                    if (x > Window.WINDOW_WIDTH - 1 || y > Window.WINDOW_HEIGHT - 1) continue;
                    if (x < 0 || y < 0) continue;

                    LineArr[x, y] = color;
                }
            }

            //Image image = new Image(LineArr);
            //if (xdir)
            //{
            //    image.FlipHorizontally();
            //    pos1.x -= w;
            //}
            //if (ydir)
            //{
            //    image.FlipVertically();
            //    pos1.y -= h;
            //}
            //Texture texture = new Texture((uint)w, (uint)h);
            //texture.Update(image);
            //
            ////Create Sprite
            //Sprite sprite = new Sprite(texture);
            //sprite.Position = (SFML.System.Vector2f)pos1;
            //vfx.Add(sprite);
        }
    }
}
