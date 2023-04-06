
namespace FilenFolderManager.Modules
{
    internal class App
    {
        private string input = "";
        private string currentDirectory = "";
        private bool doExit = false;
        private InputHandler inputHandler;
        private FolderTasks folderTasks;

        private string[] options =
        {
            "1 | Change drive",
            "2 | Go to a folder",
            "3 | See list of files and folder in current folder",
            "q/Q | Exit"
        };

        public App()
        {
            inputHandler = new InputHandler();
            folderTasks = new FolderTasks();
        }

        public void Run()
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
                    Console.WriteLine("Do something else");
                }
            }
        }
    }
}
