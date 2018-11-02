using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TORSHIA.Models;

namespace TORSHIA.Web.Services.Contracts
{
    public interface IReportsService
    {
        IQueryable<Report> All();

        Report GetReportById(int id);
    }
}
