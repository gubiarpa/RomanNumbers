using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanHelper.Extension
{
    public static class NumberConverter
    {
        public static int ConvertToInteger(this string romanNumber)
        {
            var romanLetters = romanNumber.ToUpper().ToCharArray().ToList();
            return 15;
        }
    }
}
