using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class AmphibiousCar : Vehicle, IDriveable, ISwimmable
    {
        public AmphibiousCar(string make, string model) : base(make, model)
        {
        }

        public override string Move(double km)
        {
            return Drive(km);
        }

        public string Drive(double km)
        {
            ValidateDistance(km);
            Odometer += km;
            return $"{Make} drove {km} km. Odometer: {Odometer} km.";
        }

        public string Swim(double km)
        {
            ValidateDistance(km);
            Odometer += km;
            return $"{Make} sailed {km} km. Odometer: {Odometer} km.";
        }
    }
}