using System;
using System.IO;

namespace SilentBackuper
{
    class Program
    {

        static void Main(string[] args)
        {
            
            string settingsFile = Path.Combine(Environment.CurrentDirectory, "settings.ini");
            if(!File.Exists(settingsFile))
            {
                Console.WriteLine("Settings file wasn't found");
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadKey();
                return;
                
            }
            BackupService service = new BackupService(settingsFile);

            service.PrepareMainDestination();
            service.PrepareMonthFolder();
            service.PrepareDayFolder();
            service.PrintHeader();
            service.DoBackup();

        }

    }
}
