
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
                Console.WriteLine(Directory.GetCurrentDirectory());
                return Directory.GetCurrentDirectory();
            }
            else
            {
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

        internal void SearchFolder(string folderName)
        {

        }
    }
}
