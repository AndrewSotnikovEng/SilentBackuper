using BackupTaskManager.View;
using BackupTaskManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BackupManager.Model;

namespace BackupManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWinViewModel();
            //Closing += MainWindow_Closing;
            MessengerStatic.TaskItemWindowOpened += MessengerStatic_TaskItemWindowOpened;
        }

        void MainWindowClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BackupItemWindow taskItemWin = new BackupItemWindow();
            taskItemWin.Show();

        }

        private void MessengerStatic_TaskItemWindowOpened(object obj)
        {
            BackupItemModel selectedItem = (BackupItemModel)obj;
            BackupItemWindow taskItemWin = new BackupItemWindow(selectedItem);
            taskItemWin.Show();

        }


    }
}
