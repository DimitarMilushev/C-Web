﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IRunesWebApp.Models
{
    public class Track : BaseEntity<string>
    {
        public Track()
        {
            this.Albums = new HashSet<TrackAlbum>();
        }

        public string Name { get; set; }

        // URL
        public string Link { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<TrackAlbum> Albums { get; set; }
    }
}