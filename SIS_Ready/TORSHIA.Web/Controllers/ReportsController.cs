using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;
using SIS.Framework.Attributes.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TORSHIA.Web.Controllers.Base;
using TORSHIA.Web.Services;
using TORSHIA.Web.Services.Contracts;
using TORSHIA.Web.ViewModels;

namespace TORSHIA.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IReportsService reportsService;

        public ReportsController(ReportsService reportsService)
        {
            this.reportsService = reportsService;
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult All()
        {
            var reports = this.reportsService.All();

            var reportsViewModel = new HashSet<ReportViewModel>();
            foreach (var report in reports)
            {
                reportsViewModel.Add(new ReportViewModel
                {
                    Id = report.Id,
                    TaskTitle = report.Task.Title,
                    Level = report.Task.AffectedSectors.Count,
                    Status = report.Status.ToString()
                });
            }

            this.Model.Data["Reports"] = reportsViewModel;

            return this.View();
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var report = this.reportsService.GetReportById(Id);

            var reportDetailsViewModel = new ReportDetailsViewModel
            {
                TaskTitle = report.Task.Title,
                Id = report.Id,
                Description = report.Task.Description,
                DueDate = report.Task.DueDate.ToShortDateString(),
                ReportedOn = report.ReportedOn.ToShortDateString(),
                Reporter = report.Reporter.Username,
                Level = report.Task.AffectedSectors.Count,
                Participants = report.Task.Participants,
                Status = report.Status.ToString(),
                AffectedSectors = string.Join(" ", report.Task.AffectedSectors.Select(asf => asf.Sector))
            };

            this.Model.Data["ReportDetails"] = reportDetailsViewModel;

            return this.View();
        }
    }
}
