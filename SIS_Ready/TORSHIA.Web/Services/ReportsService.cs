using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TORSHIA.Data;
using TORSHIA.Models;
using TORSHIA.Web.Services.Contracts;

namespace TORSHIA.Web.Services
{
    public class ReportsService : IReportsService
    {
        private TorshiaContext context;

        public ReportsService(TorshiaContext context)
        {
            this.context = context;
        }

        public IQueryable<Report> All()
        => this.context.Reports
            .Include(x => x.Task)
            .Include(x => x.Task.AffectedSectors);
            

        public Report GetReportById(int id)
        => this.context.Reports
            .Include(x => x.Task)
            .Include(x => x.Task.AffectedSectors)
            .Include(x => x.Reporter)
            .FirstOrDefault(x => x.Id == id);
    }
}
