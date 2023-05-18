using FilenFolderManager.LogUtils;
using Microsoft.Win32;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FilenFolderManager.Modules
{
    internal class FileTasks
    {
        private Dictionary<string, string> file_process;
        private List<string> openedFiles;

        public FileTasks()
        {
            file_process = new Dictionary<string, string>();
            openedFiles = new List<string>();
        }

        internal void OpenFile(string filePath)
        {
            if(Path.IsPathRooted(filePath) && File.Exists(filePath))
            {
                try
                {
                    string extension = Path.GetExtension(filePath);

                    if(filePath.Contains(" "))
                    {
                        Logger.Info("filePath contains space");
                        filePath = "\"" + filePath + "\"";
                        Logger.Info($"Modified filePath {filePath}");
                    }
                    RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension);
                    if (key != null)
                    {
                        object defaultValue = key.GetValue("");
                        if (defaultValue != null)
                        {
                            RegistryKey appKey = Registry.ClassesRoot.OpenSubKey(defaultValue.ToString() + "\\shell\\open\\command");
                            if (appKey != null)
                            {
                                object appValue = appKey.GetValue("");
                                if (appValue != null)
                                {
                                    string applicationPath = appValue.ToString().ToLower();
                                    string appPath = Regex.Replace(applicationPath, @"[^\u0000-\u007F]+", string.Empty);
                                    appPath = appPath.Trim();
                                    Logger.Info($"appPath: {appPath}");

                                    int index = appPath.IndexOf(".exe");
                                    if(index >= 0)
                                    {
                                        appPath = appPath.Substring(0, index + 4);
                                        appPath = appPath.Replace("\"", "");
                                    }
                                    Logger.Info($"Modified appPath: {appPath}");
                                    file_process.Add(filePath, appPath);
                                    openedFiles.Add(filePath);

                                    Process.Start(appPath, filePath);
                                }
                                else
                                {
                                    Console.WriteLine("Something went wrong");
                                    Logger.Info("App value is null");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Something went wrong");
                                Logger.Info("App Key is null");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                            Logger.Info("Defualt value is null");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong");
                        Logger.Info("Registry Key is null");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Info("Exception occured. Message: " + ex.Message);
                }
            }
            else
            {
                Logger.Info("File path or file doesn't exits");
                Console.WriteLine("Something wrong with the file path/file name");
            }
        }

        internal void CloseFile(string filePath)
        {
            string processName = Path.GetFileNameWithoutExtension(file_process[filePath]);
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                Console.WriteLine("No file found.");
                Logger.Info($"There are no process named {file_process[filePath]}");
                return;
            }
            foreach (Process process in processes)
            {
                try
                {
                    string processFileName = process.MainModule.FileName;
                    if (processFileName.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                    {
                        process.Kill();
                        Console.WriteLine($"{filePath} closed.");
                        Logger.Info($"{filePath} closed.");
                        file_process.Remove(filePath);
                        openedFiles.Remove(filePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not close file: {filePath}");
                    Logger.Info($"Error trying to close file- {filePath} : {ex.Message}");
                }
            }
        }

        internal string[] GetListOfOpenedFiles()
        {
            return openedFiles.ToArray();
        }
    }
}
