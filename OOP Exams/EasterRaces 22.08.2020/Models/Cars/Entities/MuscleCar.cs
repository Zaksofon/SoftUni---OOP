﻿
using System;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models
{
    public class MuscleCar : Car
    {
        private const double cubicCentimeters = 5000;
        private const int minHorsePower = 400;
        private const int maxHorsePower = 600;

        public MuscleCar(string model, int horsePower) 
            : base(model, horsePower, cubicCentimeters, minHorsePower, maxHorsePower)
        {
        }
    }
}
