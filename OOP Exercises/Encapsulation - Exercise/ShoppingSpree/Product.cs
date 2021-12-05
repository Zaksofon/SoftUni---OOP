
namespace ShoppingSpree
{
    public class Product
    {
        private string name;
        private decimal cost;

        public string Name
        {
            get => name;
            private set
            {
                Validator.ThrowIfStringIsNullOrEmpty(value, "Name cannot be empty");
                name = value;
            }
        }
        public decimal Cost
        {
            get => cost;
            private set
            {
                Validator.ThrowIfDecimalIsLessThanZero(value, "Money cannot be negative");
                cost = value;
            }
        }

        public Product(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
        }
    }
}
