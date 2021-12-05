
using MilitaryElite.Interfaces;

namespace MilitaryElite
{
    public abstract class Soldier : ISoldier
    {
        protected Soldier(int id, string firstName, string secondName)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
        }
       
        public int Id { get; }
        public string FirstName { get; }
        public string SecondName { get; }

        public override string ToString()
        {
            return $"Name: {FirstName} {SecondName} Id: {Id} ";
        }
    }
}
