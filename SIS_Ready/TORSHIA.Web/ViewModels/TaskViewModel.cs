using System;
using System.Collections.Generic;
using System.Text;

namespace TORSHIA.Web.ViewModels
{
    public class TaskViewModel
    {
        public string EmptyTask { get; set; }

        public string BackgroundColor { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

         public int Id { get; set; }

        // public DateTime? DueDate { get; set; }
        //
        // public bool IsReported { get; set; }
        //
        // public string Description { get; set; }
        //
        // public string[] Participants { get; set; }
    }
}
