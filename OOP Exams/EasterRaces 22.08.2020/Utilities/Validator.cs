
using System;

namespace EasterRaces.Utilities
{
    public class Validator
    {
        public static void ThrowIfStringIsNullOrWhiteSpaceOrLessThan(string str, int minLength, string message)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < minLength)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
