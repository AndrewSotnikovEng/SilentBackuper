using BackupTaskManager.Commands;
using BackupTaskManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupManager.Model;
using BackupTaskManager.ViewModels;

namespace BackupManager.ViewModels
{
    class BackupItemWindowViewModel : IDataErrorInfo
    {

        public string Path { get; set; }


        BackupItemModel sourceTaskItem;

        public RelayCommand CommitTaskItemCmd { get; }

        public RelayCommand CancelTaskItemCmd { get; }

        public string Error { get; set; }

        public string this[string columnName]
        { 
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Path":
                        if (!File.Exists(Path) && !Directory.Exists(Path)) error = "File not exist!";
                        break;
                }
                this.Error = error;

                return error;
            }
        }

        public BackupItemWindowViewModel()
        {
            CommitTaskItemCmd = new RelayCommand(o => { CommitTaskInAddMode(); }, CommitTaskItemCanExecute);
            CancelTaskItemCmd = new RelayCommand(o => { CancelTask(); });
        }

        public BackupItemWindowViewModel(BackupItemModel selectedItem)
        {
            Path = selectedItem.Path;


            sourceTaskItem = new BackupItemModel(Path);
            CommitTaskItemCmd = new RelayCommand(o => { CommitTaskInEditMode(); }, CommitTaskItemCanExecute);
            CancelTaskItemCmd = new RelayCommand(o => { CancelTask(); });
        }

        private void CommitTaskInEditMode()
        {
            BackupItemModel taskItem = new BackupItemModel(Path);
            BackupItemModel[] taskItems = { sourceTaskItem, taskItem };
            MessengerStatic.NotifyTaskWindowInEditModeClosed(taskItems);
        }

        public void CommitTaskInAddMode()
        {

            BackupItemModel taskItem = new BackupItemModel(Path);
            MessengerStatic.NotifyTaskWindowInAddModelClosed(taskItem);
        }

        public bool CommitTaskItemCanExecute(object parameter)
        {
            bool result = false;
            if (String.IsNullOrEmpty(Error))
            {
                result = true;
            }

            return result;
        }

        public void CancelTask()
        {

            MessengerStatic.NotifyTaskWindowCanceled(null);
        }


    }
}
