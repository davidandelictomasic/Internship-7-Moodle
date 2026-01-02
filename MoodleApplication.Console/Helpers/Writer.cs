using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleApplication.Console.Helpers
{
    public static class Writer
    {
        private static readonly Random _random = new();
        public static void WriteMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public static void WaitForKey()
        {
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }

        public static void DisplayMenu(string title, Dictionary<string, (string Description, Func<Task<bool>> Action)> options)
        {
            System.Console.WriteLine($"\n=== {title} ===");

            foreach (var option in options)
            {
                System.Console.WriteLine($"{option.Key}. {option.Value.Description}");
            }
        }

        public static string GenerateCaptcha()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            int length = _random.Next(4, 9); 
            var captchaChars = new char[length];

            captchaChars[0] = letters[_random.Next(letters.Length)];
            captchaChars[1] = digits[_random.Next(digits.Length)];

            var allChars = letters + digits;
            for (int i = 2; i < length; i++)
            {
                captchaChars[i] = allChars[_random.Next(allChars.Length)];
            }

            for (int i = captchaChars.Length - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (captchaChars[i], captchaChars[j]) = (captchaChars[j], captchaChars[i]);
            }   
            

            return new string(captchaChars);
        }
    }
}

