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
        private readonly CourseActions _courseActions;
        private readonly ChatActions _chatActions;

        public MenuManager(UserActions userActions, CourseActions courseActions, ChatActions chatActions)
        {
            _userActions = userActions;
            _courseActions = courseActions;
            _chatActions = chatActions;
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
        public async Task ShowStudentMenu(int userId)
        {
            System.Console.Clear();

            bool exitRequested = false;

            var studentMenuOptions = MenuOptions.CreateStudentMenuOptions(this, userId);
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
                    await ShowStudentMenu(loginReult.UserId);
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
        public async Task ShowUserCourses(int userId)
        {
            System.Console.Clear();

            var userCourses = await _userActions.GetUserCourses(userId);

            if (!userCourses.Any())
            {
                Writer.WriteMessage("No courses found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var coursesMenuOptions = MenuOptions.CreateStudentCoursesMenuOptions(this, userCourses);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - MOJI KOLEGIJI", coursesMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (coursesMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await coursesMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }
       

        public async Task ShowSelectedCourseMenu(int courseId)
        {
            System.Console.Clear();
            bool exitRequested = false;

            var studentCourseMenuOptions = MenuOptions.CreateStudentCourseMenuOptions(this, courseId);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - MY COURSES", studentCourseMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (studentCourseMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await studentCourseMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }
        public async Task ShowCourseMaterials(int courseId)
        {
            System.Console.Clear();

            var courseMaterials = await _courseActions.GetCourseMaterials(courseId);

            if (!courseMaterials.Any())
            {
                Writer.WriteMessage("No materials found.");
                Writer.WaitForKey();
                return;
            }
            Writer.WriteMessage("=== COURSE MATERIALS ===\n");

            foreach (var material in courseMaterials)
            {
                Writer.WriteMessage($"Added at: {material.MaterialAddedAt}, Name: {material.MaterialName}, URL: {material.MaterialURL}");
            }

            Writer.WaitForKey();
            System.Console.Clear();

        }
        public async Task ShowCourseAnnouncements(int courseId)
        {
            System.Console.Clear();

            var courseAnnouncements = await _courseActions.GetCourseAnnouncements(courseId);

            if (!courseAnnouncements.Any())
            {
                Writer.WriteMessage("No materials found.");
                Writer.WaitForKey();
                return;
            }
            Writer.WriteMessage("=== COURSE ANNOUNCEMENTS ===\n");
            foreach (var announcement in courseAnnouncements)
            {
                Writer.WriteMessage($"Created at: {announcement.AnnouncementCreatedAt}, Title: {announcement.AnnouncementTitle}, Content: {announcement.AnnouncementContent}, Professor: {announcement.ProfessorName}");
            }

            Writer.WaitForKey();
            System.Console.Clear();

        }

        public async Task ShowPrivateChatMenu(int userId)
        {
            System.Console.Clear();

            bool exitRequested = false;

            var privateChatMenuOptions = MenuOptions.CreatePrivateChatMenuOptions(this, userId);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - PRIVATE CHAT", privateChatMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (privateChatMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await privateChatMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowNewMessageMenu(int currentUserId)
        {
            System.Console.Clear();

            var users = await _userActions.GetAllUsers(currentUserId);

            if (!users.Any())
            {
                Writer.WriteMessage("No users found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var usersMenuOptions = MenuOptions.CreateUsersListMenuOptions(this, currentUserId, users);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - SEND MESSAGE TO USER", usersMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (usersMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await usersMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowSendMessageScreen(int senderId, int receiverId, string receiverName)
        {
            System.Console.Clear();
            Writer.WriteMessage($"=== NEW MESSAGE FOR: {receiverName} ===\n");

            var messageContent = Reader.ReadString("Input message: ");

            var success = await _chatActions.SendMessage(senderId, receiverId, messageContent);

            if (success)
            {
                Writer.WriteMessage("Message sent!");
            }
            else
            {
                Writer.WriteMessage("Error while sending.");
            }

            Writer.WaitForKey();
            System.Console.Clear();
        }

        public async Task ShowMyChatRoomsMenu(int userId)
        {
            System.Console.Clear();

            var chatRooms = await _chatActions.GetUserChatRooms(userId);

            if (!chatRooms.Any())
            {
                Writer.WriteMessage("No chats.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var chatRoomsMenuOptions = MenuOptions.CreateChatRoomsMenuOptions(this, userId, chatRooms);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - MY CHATS", chatRoomsMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (chatRoomsMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await chatRoomsMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowChatScreen(int currentUserId, int chatRoomId, int otherUserId, string otherUserName)
        {
            System.Console.Clear();
            Writer.WriteMessage($"=== CHAT WITH: {otherUserName} ===\n");

            var messages = await _chatActions.GetChatMessages(chatRoomId);

            if (!messages.Any())
            {
                Writer.WriteMessage("No messages in this chat.\n");
            }
            else
            {
                foreach (var message in messages)
                {
                    var senderLabel = message.SenderId == currentUserId ? "You" : message.SenderName;
                    Writer.WriteMessage($"[{message.SentAt:dd.MM.yyyy HH:mm}] {senderLabel}: {message.Content}");
                }
            }

            Writer.WaitForKey();
            System.Console.Clear();
        }
    }
}
