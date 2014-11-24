using System;

namespace Monopolio
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Monopolio game = new Monopolio())
            {
                game.Run();
            }
        }
    }
#endif
}

