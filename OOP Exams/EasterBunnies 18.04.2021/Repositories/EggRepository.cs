
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> eggs;

        public EggRepository()
        {
            eggs = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models { get; private set; } = new List<IEgg>();

        public void Add(IEgg model)
        {
            eggs.Add(model);
            Models =  eggs;
        }

        public bool Remove(IEgg model)
        {
            if (eggs.Contains(model))
            {
                eggs.Remove(model);
                Models =  eggs;
                return true;
            }

            return false;
        }

        public IEgg FindByName(string name)
        {
            var currentEgg = eggs.FirstOrDefault(n => n.Name == name);
            if (currentEgg == null)
            {
                return null;   
            }

            return currentEgg;
        }
    }
}
