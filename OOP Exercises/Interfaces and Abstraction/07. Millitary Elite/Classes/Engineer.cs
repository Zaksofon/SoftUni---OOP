
using System;
using System.Collections.Generic;
using System.Text;
using MilitaryElite.Interfaces;

namespace MilitaryElite.Classes
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly List<IRepair> repairs;
        public Engineer(int id, string firstName, string secondName, decimal salary, Corps corps) : base(id, firstName, secondName, salary, corps)
        {
            repairs = new List<IRepair>();
        }

        public IReadOnlyCollection<IRepair> Repears { get; }

        public void AddRepairs(IRepair repair)
        {
            repairs.Add(repair);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString() + Environment.NewLine + $"Repairs:");

            foreach (var repair in repairs)
            {
                sb.AppendLine($"  {repair}");
            }

            return sb.ToString().Trim();
        }
    }
}
