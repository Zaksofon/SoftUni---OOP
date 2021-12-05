
using System.Data;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        private const int freshWaterFishInitialSize = 3;

        public FreshwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            Size = freshWaterFishInitialSize;
        }

        public override void Eat()
        {
            Size += freshWaterFishInitialSize;
        }
    }
}
