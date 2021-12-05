
using System;
using System.Linq;

namespace Telephony
{
    public class StationaryPhone : ICallable
    {
        public string MakeACall(string number)
        {
            if (number.Any(char.IsLetter))
            {
                throw new InvalidOperationException("Invalid number!");
            }
            return $"Dialing... {number}";
        }
    }
}
