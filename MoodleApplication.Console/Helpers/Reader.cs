using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoodleApplication.Console.Helpers
{
    public static class Reader
    {
        public static int? ReadInt(string prompt)
        {
            System.Console.Write(prompt);
            var input = System.Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            return null;
        }
        public static string ReadString(string prompt)
        {
            string input;
            while (true)
            {
                System.Console.Write(prompt);
                input = System.Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    System.Console.WriteLine("Polje ne smije biti prazno. Pokušajte ponovo.");

                    continue;
                }             


                return input;
            }
        }

        public static string ReadMenuChoice(string prompt = "Select an option: ")
        {
            System.Console.Write(prompt);
            return System.Console.ReadLine() ?? "";
        }
        public static string ReadEmail(string prompt)
        {
            string input;
            Regex emailRegex = new(@"^[^@\s]{1,}@[^@\s\.]{2,}(\.[^@\s\.]{2,})*\.[^@\s\.]{3,}$");


            while (true)
            {
                System.Console.Write(prompt);
                input = System.Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    System.Console.WriteLine("Email ne smije biti prazan. Pokušajte ponovo.");
                    continue;
                }

                if (!emailRegex.IsMatch(input))
                {
                    System.Console.WriteLine("Neispravan format email-a. Pokušajte ponovo.");
                    continue;
                }

                return input;
            }
        }
        public static DateOnly ReadDateOfBirth(string prompt)
        {
            DateOnly result;
            while (true)
            {
                System.Console.Write(prompt);
                string input = System.Console.ReadLine()?.Trim();

                if (!DateOnly.TryParse(input, out result))
                {
                    System.Console.WriteLine("Neispravan format datuma/vremena. Pokušajte ponovo (npr. 19.11.2025 14:30).");
                    continue;
                }

                if (result > DateOnly.FromDateTime(DateTime.Now))
                {
                    System.Console.WriteLine("Datum ne može biti u budućnosti. Pokušajte ponovo.");
                    continue;
                }

                return result;
            }
        }
        public static bool ValidateCaptcha(string captcha)
        {
            while (true)
            {
                System.Console.Write("Unesite CAPTCHA kod: ");
                var input = System.Console.ReadLine()?.Trim();

                if (string.Equals(input, captcha, StringComparison.Ordinal))
                {
                    System.Console.WriteLine("CAPTCHA uspješno potvrđena!");
                    return true;
                }

                System.Console.WriteLine("Neispravan CAPTCHA. Pokušajte ponovo.");
            }
        }
    }
}
