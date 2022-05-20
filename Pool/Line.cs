using Utility;

namespace Pool
{
    public class Line
    {
        public Vector2 pos1 { get; set; }
        public Vector2 pos2 { get; set; }
        public Vector2 normal { get; }

        public Line(Vector2 pos1, Vector2 pos2)
        {
            this.pos1 = pos1;
            this.pos2 = pos2;

            normal = new Vector2((pos2.y - pos1.y) * -1, pos2.x - pos1.x).Normalize() * 10;
            //Console.WriteLine(normal);
        }
    }
}
