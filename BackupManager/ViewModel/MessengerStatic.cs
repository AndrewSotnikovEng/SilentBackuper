using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupTaskManager.ViewModels
{
    public class MessengerStatic



    {
        public static event Action<object> TaskItemWindowClosedInAddMode;

        public static void NotifyTaskWindowInAddModelClosed(object taskItem)
            => TaskItemWindowClosedInAddMode?.Invoke(taskItem);


        public static event Action<object> TaskItemWindowClosedInEditMode;
        public static void NotifyTaskWindowInEditModeClosed(object taskItem)
            => TaskItemWindowClosedInEditMode?.Invoke(taskItem);


        public static event Action<object> TaskItemWindowOpened;

        public static void NotifyTaskWindowOpened(object taskItem)
            => TaskItemWindowOpened?.Invoke(taskItem);

    }
}