using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TORSHIA.Models;
using TORSHIA.Web.ViewModels;

namespace TORSHIA.Web.Services.Contracts
{
    public interface ITasksService
    {
        IQueryable<Task> All();

        Task GetTaskById(int id);

        void CreateTask(Task model);
    }
}
