using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ShoppingSpree
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] people = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string[] products = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            Dictionary<string, Person> peopleList = new Dictionary<string, Person>();
            Dictionary<string, Product> productsList = new Dictionary<string, Product>();

            try
            {
                for (int i = 0; i < people.Length; i++)
                {

                    string[] peopleArgs =people[i]
                        .Split("=", StringSplitOptions.RemoveEmptyEntries);

                    string personName = peopleArgs[0];
                    decimal personMoney = decimal.Parse(peopleArgs[1]);

                    peopleList[personName] = new Person(personName, personMoney);
                }

                for (int i = 0; i < products.Length; i++)
                {
                    string[] productsArgs = products[i]
                        .Split("=", StringSplitOptions.RemoveEmptyEntries);

                    string productName = productsArgs[0];
                    decimal productCost = decimal.Parse(productsArgs[1]);

                    productsList[productName] = new Product(productName, productCost);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                string[] parts = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = parts[0];
                string product = parts[1];

                Person currentPerson = peopleList[name];
                Product currentProduct = productsList[product];

                try
                {
                    currentPerson.AddProduct(currentProduct);
                    Console.WriteLine($"{name} bought {product}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var person in peopleList)
            {
                Console.WriteLine(person.Value);
            }
        }
    }
}
