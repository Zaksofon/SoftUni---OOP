
using System.Linq;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (egg.IsDone() == false)
            {
                if (bunny.Energy == 0)
                {
                    break;
                }

                if (bunny.Dyes.All(c => c.IsFinished()))
                {
                    break;
                }
                bunny.Work();
                egg.GetColored();
            }
        }
    }
}
