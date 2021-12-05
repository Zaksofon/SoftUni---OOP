
namespace MilitaryElite.Classes
{
    public class Private : Soldier, IPrivate
    {
        public Private(int id, string firstName, string secondName, decimal salary) : base(id, firstName, secondName)
        {
            Salary = salary;
        }

        public decimal Salary { get; }

        public override string ToString()
        {
            return base.ToString() + $"Salary: {Salary :F2}";
        }
    }
}
