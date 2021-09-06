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
            {   service = new BackupService("settings.ini");}
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
