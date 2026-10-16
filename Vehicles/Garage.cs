using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class Garage
    {
        public string Name { get; set; }
        public List<Vehicle> Vehicles { get; } = new();
        public Garage(string name)
        {
            Name = name;
        }
    }
}
