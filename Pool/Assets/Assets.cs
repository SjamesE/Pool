using SFML.Graphics;

namespace Pool.Assets
{
    internal static class Textures
    {
        public static Texture WhiteBall;
        public static Texture BlackBall;
        public static Texture Table;

        static Textures()
        {
            WhiteBall = new Texture("Assets/WhiteBall.png");
            BlackBall = new Texture("Assets/BlackBall.png");
            Table     = new Texture("Assets/Table.png");
        }
    }
}
