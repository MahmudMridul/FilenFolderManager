using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenFolderManager.Modules
{
    internal class InputHandler
    {
        
        public InputHandler()
        {
            
        }

        internal string ReadInput(int numOfOptions)
        {
            string input = Console.ReadLine();

            while(string.IsNullOrEmpty(input) || !ValidInput(input, numOfOptions))
            {
                Console.WriteLine("Invalid input. Input has to be non empty and between given options...");
                input = Console.ReadLine();
            }
            return input;
        }

        //private void ShowOptions()
        //{
        //    for(int i = 0; i < options.Length; i++)
        //    {
        //        Console.WriteLine(options[i]);
        //    }
        //}

        private bool ValidInput(string input, int numOfOptions)
        {
            int n;
            return (int.TryParse(input, out n) && (n >= 1 && n <= numOfOptions) || Exit(input)); 
        }

        internal bool Exit(string input)
        {
            return input.ToLower().Equals("q");
        }

        

        

        

        
    }
}
