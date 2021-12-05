
using System;
using MilitaryElite.Interfaces;

namespace MilitaryElite.Classes
{
    public class Spy : Soldier, ISpy
    {
        public Spy(int id, string firstName, string secondName, int codeNumber) : base(id, firstName, secondName)
        {
            CodeNumber = codeNumber;
        }

        public int CodeNumber { get; }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + $"Code Number: {CodeNumber}";
        }
    }
}
