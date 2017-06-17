using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Tank_War_Multiplayer
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
