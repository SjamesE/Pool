using SFML.Graphics;
using SFML.System;
using Utility;

namespace Pool
{
    public class GameObject
    {
        public string Name { get; set; }
        public Transform Transform { get; set; }
        public Texture Texture { get; private set; }
        public Sprite Sprite 
        { 
            get
            {
                Sprite sprite = new Sprite(Texture);

                sprite.Position = (Vector2f)Transform.Position;
                sprite.Scale = (Vector2f)Transform.Scale;
                sprite.Rotation = Transform.Rotation;

                return sprite;
            }
        }

        public Vector2 Center => new Vector2(Transform.Position.x + (Transform.ScaledSize.x / 2),
                                             Transform.Position.y + (Transform.ScaledSize.y / 2));

        public float Top => Transform.Position.y;

        public float Bottom => Transform.Position.y + Texture.Size.Y * Transform.Scale.y;

        public float Left => Transform.Position.x;

        public float Right => Transform.Position.x + Texture.Size.X * Transform.Scale.x;

        public Vector2 TopLeft => Transform.Position;

        public Vector2 TopRight => new Vector2(Right, Top);

        public Vector2 BottomLeft => new Vector2(Left, Bottom);

        public Vector2 BottomRight => new Vector2(Right, Bottom);

        public GameObject(string name, Transform transform, Texture texture)
        {
            Name = name;
            Transform = transform;
            Texture = texture;

            Transform.Size = new Vector2(Texture.Size);
        }

        public void Update()
        {
            Transform.Update();
        }

        public void UpdateTexture(Texture texture)
        {
            Texture = texture;
        }
    }
}
