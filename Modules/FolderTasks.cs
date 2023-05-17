
using FilenFolderManager.LogUtils;

namespace FilenFolderManager.Modules
{
    internal class FolderTasks
    {
        internal int numberOfDrives;
        private DriveInfo[] drives;

        public FolderTasks()
        {
            drives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed).ToArray();
            numberOfDrives = drives.Length;
        }

        internal void ShowDrives()
        {
            for (int i = 0; i < drives.Length; ++i)
            {
                Console.WriteLine($"{i + 1} | {drives[i].Name}");
            }
        }

        internal bool ValidDriveSelected(string input)
        {
            int driveNo;
            return int.TryParse(input, out driveNo) && (driveNo >= 1 && driveNo <= drives.Length);
        }

        internal string SelectDrive(string input)
        {
            return ChangeDirectory(drives[int.Parse(input) - 1].Name);
        }

        internal string ChangeDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.SetCurrentDirectory(directory);
                return Directory.GetCurrentDirectory();
            }
            else
            {
                Logger.Info("Invalid path");
                Console.WriteLine("Invalid path");
                return "";
            }
        }

        internal void ShowFilesAndFolder(string directory)
        {
            if(Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                string[] folders = Directory.GetDirectories(directory);

                if (files.Length > 0)
                {
                    Console.WriteLine("Files: ");
                    for (int i = 0; i < files.Length; ++i)
                    {
                        Console.WriteLine(files[i]);
                    }
                }
                else
                {
                    Console.WriteLine($"No files in {directory}");
                }

                if (folders.Length > 0)
                {
                    Console.WriteLine("Folders: ");
                    for (int i = 0; i < folders.Length; ++i)
                    {
                        Console.WriteLine(folders[i]);
                    }
                }
                else
                {
                    Console.WriteLine($"No folders in {directory}");
                }
            }
            else
            {
                Console.WriteLine("Invalid directory!");
                Logger.Info("Invalid directory found while showing list of files and folder");
            }
        }

        internal string[] GetListOfFolders(string directory)
        {
            if(Directory.Exists(directory))
            {
                string[] folders = Directory.GetDirectories(directory);
                return folders;
            }
            return null;
        }

        internal string[] GetListOfFiles(string directory)
        {
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                return files;
            }
            return null;
        }

        internal string GoBackToPrevious(string directory)
        {
            int index = directory.LastIndexOf("\\");
            if (index == 2) //Handle root directory separately
            {
                directory = directory.Substring(0, index);
                directory += "\\";
                return ChangeDirectory(directory);
            }
            if (index >= 0)
            {
                directory = directory.Substring(0, index);
                return ChangeDirectory(directory);
            }
            else
            {
                Console.WriteLine("Can't go back");
                Logger.Info("Can't go back to previous. Base directory");
                return directory;
            }
        }
    }
}
