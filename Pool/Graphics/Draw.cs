using Pool.Scenes;
using Pool.Utilities;
using SFML.Graphics;
using Utility;

namespace Pool.Graphics
{
    public class Draw
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
                if (gameObject.Transform.Velocity.GetLength() != 0)
                {
                    Line((Vector2i)(gameObject.Transform.Position + gameObject.Transform.ScaledSize / 2),
                         (Vector2i)(gameObject.Transform.Position + gameObject.Transform.ScaledSize / 2 + gameObject.Transform.Velocity * App.FRAME_TIME * 5),
                         Color.Red);
                }
            }

            Vector2[] holes = new Vector2[6]
            {
                new Vector2(39, 39),
                new Vector2(Window.WINDOW_WIDTH / 2, 39),
                new Vector2(Window.WINDOW_WIDTH - 41, 39),
                new Vector2(39, Window.WINDOW_HEIGHT - 41),
                new Vector2(Window.WINDOW_WIDTH / 2, Window.WINDOW_HEIGHT - 41),
                new Vector2(Window.WINDOW_WIDTH - 41, Window.WINDOW_HEIGHT - 41)
            };

            foreach (var line in GameScene.lines)
            {
                Line((Vector2i)line.pos1, (Vector2i)line.pos2, Color.Black);
                Vector2i middlePoint = (Vector2i)JMath.Lerp(line.pos1, line.pos2, .5f);
                Line(middlePoint, middlePoint + (Vector2i)line.normal, Color.Red);
            }
            

            Window.RenderWindow.Clear(new Color(80, 80, 80));

            Sprite table = new Sprite(Assets.Textures.Table);
            Window.RenderWindow.Draw(table);

            foreach (var gameObject in GameScene.gameObjects)
            {
                Window.RenderWindow.Draw(gameObject.Sprite);
            }

            Image image = new Image(LineArr);
            Texture texture = new Texture(image);
            Sprite sprite = new Sprite(texture);
            Window.RenderWindow.Draw(sprite);

            CircleShape circle;
            foreach (var hole in holes)
            {
                circle = new CircleShape(18);
                circle.FillColor = new Color(10, 10, 10);
                circle.OutlineThickness = 2;
                circle.OutlineColor = new Color(80, 80, 80);
                circle.Position = (SFML.System.Vector2f)hole - new SFML.System.Vector2f(17, 17);
                //LineArr[(int)circle.Position.X, (int)circle.Position.Y] = Color.White;
                //LineArr[(int)circle.Position.X + 34, (int)circle.Position.Y + 34] = Color.White;

                Window.RenderWindow.Draw(circle);
            }

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

            bool horizontal = w > h;
            float d = (horizontal) ? (float)h / (float)w : (float)w / (float)h;
            int   l = (horizontal) ? w : h;

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
        }
    }
}
