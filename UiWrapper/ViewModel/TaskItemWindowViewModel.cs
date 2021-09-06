using BackupTaskManager.Commands;
using BackupTaskManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiWrapper.Model;
using BackupTaskManager.ViewModels;

namespace UiWrapper.ViewModels
{
    class TaskItemWindowViewModel : IDataErrorInfo
    {

        public string Path { get; set; }


        BackupItemModel sourceTaskItem;

        public RelayCommand CommitTaskItemCmd { get; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        { 
            get
            {
                string error = String.Empty;

                switch (columnName)
                {
                    case "Path":
                        if (String.IsNullOrEmpty(Path)) error = "Empty Name";
                        break;

                }

                return error;

            }
        
        }

        public TaskItemWindowViewModel()
        {
            CommitTaskItemCmd = new RelayCommand(o => { CommitTaskInAddMode(); });
        }

        public TaskItemWindowViewModel(BackupItemModel selectedItem)
        {
            Path = selectedItem.Path;


            sourceTaskItem = new BackupItemModel(Path);

            CommitTaskItemCmd = new RelayCommand(o => { CommitTaskInEditMode(); });
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

    }
}
