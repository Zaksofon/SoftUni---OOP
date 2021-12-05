
namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int plantComfort = 1;
        private const decimal plantPrice = 5;

        public Ornament() 
            : base(plantComfort, plantPrice)
        {
        }
    }
}
