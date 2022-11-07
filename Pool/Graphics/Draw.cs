using Pool.Scenes;
using Pool.Utilities;
using SFML.Graphics;
using Utility;

namespace Pool.Graphics
{
    public class Draw
    {
        private static Color[,] LineArr = new Color[Window.WINDOW_WIDTH, Window.WINDOW_HEIGHT];
        private static Image image = new Image(LineArr);
        private static List<Vector2i> pixelsToClear = new List<Vector2i>();
        public static Vector2 predictionCircle = new Vector2(-100);

        static Draw()
        {
            LineArr = new Color[Window.WINDOW_WIDTH, Window.WINDOW_HEIGHT];
        }

        public static void Update()
        {
            // Draw Velocities
            //foreach (var gameObject in GameScene.gameObjects)
            //{
            //    if (gameObject.Transform.Velocity.GetLength() != 0)
            //    {
            //        Line((Vector2i)(gameObject.Transform.Position + gameObject.Transform.ScaledSize / 2),
            //             (Vector2i)(gameObject.Transform.Position + gameObject.Transform.ScaledSize / 2 + gameObject.Transform.Velocity * App.FRAME_TIME * 5),
            //             Color.Red);
            //    }
            //}

            // Draw Lines
            //foreach (var line in GameScene.lines)
            //{
            //    Line((Vector2i)line.pos1, (Vector2i)line.pos2, Color.Black);
            //    Vector2i middlePoint = (Vector2i)JMath.Lerp(line.pos1, line.pos2, .5f);
            //    Line(middlePoint, middlePoint + (Vector2i)line.normal, Color.Red);
            //}
            
            // Clear Screen
            Window.RenderWindow.Clear(new Color(80, 80, 80));

            // Draw Table
            Sprite table = new Sprite(Assets.Textures.Table);
            Window.RenderWindow.Draw(table);

            // Draw Objects
            foreach (var gameObject in GameScene.gameObjects)
            {
                if (gameObject.Active)
                    Window.RenderWindow.Draw(gameObject.Sprite);
            }

            // Draw Lines
            Texture texture = new Texture(image);
            Sprite sprite = new Sprite(texture);
            Window.RenderWindow.Draw(sprite);

            if (predictionCircle != new Vector2(-100))
            {
                float radius = GameScene.gameObjects[0].Transform.ScaledSize.x / 2 - 2;
                CircleShape circle;
                circle = new CircleShape(radius);
                circle.FillColor = new Color();
                circle.OutlineThickness = 2;
                circle.OutlineColor = Color.White;
                circle.Position = (SFML.System.Vector2f)predictionCircle - new SFML.System.Vector2f(radius, radius);

                Window.RenderWindow.Draw(circle);
            }

            Window.RenderWindow.Display();

            texture.Dispose();
            ClearImage();
        }

        private static void ClearImage()
        {
            foreach (Vector2i v in pixelsToClear)
            {
                image.SetPixel((uint)v.x, (uint)v.y, new Color());
            }
            pixelsToClear.Clear();
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

                    //LineArr[x, y] = color;
                    image.SetPixel((uint)x, (uint)y, color);
                    pixelsToClear.Add(new Vector2i(x, y));
                }
                else
                {
                    int x = (xdir) ? pos1.x - c : pos1.x + c;
                    int y = (ydir) ? pos1.y - i : pos1.y + i;

                    if (x > Window.WINDOW_WIDTH - 1 || y > Window.WINDOW_HEIGHT - 1) continue;
                    if (x < 0 || y < 0) continue;

                    image.SetPixel((uint)x, (uint)y, color);
                    pixelsToClear.Add(new Vector2i(x, y));
                }
            }
        }
    }
}
