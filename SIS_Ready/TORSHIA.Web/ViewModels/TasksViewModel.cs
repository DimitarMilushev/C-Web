using System;
using System.Collections.Generic;
using System.Text;
using TORSHIA.Models;

namespace TORSHIA.Web.ViewModels
{
    public class TasksViewModel
    {
        public TasksViewModel()
        {
            this.TaskViewModels = new List<TaskViewModel>();
        }

        public ICollection<TaskViewModel> TaskViewModels; 
    }
}
