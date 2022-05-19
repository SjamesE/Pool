namespace Pool
{
    public class App
    {
        public static readonly int TARGET_FPS = 60;
        public static readonly float FRAME_TIME = 1f / TARGET_FPS;

        Window window;

        public App()
        {

        }

        /// <summary>
        /// App core loop
        /// </summary>
        public void Run()
        {
            float timeTillUpdate = FRAME_TIME;
            Initialize();

            while (Window.RenderWindow.IsOpen)
            {
                Update();

                if (timeTillUpdate < 0)
                {
                    timeTillUpdate = FRAME_TIME;
                    //Console.WriteLine(Time.totalTime);
                    FixedUpdate();
                }
                else timeTillUpdate -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Gets called when the App starts
        /// </summary>
        private void Initialize()
        {
            window = new Window();
        }

        /// <summary>
        /// Gets called continuously
        /// </summary>
        private void Update()
        {
            Time.Update();
        }

        /// <summary>
        /// Gets called once per frame
        /// </summary>
        private void FixedUpdate()
        {
            window.Update();
        }
    }
}
