﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Models
{
    public class Receipt
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string RecipientId { get; set; }

        public User Recipient { get; set; }

        public string PackageId { get; set; }

        public Package Package { get; set; }
    }
}
