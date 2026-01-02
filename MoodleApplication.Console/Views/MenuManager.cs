using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;
using Npgsql.Internal;

namespace MoodleApplication.Console.Views
{
    public class MenuManager
    {
        private readonly UserActions _userActions;

        public MenuManager(UserActions userActions)
        {
            _userActions = userActions;
        }

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
            System.Console.Clear();



        }
        public async Task ShowStudentMenu()
        {
            System.Console.Clear();

            bool exitRequested = false;

            var studentMenuOptions = MenuOptions.CreateStudentMenuOptions(this);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - STUDENT MENU", studentMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (studentMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await studentMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();



        }
        public async Task HandleUserLogin()
        {
            System.Console.Clear();
            Writer.WriteMessage("=== USER LOGIN ===");

            var userEmail = Reader.ReadEmail("Email: ");
            var userPassword = Reader.ReadString("Password: ");
            var loginReult = await _userActions.LoginUser(userEmail, userPassword);
            if(loginReult.IsSuccess)
            {
                Writer.WriteMessage("Login successful.");
                Writer.WaitForKey();
                if (loginReult.Role == "Student")
                {
                    await ShowStudentMenu();
                }
            }
            else
            {
                Writer.WriteMessage("Login failed. Please check your credentials.");
                Writer.WaitForKey();
            }
        }
        public async Task HandleUserRegister()
        {
            System.Console.Clear();
            Writer.WriteMessage("=== USER REGISTRATION ===");

            var userName = Reader.ReadString("Full Name: ");

            var userDob = Reader.ReadDateOfBirth("Date of Birth (YYYY-MM-DD): ");

            var userEmail = Reader.ReadEmail("Email: ");

            var userPassword = Reader.ReadString("Password: ");

            var captcha = Writer.GenerateCaptcha();

            Writer.WriteMessage($"CAPTCHA: {captcha}");
            Reader.ValidateCaptcha(captcha);
           if (await _userActions.RegisterUser(userName, userDob, userEmail, userPassword))
           {
               Writer.WriteMessage("User registered successfully.");    
                Writer.WaitForKey();

            }
           else
           {
               Writer.WriteMessage("User registration failed.");
           }
        }

    }
}
