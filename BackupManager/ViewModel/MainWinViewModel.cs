using BackupTaskManager.Commands;
using BackupTaskManager.ViewModels;
using SilentBackuper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackupManager.Model;
using BackupManager.ViewModel;

namespace BackupManager
{
    class MainWinViewModel : ViewModelBase
    {
        IniService iniService;
        public MainWinViewModel()
        {


            iniService = new IniService("settings.ini");
            List<string> sources = iniService.Sources;

                
            
            foreach (var item in sources)
            {
                BackupItems.Add(new BackupItemModel(item));
            }


            EditButtonCmd = new RelayCommand(o => { EditAction(); }, EditBtnCanExecute);
            DeleteButtonCmd = new RelayCommand(o => { DeleteAction(); }, DeleteBtnCanExecute);

            SaveButtonCmd = new RelayCommand(o => { SaveAction(); }, SaveBtnCanExecute);

            MessengerStatic.TaskItemWindowClosedInAddMode += MessengerStatic_TaskItemWindowClosedInAddMode;
            MessengerStatic.TaskItemWindowClosedInEditMode += MessengerStatic_TaskItemWindowClosedInEditMode;

        }

        private void SaveAction()
        {
            iniService.SetSources(BackupItems.Select(x => x.Path).ToList());
            iniService.SaveToFile();

            IsChangedIndicator = false;
        }

        private bool SaveBtnCanExecute(object arg)
        {
            return IsChangedIndicator;
        }

        public bool IsChangedIndicator { get; set; } = false;

        public RelayCommand EditButtonCmd { get; set; }
        public RelayCommand DeleteButtonCmd { get; set; }
        public RelayCommand SaveButtonCmd { get; set; }

        ObservableCollection<BackupItemModel> taskItems = new ObservableCollection<BackupItemModel>();

        public ObservableCollection<BackupItemModel> BackupItems
        {
            get => taskItems;
            set
            {
                taskItems = value;
                OnPropertyChanged("TaskItems");
            }

        }

        BackupItemModel selectedTaskItem;

        public BackupItemModel SelectedTaskItem
        {
            get => selectedTaskItem;
            set => selectedTaskItem = value;
        }
        public bool EditBtnCanExecute(object arg) {

            return true;
        }

        private void EditAction()
        {
            MessengerStatic.NotifyTaskWindowOpened(SelectedTaskItem);
            IsChangedIndicator = true;
        }

        private bool DeleteBtnCanExecute(object arg)
        {
            bool result = (SelectedTaskItem != null) ? true : false;

            return result;
        }

        private void DeleteAction()
        {
            BackupItems.Remove(SelectedTaskItem);
            IsChangedIndicator = true;
        }

        private void MessengerStatic_TaskItemWindowClosedInAddMode(object obj)
        {
            BackupItems.Add((BackupItemModel)obj);
            IsChangedIndicator = true;
        }

        private void MessengerStatic_TaskItemWindowClosedInEditMode(object obj)
        {
            BackupItemModel[] arr = ((BackupItemModel[])obj);
            for (int i = 0; i < BackupItems.Count; i++)
            {
                if (BackupItems.ElementAt(i).Path == arr[0].Path)
                {
                    BackupItems.RemoveAt(i);
                    BackupItems.Insert(i, arr[1]);
                    
                }
            }
        }
    }
}
