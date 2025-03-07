using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace REG_MARK_LIB
{
    public class RegMarkDllLib
    {

        private static readonly string correctLetters = "ABEKMHOPCTYX";

        private static readonly Regex regularString = new Regex(@"^[ABEKMHOPCTYX]\d{3}[ABEKMHOPCTYX]{2}\d{2,3}$");

        public static bool CheckMark(string mark)
        {
            try
            {
                if (!regularString.IsMatch(mark)) return false;

                int region = int.Parse(mark.Substring(6));
                if (region >= 1 && region <= 199) return true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetNextMarkAfter(string mark)
        {
            try
            {
                if (!CheckMark(mark)) throw new ArgumentException("Invalid format");

                char letter1 = mark[0];
                int number = int.Parse(mark.Substring(1, 3));
                char letter2 = mark[4];
                char letter3 = mark[5];
                int region = int.Parse(mark.Substring(6));

                number++;

                if (number > 199)
                {
                    number = 1;

                    int indexLetter3 = correctLetters.IndexOf(letter3);
                    if (indexLetter3 < correctLetters.Length - 1)
                    {
                        letter3 = correctLetters[indexLetter3 + 1];
                    }
                    else
                    {
                        letter3 = correctLetters[0];

                        int indexLetter2 = correctLetters.IndexOf(letter2);
                        if (indexLetter2 < correctLetters.Length - 1)
                        {
                            letter2 = correctLetters[indexLetter2 + 1];
                        }
                        else
                        {
                            letter2 = correctLetters[0];

                            int indexLetter1 = correctLetters.IndexOf(letter1);
                            if (indexLetter1 < correctLetters.Length - 1)
                            {
                                letter1 = correctLetters[indexLetter1 + 1];
                            }
                            else
                            {
                                letter1 = correctLetters[0];
                                region++;
                                if (region > 199) region = 1;
                            }
                        }
                    }
                }

                return $"{letter1}{number:D3}{letter2}{letter3}{region}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GetNextMarkAfterInRange(string prevMark, string rangeStart, string rangeEnd)
        {
            try
            {
                if (!CheckMark(prevMark) || !CheckMark(rangeStart) || !CheckMark(rangeEnd))
                {
                    throw new ArgumentException("Invalid format");
                }
                string nextMark = GetNextMarkAfter(prevMark);
                if (string.Compare(nextMark, rangeStart) >= 0 && string.Compare(nextMark, rangeEnd) <= 0)
                {
                    return nextMark;
                }
                return "out of stock";
            }
            catch (Exception ex)
            {
                return ex.Message;
            } 
        }

        public static int GetCombinationsCountInRange(string mark1, string mark2)
        {
            try
            {
                if (!CheckMark(mark1) || !CheckMark(mark2))
                {
                    throw new ArgumentException("Invalid format");
                }
                int count = 0;
                string currentMark = mark1;

                while (string.Compare(currentMark, mark2) <= 0)
                {
                    count++;
                    currentMark = GetNextMarkAfter(currentMark);
                    if (currentMark == mark1) break;
                }

                return count;
            }
            catch
            {
                return -1;
            }
        }
    }
}
