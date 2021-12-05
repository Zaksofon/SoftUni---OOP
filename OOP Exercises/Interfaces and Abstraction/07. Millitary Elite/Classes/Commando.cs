
using System;
using System.Collections.Generic;
using System.Text;
using MilitaryElite.Enumerators;
using MilitaryElite.Interfaces;

namespace MilitaryElite.Classes
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        private readonly List<IMission> missions;
        public Commando(int id, string firstName, string secondName, decimal salary, Corps corps) : base(id, firstName, secondName, salary, corps)
        {
            missions = new List<IMission>();
        }

        public IReadOnlyCollection<IMission> Missions => missions.AsReadOnly();

        public void AddMission(IMission mission)
        {
           missions.Add(mission);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString() + Environment.NewLine + "Missions:");

            foreach (var mission in missions)
            {
                sb.AppendLine($"  {mission}");
            }

            return sb.ToString().Trim();
        }
    }
}
