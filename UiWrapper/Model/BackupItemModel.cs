using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiWrapper.Model
{
    public class BackupItemModel
    {
        public BackupItemModel(string path)
        {
            Path = path;
        }

        public string Path { get; set; }


    }
}
