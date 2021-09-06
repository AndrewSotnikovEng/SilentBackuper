using UiWrapper.Model;
using System.Windows;
using UiWrapper.ViewModels;
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
            DataContext = new TaskItemWindowViewModel();
            MessengerStatic.TaskItemWindowClosedInAddMode += CloseWin;
            


        }


        public BackupItemWindow(BackupItemModel selectedItem)
        {
            InitializeComponent();
            DataContext = new TaskItemWindowViewModel(selectedItem);
            MessengerStatic.TaskItemWindowClosedInEditMode += CloseWin;

        }

        private void CloseWin(object obj)
        {
            this.Close();
        }




    }
}
