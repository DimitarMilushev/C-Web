using System;
using System.Collections.Generic;
using System.Text;

namespace TORSHIA.Web.ViewModels
{
    public class TaskViewModelWrapper
    {
        public TaskViewModelWrapper()
        {
            TaskViewModels = new List<TaskViewModel>();
        }

        public ICollection<TaskViewModel> TaskViewModels { get; set; }
    }
}
