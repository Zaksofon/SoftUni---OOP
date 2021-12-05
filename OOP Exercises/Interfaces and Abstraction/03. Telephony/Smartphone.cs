
using System;
using System.Linq;

namespace Telephony
{
    public class Smartphone : ICallable, IBrowseble
    {

        public string MakeACall(string number)
        {
            if (number.Any(char.IsLetter))
            {
                throw new InvalidOperationException("Invalid number!");
            }
            return $"Calling... {number}";
        }


        public string BrowsePage(string url)
        {
            if (url.Any(char.IsDigit))
            {
                throw new InvalidOperationException("Invalid URL!");
            }
            return $"Browsing: {url}!";
        }
    }
}
