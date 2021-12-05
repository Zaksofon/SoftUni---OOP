
namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        private const int saltwaterFishInitialSize = 5;

        public SaltwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            Size = saltwaterFishInitialSize;
        }

        public override void Eat()
        {
            Size += saltwaterFishInitialSize;
        }
    }
}
