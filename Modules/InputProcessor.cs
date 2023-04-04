using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenFolderManager.Modules
{
    internal class InputProcessor
    {
        private string[] options = 
        {
            "1 | Go to "
        };

        private DriveInfo[] drives;

        public InputProcessor()
        {
            drives = DriveInfo.GetDrives();
        }

        public void Process()
        {
            Console.WriteLine("Press q/Q to exit");
            while (true)
            {
                string input = ReadInput();
                if (ValidInput(input))
                {
                    if (input.ToLower().Equals("q"))
                    {
                        Console.WriteLine("Exiting program...");
                        break;
                    }
                    ShowDrives();
                    Console.WriteLine("Select a drive to continue...");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        private string ReadInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        private bool ValidInput(string input)
        {
            if(string.IsNullOrEmpty(input)) return false;
            return true;
        }

        private void ShowDrives()
        {
            for(int i = 0; i < drives.Length; ++i)
            {
                Console.WriteLine($"{i + 1} | {drives[i].Name} | {drives[i].DriveType}");
            }
        }
    }
}
