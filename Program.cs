namespace FilenFolderManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TEST
            string currDir = Directory.GetCurrentDirectory();
            string[] filesAndFolders = Directory.GetFileSystemEntries(currDir);

            foreach(string fileOrFolder in filesAndFolders)
            {
                Console.WriteLine(fileOrFolder);
            }
        }
    }
}