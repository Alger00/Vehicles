using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class Boat : Vehicle, ISwimmable
    {
        public Boat(string make, string model) : base(make, model)
        {
        }
        public override string Move(double km)
        {
            return Swim(km);
        }
        public string Swim(double km)
        {
            ValidateDistance(km);
            Odometer += km;
            return $"{Make} sailed {km} km. Odometer: {Odometer} km.";
        }
    }
}
