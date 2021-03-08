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
            var romanLetters = RomanToArabic(romanNumber.ToUpper());
            return romanLetters;
        }

        #region RomanAlgorithm
        private class RomanElem
        {
            public int Arabic { get; set; }
            public string Roman { get; set; }
            public string Rest { get; set; }
        }

        /// <summary>
        /// Returns a list of Roman-Arabic main numbers
        /// </summary>
        /// <returns>List of Roman-Arabic main numbers</returns>
        private static IList<RomanElem> MainRomanList()
        {
            return new List<RomanElem> {
                new RomanElem { Roman = "I"    , Arabic = 1     , Rest = string.Empty },
                new RomanElem { Roman = "II"   , Arabic = 2     , Rest = string.Empty },
                new RomanElem { Roman = "III"  , Arabic = 3     , Rest = string.Empty },
                new RomanElem { Roman = "IV"   , Arabic = 4     , Rest = string.Empty },
                new RomanElem { Roman = "V"    , Arabic = 5     , Rest = string.Empty },
                new RomanElem { Roman = "VI"   , Arabic = 6     , Rest = string.Empty },
                new RomanElem { Roman = "VII"  , Arabic = 7     , Rest = string.Empty },
                new RomanElem { Roman = "VIII" , Arabic = 8     , Rest = string.Empty },
                new RomanElem { Roman = "IX"   , Arabic = 9     , Rest = string.Empty },
                new RomanElem { Roman = "X"    , Arabic = 10    , Rest = string.Empty },
                new RomanElem { Roman = "XX"   , Arabic = 20    , Rest = string.Empty },
                new RomanElem { Roman = "XXX"  , Arabic = 30    , Rest = string.Empty },
                new RomanElem { Roman = "XL"   , Arabic = 40    , Rest = string.Empty },
                new RomanElem { Roman = "L"    , Arabic = 50    , Rest = string.Empty },
                new RomanElem { Roman = "LX"   , Arabic = 60    , Rest = string.Empty },
                new RomanElem { Roman = "LXX"  , Arabic = 70    , Rest = string.Empty },
                new RomanElem { Roman = "LXXX" , Arabic = 80    , Rest = string.Empty },
                new RomanElem { Roman = "XC"   , Arabic = 90    , Rest = string.Empty },
                new RomanElem { Roman = "C"    , Arabic = 100   , Rest = string.Empty },
                new RomanElem { Roman = "CC"   , Arabic = 200   , Rest = string.Empty },
                new RomanElem { Roman = "CCC"  , Arabic = 300   , Rest = string.Empty },
                new RomanElem { Roman = "CD"   , Arabic = 400   , Rest = string.Empty },
                new RomanElem { Roman = "D"    , Arabic = 500   , Rest = string.Empty },
                new RomanElem { Roman = "DC"   , Arabic = 600   , Rest = string.Empty },
                new RomanElem { Roman = "DCC"  , Arabic = 700   , Rest = string.Empty },
                new RomanElem { Roman = "DCCC" , Arabic = 800   , Rest = string.Empty },
                new RomanElem { Roman = "CM"   , Arabic = 900   , Rest = string.Empty },
                new RomanElem { Roman = "M"    , Arabic = 1000  , Rest = string.Empty },
                new RomanElem { Roman = "MM"   , Arabic = 2000  , Rest = string.Empty },
                new RomanElem { Roman = "MMM"  , Arabic = 3000  , Rest = string.Empty }
            };
        }

        /// <summary>
        /// Returns a list of Roman-Arabic main numbers
        /// </summary>
        /// <returns>List of Roman-Arabic main numbers</returns>
        private static IList<RomanElem> ErrorRepeatList()
        {
            return new List<RomanElem> {
                new RomanElem { Roman = "IIII"  , Arabic = 0    , Rest = string.Empty },
                new RomanElem { Roman = "VV"    , Arabic = 0    , Rest = string.Empty },
                new RomanElem { Roman = "XXXX"  , Arabic = 0    , Rest = string.Empty },
                new RomanElem { Roman = "LL"    , Arabic = 0    , Rest = string.Empty },
                new RomanElem { Roman = "CCCC"  , Arabic = 0    , Rest = string.Empty },
                new RomanElem { Roman = "DD"    , Arabic = 0    , Rest = string.Empty },
                new RomanElem { Roman = "MMMM"  , Arabic = 0    , Rest = string.Empty }
            };
        }

        /// <summary>
        /// Matches the roman number to an item in the Main Roman List.
        /// For example: GetArabicNumber("DCCC") = { Roman = "DCCC" , Arabic = 800, Rest = string.Empty }
        /// </summary>
        /// <param name="romanNumber">A number in roman format (ej. "DCCC")</param>
        /// <returns>GetArabicNumber("DCCC") = { Roman = "DCCC", Arabic = 80, Rest = string.Empty }</returns>
        private static RomanElem GetArabicNumber(string romanNumber)
        {
            for (var index = MainRomanList().Count - 1; index >= 0; index--)
            {
                var romanElem = MainRomanList().ElementAt(index);
                if (romanElem.Roman == romanNumber)
                {
                    return romanElem;
                }
            }
            return null;
        }

        private static bool HasRepetions(string romanNumber)
        {
            var _errorRepeatList = ErrorRepeatList();
            foreach (var elem in _errorRepeatList)
                if (romanNumber.Contains(elem.Roman))
                    return true;

            return false;
        }

        /// <summary>
        /// Returns 10-base bound limit.
        /// For example. f(2560)=1000, f(378)=100, f(23)=10, f(98)=10, f(6)=1
        /// </summary>
        /// <param name="number">Any number</param>
        /// <returns></returns>
        private static int GetBoundLimit(int number)
        {
            int power = 1;
            while (power * 10 <= number) power *= 10;
            return power;
        }

        /// <summary>
        /// Matches the Roman Number to an item in the main Roman list.
        /// If it doesn't match, discard the last letter on the right and searchs the match.
        /// If the match is found, the method returns the match.
        /// So on, until no subtext(from the left) matches the main Roman list.So the method returns null.
        /// </summary>
        /// <param name="romanNumber">A number in roman format (ej. "DCCC")</param>
        /// <returns></returns>
        private static RomanElem GetLeftMatch(string romanNumber)
        {
            for (var index = romanNumber.Length; index > 0; index--)
            {
                var romanSubStr = romanNumber.Substring(0, index);

                var romanSubStrElem = GetArabicNumber(romanSubStr);
                if (romanSubStrElem != null)
                {
                    return new RomanElem {
                        Roman = romanSubStrElem.Roman,
                        Arabic = romanSubStrElem.Arabic,
                        Rest = romanNumber.Substring(index)
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the equivalent arabic number
        /// </summary>
        /// <param name="romanNumber">A number in roman format</param>
        /// <returns></returns>
        private static int RomanToArabic(string romanNumber)
        {
            // Validation 1: Has repetitions?
            if (HasRepetions(romanNumber)) return 0;

            var _numbers = new List<RomanElem>();
            var _rest = romanNumber; // It always be the remaining roman number (ej. 'MDXII')
            var _sum = 0;

            while (_rest != string.Empty)
            {
                // Get Match from left
                var _getLeftMatch = GetLeftMatch(_rest); // getLeftMatch('MDXII') = { roman: 'M', arabic: 1000, rest: 'DXII' }

                // Validation 2: post-conversion
                if (_getLeftMatch == null) return 0;
                if (_numbers.Count > 0 && _getLeftMatch.Arabic > GetBoundLimit(_numbers.Min(x => x.Arabic))) return 0;

                // Adds the match
                _numbers.Add(_getLeftMatch);
                _sum += _getLeftMatch.Arabic; // _sum = 0 + 1000
                _rest = _getLeftMatch.Rest; // _rest = 'DXII'
            }
            return _numbers.Sum(x => x.Arabic);
        }
        #endregion
    }
}
