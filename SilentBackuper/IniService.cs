using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//System.Collections.Generic.Dictionary;


namespace SilentBackuper
{
    public class IniService
    {

        string _sourceHeader = "#SOURCES";
        string _destiantionHeader = "#DESTINATION";
        string _sourceCondition = "source[] =";
        string _destinationCondition = "destination =";

        public IniService(string settingsFile)
        {
            this.SettingsFile = settingsFile;
            ReadFile();
            RemoveEmptyStrings();
        }

        public List<string> Sources
        {
            get
            {
                return linesList.Where(s => s.Contains(_sourceCondition) && !s.StartsWith("#"))
                    .Select(s => s.Split('=')[1]).ToList();
            }
        }

        public string Destination
        {
            get
            {
                return linesList.Where(s => s.Contains(_destinationCondition) && !s.StartsWith("#"))
                  .Select(s => s.Split('=')[1]).FirstOrDefault();
            }
        }

        List<string> linesList;

        public void ReadFile()
        {
            linesList = File.ReadAllLines(SettingsFile).ToList();
        }


        string SettingsFile { get; set; }

        public void AddSource(string source)
        {
            for (int i = 0; i < linesList.Count; i++)
            {
                if (!linesList[i + 1].Contains(source) && !linesList[i + 1].StartsWith("#"))
                {
                    linesList.Insert(i + 1, $"{_sourceCondition} {source}");
                }
            }
        }

        public void SetSources(List<string> sources)
        {

            List<string> linesToDelete = linesList.Where(l => l.Contains(_sourceCondition)
                                                        && !l.StartsWith("#")).ToList();

            foreach (var item in linesToDelete)
            {
                linesList.Remove(item);
            }

            int rowToAppend = 1;
            foreach (var item in linesList)
            {
                if (item.StartsWith(_sourceHeader))
                {
                    break;
                }
                rowToAppend++;
            }

            sources.Reverse();
            foreach (var source in sources)
            {
                linesList.Insert(rowToAppend, $"{_sourceCondition} {source}");
            }
        }


        public void RemoveEmptyStrings()
        {
            if (linesList == null) { return; }

            List<string> linesToDelete = linesList.Where(l =>
                                                String.IsNullOrEmpty(l)
                                            ||
                                                !l.StartsWith("#")
                                                    && !l.StartsWith(_sourceCondition)
                                                && !l.StartsWith(_destinationCondition)
                                            ).ToList();

            foreach (var item in linesToDelete)
            {
                linesList.Remove(item);
            }
        }

        public void SaveToFile()
        {
            TextWriter tw = new StreamWriter(this.SettingsFile);

            foreach (String s in linesList)
                tw.WriteLine(s);
            tw.Close();
        }
    }
}


