using FilenFolderManager.LogUtils;
using FilenFolderManager.Modules;

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
        }
    }
}