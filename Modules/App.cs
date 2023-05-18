
using FilenFolderManager.LogUtils;

namespace FilenFolderManager.Modules
{
    internal class App
    {
        private string input = "";
        private string currentDirectory = "";
        private string info = "";
        private InputHandler inputHandler;
        private FolderTasks folderTasks;
        private FileTasks fileTasks;

        private string[] options =
        {
            "1 | Clear console",
            "2 | Change drive",
            "3 | Go to a folder",
            "4 | See list",
            "5 | Open a file",
            "6 | Close a file",
            "7 | Go back",
            "q/Q | Exit"
        };

        private Action[] actions;

        internal App()
        {
            inputHandler = new InputHandler();
            folderTasks = new FolderTasks();
            fileTasks = new FileTasks();
            actions = new Action[] 
            {
                ClearConsole,
                ChangeDrive,
                GotoFolder,
                ListOfFilesAndFolder,
                OpenFile,
                CloseFile,
                GoBack
            };
        }

        internal void Run()
        {
            while(true)
            {
                UpdateInfo();
                Console.WriteLine(info);
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

        private void UpdateInfo()
        {
            info = $"===== INFO =====\nCurrent Directory: {currentDirectory}\n================";
        }

        internal void ShowOptions()
        {
            for(int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }
        }

        internal void ClearConsole()
        {
            Console.Clear();
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

        internal void ListOfFilesAndFolder()
        {
            folderTasks.ShowFilesAndFolder(currentDirectory);
        }

        internal void GotoFolder()
        {
            string[] folders = folderTasks.GetListOfFolders(currentDirectory);

            if(folders != null && folders.Length > 0)
            {
                for(int i = 0; i < folders.Length; ++i)
                {
                    Console.WriteLine($"{i + 1} | {folders[i]}");
                }
                Console.WriteLine("Select a folder from the list...");
                string input = inputHandler.ReadInput(folders.Length);
                currentDirectory = folderTasks.ChangeDirectory(folders[int.Parse(input) - 1]);
            }
            else
            {
                Logger.Info("Current directory doesn't have any folder");
                Console.WriteLine($"Current directory doesn't have any folder.\nCurrent directory is {currentDirectory}");
            }
            
        }

        internal void OpenFile()
        {
            string[] files = folderTasks.GetListOfFiles(currentDirectory);
            if (files != null && files.Length > 0)
            {
                for (int i = 0; i < files.Length; ++i)
                {
                    Console.WriteLine($"{i + 1} | {files[i]}");
                }
                Console.WriteLine("Select a file from the list...");
                string input = inputHandler.ReadInput(files.Length);
                fileTasks.OpenFile(files[int.Parse(input) - 1]);
            }
            else
            {
                Logger.Info("Current directory doesn't have any files");
                Console.WriteLine($"Current directory doesn't have any folder.\nCurrent directory is {currentDirectory}");
            }
        }

        internal void CloseFile()
        {
            string[] openedFiles = fileTasks.GetListOfOpenedFiles();
            if(openedFiles.Length > 0)
            {
                for(int i = 0; i < openedFiles.Length; ++i)
                {
                    Console.WriteLine($"{i + 1} | {openedFiles[i]}");
                }
                Console.WriteLine("Select a file from the list...");
                string input = inputHandler.ReadInput(openedFiles.Length);
                fileTasks.CloseFile(openedFiles[int.Parse(input) - 1]);
            }
            else
            {
                Console.WriteLine("No opened file.");
                Logger.Info("User tried to close file. But no file is opened");
            }
        }

        internal void GoBack()
        {
            currentDirectory = folderTasks.GoBackToPrevious(currentDirectory);
        }
    }
}
