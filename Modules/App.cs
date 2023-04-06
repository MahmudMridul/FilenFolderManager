
namespace FilenFolderManager.Modules
{
    internal class App
    {
        private string input = "";
        private string currentDirectory = "";
        private bool exitRequest = false;
        private InputHandler inputHandler;
        private FolderTasks folderTasks;

        private string[] options =
        {
            "1 | Change drive",
            "2 | Go to a folder",
            "3 | See list of files and folder in current folder",
            "q/Q | Exit"
        };

        private Action[] actions;

        internal App()
        {
            inputHandler = new InputHandler();
            folderTasks = new FolderTasks();
            actions = new Action[] 
            {
                ChangeDrive,
                GotoFolder
            };
        }

        internal void Run()
        {
            while(true)
            {
                if(string.IsNullOrEmpty(currentDirectory))
                {
                    folderTasks.ShowDrives();
                    Console.WriteLine("Select a drive to continue...");
                    input = inputHandler.ReadInput(folderTasks.numberOfDrives);

                    if (inputHandler.Exit(input)) break;

                    currentDirectory = folderTasks.SelectDrive(input);
                }
                else
                {
                    ShowOptions();
                    input = inputHandler.ReadInput(options.Length - 1);
                    if (inputHandler.Exit(input)) break;
                    actions[int.Parse(input) - 1]();
                }
            }
        }

        internal void ShowOptions()
        {
            for(int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }
        }

        internal void ChangeDrive()
        {
            folderTasks.ShowDrives();
            Console.WriteLine("Select a drive to continue...");
            input = inputHandler.ReadInput(folderTasks.numberOfDrives);
            if (!inputHandler.Exit(input))
            {
                currentDirectory = folderTasks.SelectDrive(input);
            }
            else
            {
                Console.WriteLine("You cannot choose exit from here.");
            }
        }

        internal void GotoFolder()
        {

        }
    }
}
