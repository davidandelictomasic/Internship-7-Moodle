using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleApplication.Console.Helpers
{
    public static class Writer
    {
        public static void WriteMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public static void WaitForKey()
        {
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}
}
