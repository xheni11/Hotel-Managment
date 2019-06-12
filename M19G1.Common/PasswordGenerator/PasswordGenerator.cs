using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Common.RandomPassword
{
    public class PasswordGenerator
    {
        Random _random;
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
             _random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public char RandomSpecialChar()
        {
             _random = new Random();
            string specialStringChar = "!@#$%^&*()_+=<>?.,;{}[]";
            char[] charArray=specialStringChar.ToCharArray();
            int chIndex = _random.Next(charArray.Length);
            return charArray[chIndex] ;
        }

        public int RandomNumber(int min, int max)
        {
             _random = new Random();
            return _random.Next(min, max);
        }

        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, true));
            builder.Append(RandomNumber(100, 999));
            builder.Append(RandomString(3, false));
            builder.Append(RandomSpecialChar());
            return builder.ToString();
        }
    }
}
