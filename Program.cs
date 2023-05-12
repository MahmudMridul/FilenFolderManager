using FilenFolderManager.LogUtils;
using FilenFolderManager.Modules;
using System.Diagnostics;
using System.IO;

namespace FilenFolderManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger.Info("Instantiating app");
            App app = new App();
            Logger.Info("Starting app");
            app.Run();

            //Process.Start("c:\\program files\\microsoft office\\office16\\winword.exe", "E:\\CV\\Joining Letter.docx");
        }
    }
}