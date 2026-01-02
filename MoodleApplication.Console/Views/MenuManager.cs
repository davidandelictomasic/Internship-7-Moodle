using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Console.Helpers;
using Npgsql.Internal;

namespace MoodleApplication.Console.Views
{
    public class MenuManager
    {
        public async Task RunAsync()
        {
            System.Console.Clear();

            bool exitRequested = false;

            var mainMenuOptions = MenuOptions.CreateStartMenuOptions(this);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - START MENU", mainMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (mainMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await mainMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }

            
        }
        public static void HandleUserLogin()
        {
            System.Console.Clear();
            Writer.WriteMessage("=== USER LOGIN ===");

            var userEmail = Reader.ReadEmail("Email: ");
            var userPassword = Reader.ReadString("Password: ");
        }
        public static void HandleUserRegister()
        {
            System.Console.Clear();
            Writer.WriteMessage("=== USER REGISTRATION ===");

            var userEmail = Reader.ReadEmail("Email: ");

            var userPassword = Reader.ReadString("Password: ");

            var captcha = Writer.GenerateCaptcha();

            Writer.WriteMessage($"CAPTCHA: {captcha}");
            Reader.ValidateCaptcha(captcha);
        }

    }
}
