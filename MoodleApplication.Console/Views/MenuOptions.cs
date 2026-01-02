namespace MoodleApplication.Console.Views
{
    public class MenuOptions
    {
        private readonly Dictionary<string, (string Description, Func<Task<bool>> Action)> _options;

        public MenuOptions()
        {
            _options = [];
        }

        public MenuOptions AddOption(string key, string description, Func<Task<bool>> action)
        {
            _options.Add(key, (description, action));
            return this;
        }

        public Dictionary<string, (string Description, Func<Task<bool>> Action)> Build()
        {
            return _options;
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStartMenuOptions(MenuManager menuManager)
        {
                       var menuOptions = new MenuOptions()
                .AddOption("1", "Login", async () =>
                {
                    MenuManager.HandleUserLogin();
                    return false;
                })
                .AddOption("2", "Register", async () =>
                {
                    MenuManager.HandleUserRegister();
                    return false;
                })
                .AddOption("0", "Exit", async () =>
                {
                    return true;
                });
            return menuOptions.Build();

        }
    }
}
