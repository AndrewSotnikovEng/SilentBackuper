using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using IniParser.Model;
using IniParser;
using System.Runtime.InteropServices;
using CommandLine.Text;
using CommandLine;

namespace SilentBackuper
{
    class Program
    {

        static void Main(string[] args)
        {
            BackupService service = null;
            CmdOptions options = new CmdOptions();

            Parser.Default.ParseArguments<CmdOptions>(args).WithParsed<CmdOptions>(o => options = o );

            if (options.SettingsFile == null)
            {   service = new BackupService("settings.ini");        }
            else
            {   service = new BackupService(options.SettingsFile);  }
                        
            service.PrepareMainDestination();
            service.PrepareMonthFolder();
            service.PrintHeader();
            service.DoBackup();
            

        }


        public class CmdOptions
        {
            [Option('s', "settings", Required = false, HelpText = "Path to settings file")]
            public string SettingsFile { get; set; }

            public int ConcurrentCount { get; set; }
        }
    }
}
