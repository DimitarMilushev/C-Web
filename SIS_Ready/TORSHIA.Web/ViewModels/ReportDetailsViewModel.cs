using System;
using System.Collections.Generic;
using System.Text;

namespace TORSHIA.Web.ViewModels
{
    public class ReportDetailsViewModel
    {
        public int Id { get; set; }

        public string TaskTitle { get; set; }

        public int Level { get; set; }

        public string Status { get; set; }

        public string DueDate { get; set; }

        public string ReportedOn { get; set; }

        public string Reporter { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }

        public string Description { get; set; }
    }
}
