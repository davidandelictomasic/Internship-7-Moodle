using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;

namespace MoodleApplication.Console.Views.Common
{
    public class MenuManager
    {
        protected readonly UserActions _userActions;
        protected readonly CourseActions _courseActions;
        protected readonly ChatActions _chatActions;
        protected readonly AdminActions _adminActions;

        
        public Chats.ChatMenuManager ChatMenu { get; private set; } = null!;
        public Users.UserMenuManager UserMenu { get; private set; } = null!;
        public Admin.AdminMenuManager AdminMenu { get; private set; } = null!;
        public Courses.CourseMenuManager CourseMenu { get; private set; } = null!;

        public MenuManager(
            UserActions userActions,
            CourseActions courseActions,
            ChatActions chatActions,
            AdminActions adminActions)
        {
            _userActions = userActions;
            _courseActions = courseActions;
            _chatActions = chatActions;
            _adminActions = adminActions;
        }

        public void InitializeManagers()
        {
            CourseMenu = new Courses.CourseMenuManager(_courseActions);
            ChatMenu = new Chats.ChatMenuManager(_chatActions, _userActions);
            UserMenu = new Users.UserMenuManager(_userActions, ChatMenu, CourseMenu);
            AdminMenu = new Admin.AdminMenuManager(_adminActions, ChatMenu);
        }

        public async Task RunAsync()
        {
            InitializeManagers();

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

        public async Task HandleUserLogin()
        {
            System.Console.Clear();
            Writer.WriteMessage("=== USER LOGIN ===");

            var userEmail = Reader.ReadEmail("Email: ");
            var userPassword = Reader.ReadString("Password: ");
            var loginResult = await _userActions.LoginUser(userEmail, userPassword);

            if (loginResult.IsSuccess)
            {
                Writer.WriteMessage("Login successful.");
                Writer.WaitForKey();

                switch (loginResult.Role)
                {
                    case "Student":
                        await UserMenu.ShowStudentMenu(loginResult.UserId);
                        break;
                    case "Admin":
                        await AdminMenu.ShowAdminMenu(loginResult.UserId);
                        break;
                    case "Professor":
                        await UserMenu.ShowProfessorMenu(loginResult.UserId);
                        break;
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
            int? resultId = await _userActions.RegisterUser(userName, userDob, userEmail, userPassword);
            if (resultId != null)
            {
                Writer.WriteMessage("User registered successfully.");
                Writer.WaitForKey();
                await UserMenu.ShowStudentMenu(resultId.Value);

            }
            else
            {
                Writer.WriteMessage("User registration failed.");
            }
        }
    }
}
