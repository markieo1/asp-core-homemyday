using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models.Domain
{
    public class Holiday
    {
        public string Continent { get; set; }

        public string Country { get; set; }

        public string Place { get; set; }

        public string Category { get; set; }

        public string Destination { get; set; }

        public bool Recommended { get; set; }

        public int Room { get; set; }

        public int Bed { get; set; }

        public int Price { get; set; }

        public int Reviews { get; set; }
    }
}
