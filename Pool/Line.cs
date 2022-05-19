using Utility;

namespace Pool
{
    public class Line
    {
        public Vector2 pos1 { get; set; }
        public Vector2 pos2 { get; set; }

        public Line(Vector2 pos1, Vector2 pos2)
        {
            this.pos1 = pos1;
            this.pos2 = pos2;
        }
    }
}
