using System;
using Microsoft.VisualBasic;

namespace AnimalFarm.Models
{
    public class Chicken
    {
        private const int MinAge = 0;
        private const int MaxAge = 15;

        private string name;
        private int age;

        public Chicken(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(Name)} cannot be empty.");
                }
                this.name = value;
            }
        }

        public int Age
        {
            get => age;
            private set
            {
                if (value <= MinAge || value > MaxAge)
                {
                    throw new ArgumentException($"{nameof(Age)} should be between {MinAge} and {MaxAge}.");
                }
                this.age = value;
            }
        }

        public double ProductPerDay => CalculateProductPerDay();

        private double CalculateProductPerDay() => Age
            switch
            {
               int x when x  >= 0 && x < 4 => 1.5,
               int x when x >= 4 && x < 8 => 2,
               int x when x >= 8 && x < 12 => 1,
                _ => 0.75
            };
    }
}
