using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;
using SIS.Framework.Attributes.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using TORSHIA.Models;
using TORSHIA.Models.Enums;
using TORSHIA.Web.Controllers.Base;
using TORSHIA.Web.Services;
using TORSHIA.Web.Services.Contracts;
using TORSHIA.Web.ViewModels;

namespace TORSHIA.Web.Controllers
{
    public class TasksController : BaseController
    {
        private readonly ITasksService tasksService;

        public TasksController(TasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        public IActionResult Details(int id)
        {
            var task = tasksService.GetTaskById(id);

            var tasksDetailsViewModel = new TaskDetailViewModel
            {
                Title = task.Title,
                DueDate = task.DueDate.ToShortDateString(),
                Description = task.Description,
                AffectedSectors = string.Join(", ", task.AffectedSectors.Select(asf => asf.Sector)),
                Participants = task.Participants,
                Level = task.AffectedSectors.Count
            };

            this.Model.Data["TaskDetails"] = tasksDetailsViewModel;

            return this.View();
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult Create() => this.View();

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(TaskInputViewModel model)
        {
            var affectedSectors = new HashSet<TaskSector>();
            var sectors = model.AffectedSectors.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var sector in sectors)
            {
                affectedSectors.Add(new TaskSector() { Sector = (Sector)Enum.Parse(typeof(Sector), sector) });
            }

            var tasksCreateViewModel = new Task
            {
                Title = model.Title,
                Description = model.Description,
                Participants = model.Participants,
                AffectedSectors = affectedSectors,
                DueDate = DateTime.Parse(model.DueDate)
            };

            tasksService.CreateTask(tasksCreateViewModel);

            return RedirectToAction("/");
        }
    }
}
