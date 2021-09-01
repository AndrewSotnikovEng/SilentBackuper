using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilentBackuper
{
    class BackupService
    {
        FileIniDataParser Parser = new FileIniDataParser();
        string SettingsFile { get; set; }

        IniData Data { get
            {
                return Parser.ReadFile(SettingsFile);
            } 
        }

        string Destination
        {
            get
            {
                return Data["Settings"]["destination"];
            }
        }

        public BackupService(string settingsFile)
        {
            SettingsFile = settingsFile;
        }       


        public void PrepareMainDestination()
        {
            //create main backup destination           

            if (!Directory.Exists(Destination))
            {
                Directory.CreateDirectory(Destination);
            }
        }

        public  void PrepareMonthFolder()
        {
            if (!Directory.Exists(MonthFolderPath))
            {
                Directory.CreateDirectory(MonthFolderPath);
            }
        }

        string MonthFolderPath
        {
            get {
                return Path.Combine(Destination,
                    DateTime.Now.ToString("MM") + "__" + DateTime.Now.ToString("MMMM"));
            }
        }

        
        public void DoBackup()
        {
            //get backups
            //if it files, get all files
            foreach (var key in Data["Settings"])
            {
                if (key.KeyName.Contains("source"))
                {
                    string sourcePath = Data["Settings"][key.KeyName];
                    if(!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
                    {
                        Console.WriteLine($"Not exists - {sourcePath}" );
                    }
                    if (Directory.Exists(sourcePath))
                    {
                        string directorySource = sourcePath;
                        string zipFilePath =    $"{MonthFolderPath}\\" +
                                                $"{Path.GetFileName(directorySource)}_" +
                                                $"{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}.zip";

                        CreateZipDirectory(zipFilePath, directorySource);
                    } else
                    {
                        string fileSource = sourcePath;
                        string zipFilePath =    $"{MonthFolderPath}\\" +
                                                $"{Path.GetFileNameWithoutExtension(fileSource)}_" +
                                                $"{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}.zip";

                        CreateZipFile(zipFilePath, fileSource);
                    }
                }
            }
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();
            //if it dirs
        }

        public void CreateZipFile(string zipFilePath, string filePath)
        {
            if (File.Exists(zipFilePath))
            {
                Console.WriteLine($"Path {zipFilePath} already existed");
                return;
            }
            // Create and open a new ZIP file
            var zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
            // Add the entry for each file
            zip.CreateEntryFromFile(filePath, Path.GetFileName(filePath), CompressionLevel.Optimal);
            Console.WriteLine($"Backup done {zipFilePath}");
            // Dispose of the object when we are done
            zip.Dispose();
        }

        public void CreateZipDirectory(string zipFilePath, string filePath)
        {
            if (File.Exists(zipFilePath))
            {
                Console.WriteLine($"Path {zipFilePath} already existed");
                return;
            }
            ZipFile.CreateFromDirectory(filePath, zipFilePath);
            Console.WriteLine($"Backup done {zipFilePath}");
        }


        public void PrintHeader()
        {
            Console.WriteLine("\n" + string.Concat(Enumerable.Repeat("*", 40)));
            Console.WriteLine(String.Format("{0,-10}{1,-10}" , " ", DateTime.Now.ToString()));
            Console.WriteLine(string.Concat(Enumerable.Repeat("*", 40)) + "\n");
        }
    }
}
