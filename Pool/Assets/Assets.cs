using SFML.Graphics;

namespace Pool.Assets
{
    internal static class Textures
    {
        public static Texture WhiteBall;
        public static Texture RedBall;
        public static Texture Table;

        static Textures()
        {
            WhiteBall = new Texture("Assets/WhiteBall.png");
            WhiteBall.Smooth = true;
            RedBall = new Texture("Assets/RedBall.png");
            RedBall.Smooth = true;
            Table     = new Texture("Assets/Table.png");
        }
    }
}
