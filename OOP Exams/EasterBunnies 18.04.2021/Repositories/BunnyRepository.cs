using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
   public class BunnyRepository : IRepository<IBunny>
   {
       private List<IBunny> bunnies;

       public BunnyRepository()
       {
           bunnies = new List<IBunny>();
       }

       public IReadOnlyCollection<IBunny> Models { get; private set; } = new List<IBunny>();

        public void Add(IBunny model)
        {
            bunnies.Add(model);
            Models =  bunnies;
        }

        public bool Remove(IBunny model)
        {
            if (bunnies.Contains(model))
            {
                bunnies.Remove(model);
                Models =  bunnies;
                return true;
            }

            return false;
        }

        public IBunny FindByName(string name)
        {
            var currentBunny = bunnies.FirstOrDefault(n => n.Name == name);

            if (currentBunny == null)
            {
                return null;
            }

            return currentBunny;
        }
    }
}
