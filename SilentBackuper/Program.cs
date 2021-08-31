using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using IniParser.Model;
using IniParser;

namespace SilentBackuper
{
    class Program
    {
        static void Main(string[] args)
        {
            BackupService service = new BackupService("settings.ini");

            service.PrepareMainDestination();
            service.PrepareMonthFolder();
            service.PrintHeader();
            service.DoBackup();
        }

    }
}
