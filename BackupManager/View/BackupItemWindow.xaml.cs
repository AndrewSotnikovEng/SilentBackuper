using BackupManager.Model;
using System.Windows;
using BackupManager.ViewModels;
using BackupTaskManager.ViewModels;

namespace BackupTaskManager.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BackupItemWindow : Window
    {
        public BackupItemWindow()
        {
            InitializeComponent();
            DataContext = new BackupItemWindowViewModel();
            MessengerStatic.TaskItemWindowClosedInAddMode += CloseWin;
            MessengerStatic.TaskItemWindowCanceled += CloseWin;
        }


        public BackupItemWindow(BackupItemModel selectedItem)
        {
            InitializeComponent();
            DataContext = new BackupItemWindowViewModel(selectedItem);
            MessengerStatic.TaskItemWindowClosedInEditMode += CloseWin;
            MessengerStatic.TaskItemWindowCanceled += CloseWin;
        }

        private void CloseWin(object obj)
        {
            this.Close();
        }
    }
}
