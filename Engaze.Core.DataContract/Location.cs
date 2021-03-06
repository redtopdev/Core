﻿

using System;

namespace Engaze.Core.DataContract
{
    public class Location
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Eta { get; set; }

        public string ArrivalStatus { get; set; }
    }
}
