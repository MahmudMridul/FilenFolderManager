
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

        private string ChangeDirectory(string directory)
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

        internal void ChangeDrive()
        {

        }
    }
}
