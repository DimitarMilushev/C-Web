using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TORSHIA.Data;
using TORSHIA.Models;
using TORSHIA.Models.Enums;
using TORSHIA.Web.Services.Contracts;
using TORSHIA.Web.ViewModels;

namespace TORSHIA.Web.Services
{
    public class TasksService : ITasksService
    {
        private TorshiaContext context;

        public TasksService(TorshiaContext context)
        {
            this.context = context;
        }

        public IQueryable<Task> All() => this.context.Tasks;

        public Task GetTaskById(int id)
            => this.context.Tasks
            .Include(x => x.AffectedSectors)
            .FirstOrDefault(x => x.Id == id);

        public void CreateTask(Task task)
        {
          
            this.context.Tasks.Add(task);
            context.SaveChanges();
        }
    }
}
