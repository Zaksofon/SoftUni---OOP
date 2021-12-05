using FoodShortage.Interfaces;
using System;

namespace FoodShortage.Classes
{
    public class Rebel : IBuyer
    {
        public Rebel(string name, int age, string @group, int food)
        {
            Name = name;
            Age = age;
            Group = @group;
            Food = food;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }
        public int Food { get; set; }

        public void BuyFood()
        {
            Food += 5;
        }
    }
}