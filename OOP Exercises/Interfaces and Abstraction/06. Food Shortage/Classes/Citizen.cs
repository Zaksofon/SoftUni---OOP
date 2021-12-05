using FoodShortage.Interfaces;

namespace FoodShortage.Classes
{
    public class Citizen : IIdentable, IBuyer
    {
        public Citizen(string name, int age, string id, string birthdate, int food)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
            Food = food;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public string Birthdate { get; set; }
        public int Food { get; set; }

        public void BuyFood()
        {
            Food += 10;
        }
    }
}