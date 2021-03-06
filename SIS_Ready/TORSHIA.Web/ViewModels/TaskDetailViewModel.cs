﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TORSHIA.Web.ViewModels
{
    public class TaskDetailViewModel
    {
        public string Title { get; set; }

        public string DueDate { get; set; }

        public string Participants { get; set; }

        public string Description { get; set; }

        public string AffectedSectors { get; set; }

        public int Level { get; set; }
    }
}
