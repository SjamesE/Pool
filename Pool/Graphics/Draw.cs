using Pool.Scenes;
using Pool.Utilities;
using SFML.Graphics;
using Utility;

namespace Pool.Graphics
{
    public class Draw
    {
        private static List<Vertex> lineVertices = new List<Vertex>();
        public static Vector2 predictionCircle = new Vector2(-100);
        private static RenderTexture rndTexture;

        // lists for the debug circles
        //public static List<float> circles = new();
        //public static List<Vector2> circlesPos = new();

        static Draw()
        {
            rndTexture = new RenderTexture(Window.WINDOW_WIDTH, Window.WINDOW_HEIGHT);
            //LineArr = new Color[Window.WINDOW_WIDTH, Window.WINDOW_HEIGHT];
        }

        public static void Update()
        {
            /* DEBUG
            // Draw Velocities
            foreach (var gameObject in GameScene.gameObjects)
            {
                if (gameObject.Transform.Velocity.GetLength() != 0)
                {
                    Line((Vector2i)(gameObject.Transform.Position + gameObject.Transform.ScaledSize / 2),
                         (Vector2i)(gameObject.Transform.Position + gameObject.Transform.ScaledSize / 2 + gameObject.Transform.Velocity * App.FRAME_TIME * 5),
                         Color.Red);
                }
            }

            // Draw wall debug Lines
            foreach (var line in GameScene.lines)
            {
                Line((Vector2i)line.pos1, (Vector2i)line.pos2, Color.Black);
                Vector2i middlePoint = (Vector2i)JMath.Lerp(line.pos1, line.pos2, .5f);
                Line(middlePoint, middlePoint + (Vector2i)line.normal, Color.Red);
            }
            */

            // Clear Screen
            rndTexture.Clear(new Color(80, 80, 80));

            // Draw Table
            Sprite table = new Sprite(Assets.Textures.Table);
            rndTexture.Draw(table);

            // Draw Objects
            foreach (var gameObject in GameScene.gameObjects)
            {
                if (gameObject.Active)
                    rndTexture.Draw(gameObject.Sprite);
            }

            rndTexture.Draw(lineVertices.ToArray(), PrimitiveType.Lines);


            if (predictionCircle != new Vector2(-100))
            {
                float radius = GameScene.gameObjects[0].Transform.ScaledSize.x / 2 - 2;
                CircleShape circle;
                circle = new CircleShape(radius);
                circle.FillColor = new Color();
                circle.OutlineThickness = 2;
                circle.OutlineColor = Color.White;
                circle.Position = (SFML.System.Vector2f)predictionCircle - new SFML.System.Vector2f(radius, radius);

                rndTexture.Draw(circle);
            }

            /* DEBUG
            // Draw debug Ray March lines
            for (int i = 0; i < circles.Count; i++)
            {
                CircleShape circleShape = new CircleShape(circles[i]);
                circleShape.FillColor = new Color();
                circleShape.OutlineThickness = 1;
                circleShape.OutlineColor = Color.White;
                circleShape.Position = (SFML.System.Vector2f)circlesPos[i] - new SFML.System.Vector2f(circles[i], circles[i]);
                circleShape.SetPointCount(circles[i] < 40 ? 20u : 40u);

                rndTexture.Draw(circleShape);
            }
            circles = new();
            circlesPos = new();

            // Draw holes
            foreach (var hole in GameScene.holes)
            {
                CircleShape circleShape = new CircleShape(26);
                circleShape.FillColor = new Color();
                circleShape.OutlineThickness = 1;
                circleShape.OutlineColor = Color.Red;
                circleShape.Position = (SFML.System.Vector2f)hole;

                rndTexture.Draw(circleShape);
            }
            */

            rndTexture.Display();

            // Clear Screen
            Window.RenderWindow.Clear(new Color(80, 80, 80));
            lineVertices.Clear();

            // Draw 
            Sprite sprt = new Sprite(rndTexture.Texture);
            Window.RenderWindow.Draw(sprt);

            // Display
            Window.RenderWindow.Display();
        }

        public static void Line(Vector2i pos1, Vector2i pos2, Color color)
        {
            lineVertices.Add(new Vertex((SFML.System.Vector2f)pos1, color));
            lineVertices.Add(new Vertex((SFML.System.Vector2f)pos2, color));
        }
    }
}
