﻿using System;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models
{
    public class SportsCar : Car
    {
        private const double cubicCentimeters = 3000;
        private const int minHorsePower = 250;
        private const int maxHorsePower = 450;

        public SportsCar(string model, int horsePower)
            : base(model, horsePower, cubicCentimeters, minHorsePower, maxHorsePower)
        {
            
        }
    }
}