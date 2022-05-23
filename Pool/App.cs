using Pool.Graphics;

namespace Pool
{
    public class App
    {
        public static readonly int TARGET_FPS = 144;
        public static readonly float FRAME_TIME = 1f / TARGET_FPS;
        public static readonly float PHYSICS_UPDATE = 0.0005f;

        private Window window = new Window();

        /// <summary>
        /// App core loop
        /// </summary>
        public void Run()
        {
            float timeTillUpdate = FRAME_TIME;
            float timeTillPhysicsUpdate = PHYSICS_UPDATE;

            while (Window.RenderWindow.IsOpen)
            {
                Time.Update();

                if (timeTillUpdate < 0)
                {
                    timeTillUpdate = FRAME_TIME;
                    FixedUpdate();
                }
                else timeTillUpdate -= Time.deltaTime;

                if (timeTillPhysicsUpdate < 0)
                {
                    timeTillPhysicsUpdate = PHYSICS_UPDATE;
                    PhysicsUpdate();
                    if (Time.deltaTime > timeTillPhysicsUpdate)
                    {
                        Console.WriteLine("oof");
                    }
                }
                else timeTillPhysicsUpdate -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Gets called once per frame
        /// </summary>
        private void FixedUpdate()
        {
            Window.RenderWindow.DispatchEvents();
            Draw.Update();
        }

        private void PhysicsUpdate()
        {
            Input.Update();
            Window.GameScene.Update();
            Physics.Update();
        }
    }
}
