using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;
using MoodleApplication.Console.Views.Common;

namespace MoodleApplication.Console.Views.Chats
{
    public class ChatMenuManager
    {
        private readonly ChatActions _chatActions;
        private readonly UserActions _userActions;

        public ChatMenuManager(ChatActions chatActions, UserActions userActions)
        {
            _chatActions = chatActions;
            _userActions = userActions;
        }

        public async Task ShowPrivateChatMenu(int userId)
        {
            

            bool exitRequested = false;

            var privateChatMenuOptions = MenuOptions.CreatePrivateChatMenuOptions(this, userId);
            while (!exitRequested)
            {
                System.Console.Clear();
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
                    Writer.WaitForKey();
                }
            }
            System.Console.Clear();
        }

        public async Task ShowNewMessageMenu(int currentUserId)
        {
            System.Console.Clear();

            var users = await _userActions.GetUsersWithoutExistingChat(currentUserId);

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
                System.Console.Clear();

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
                    Writer.WaitForKey();
                }
            }
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
                System.Console.Clear();

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
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowChatScreen(int currentUserId, int chatRoomId, int otherUserId, string otherUserName)
        {
            bool exitChat = false;

            while (!exitChat)
            {
                System.Console.Clear();
                Writer.WriteMessage($"=== CHAT WITH: {otherUserName} ===");
                Writer.WriteMessage("(Type /exit to go back)\n");

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

                Writer.WriteMessage("");
                var messageContent = Reader.ReadString("Input message: ");

                if (messageContent.Equals("/exit", StringComparison.OrdinalIgnoreCase))
                {
                    exitChat = true;
                }
                else
                {
                    await _chatActions.SendMessage(currentUserId, otherUserId, messageContent);
                }
            }
        }
    }
}
