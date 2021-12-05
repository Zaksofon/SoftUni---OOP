using System;

namespace Telephony
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] urls = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            StationaryPhone stationaryPhone = new StationaryPhone();
            Smartphone smartPhone = new Smartphone();

            for (int i = 0; i < numbers.Length; i++)
            {
                try
                {
                    string currentNumber = numbers[i];

                    switch (currentNumber.Length)
                    {
                        case 7:
                            stationaryPhone.MakeACall(currentNumber);
                            Console.WriteLine(stationaryPhone.MakeACall(currentNumber));
                            break;
                        case 10:
                            smartPhone.MakeACall(currentNumber);
                            Console.WriteLine(smartPhone.MakeACall(currentNumber));
                            break;
                        default:
                            throw new InvalidOperationException("Invalid number!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            for (int i = 0; i < urls.Length; i++)
            {
                try
                {
                    string currentUrl = urls[i];

                    smartPhone.BrowsePage(currentUrl);
                    Console.WriteLine(smartPhone.BrowsePage(currentUrl));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

