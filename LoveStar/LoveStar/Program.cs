using System;

namespace LoveStar
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game_Base game = new Game_Base())
            {
                game.Run();
            }
        }
    }
#endif
}

