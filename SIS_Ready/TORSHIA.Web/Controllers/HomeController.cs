using SIS.Framework.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TORSHIA.Models;
using TORSHIA.Web.Controllers.Base;
using TORSHIA.Web.Services;
using TORSHIA.Web.Services.Contracts;
using TORSHIA.Web.ViewModels;

namespace TORSHIA.Web.Controllers
{
    public class HomeController : BaseController
    {
        private ITasksService tasksService;

        public HomeController(TasksService tasksService)
        {
            this.tasksService = tasksService;      
        }

        public IActionResult Index()
        {
            var userIsSignedIn = this.Identity != null;
            if (!userIsSignedIn)
                return this.View();

            var tasks = this.tasksService.All().ToList();
            var wrapperViewModel = new List<TaskViewModelWrapper>();
            wrapperViewModel.Add(new TaskViewModelWrapper());

            TaskViewModelWrapper lastAddedWrapper = new TaskViewModelWrapper();

            for (int i = 0; i < tasks.Count(); i++)
            {
                if (i % 5 == 0)
                {
                    wrapperViewModel.Add(new TaskViewModelWrapper());
                }

                lastAddedWrapper = wrapperViewModel.Last();

                lastAddedWrapper.TaskViewModels.Add(new TaskViewModel
                {
                    BackgroundColor = "torshia",
                    EmptyTask = "block",
                    Level = tasks[i].AffectedSectors.Count,
                    Title = tasks[i].Title,
                    Id = i + 1
                });
            }

            if(lastAddedWrapper.TaskViewModels.Count != 5 
                && lastAddedWrapper.TaskViewModels.Count != 0)
            {
                for (int i = 0; i <= 5 - lastAddedWrapper.TaskViewModels.Count; i++)
                {
                    lastAddedWrapper.TaskViewModels.Add(new TaskViewModel
                    {
                        BackgroundColor = "white",
                        EmptyTask = "none",
                        Id = i + tasks.Count
                    });
                }
            }

            this.Model.Data["TaskViewModels"] = wrapperViewModel;

            return this.View();
        }
    }
}
