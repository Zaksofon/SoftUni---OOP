
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MilitaryElite.Interfaces;

namespace MilitaryElite.Classes
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private readonly List<IPrivate> privates;
        public LieutenantGeneral(int id, string firstName, string secondName, decimal salary) 
            : base(id, firstName, secondName, salary)
        {
            privates = new List<IPrivate>();
        }

        public IReadOnlyCollection<IPrivate> Privates => this.privates.AsReadOnly();

        public void AddPrivate(IPrivate @private)
        {
            privates.Add(@private);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                .AppendLine("Privates:");

            foreach (var @private in privates)
            {
                sb.AppendLine("  " + @private);
            }

            return sb.ToString().Trim();
        }
    }
}
