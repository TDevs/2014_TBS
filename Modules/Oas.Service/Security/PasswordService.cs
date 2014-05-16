using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Service.Security
{
    public class PasswordService
    {
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGH­JKLMNOPQRSTUVWXYZ";
            string _allowedNumber = "0123456789";
            string _allowedOher = "!@$?";
            char[] chars = new char[PasswordLength];

            int allowedCharCount = _allowedChars.Length;
            int allowedNumberCount = _allowedNumber.Length;
            int allowedOtherCount = _allowedOher.Length;

            Random random = new Random();

            int i = 0;
            var isNumber = false;
            var isOther = false;
            while (i < PasswordLength)
            {
                //char
                var randomIndex = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + allowedCharCount)) % allowedCharCount;
                var charRandom = _allowedChars[randomIndex];

                while (chars.Any(en => en.Equals(charRandom)))
                {
                    randomIndex = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + allowedCharCount)) % allowedCharCount;
                    charRandom = _allowedChars[randomIndex];
                }
                chars[i] = charRandom;
                i++;

                //number
                if (i < PasswordLength && randomIndex < allowedNumberCount)
                {
                    charRandom = _allowedNumber[randomIndex];
                    chars[i] = charRandom;
                    i++;
                    isNumber = true;
                }

                //other
                if (i < PasswordLength && randomIndex < allowedOtherCount)
                {
                    charRandom = _allowedOher[randomIndex];
                    chars[i] = charRandom;
                    i++;
                    isOther = true;
                }
            }

            if (!isNumber)
            {
                var randomIndex = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + allowedNumberCount)) % allowedNumberCount;
                while (randomIndex >= allowedNumberCount)
                {
                    randomIndex = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + allowedNumberCount)) % allowedNumberCount;
                }

                var charRandom = _allowedNumber[randomIndex];
                chars[PasswordLength - 2] = charRandom;
            }

            if (!isOther)
            {
                var randomIndex = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + allowedOtherCount)) % allowedOtherCount;
                while (randomIndex >= allowedOtherCount)
                {
                    randomIndex = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + allowedOtherCount)) % allowedOtherCount;
                }

                var charRandom = _allowedOher[randomIndex];
                chars[PasswordLength - 1] = charRandom;
            }

            return new string(chars);
        }
    }
}
