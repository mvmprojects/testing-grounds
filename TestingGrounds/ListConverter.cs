using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingGrounds
{
    class ListConverter
    {
        /// <summary>
        /// Instead of getting a convenient dictionary with alphabetic chars as keys you can 
        /// now get a measly array and then figure out the rest yourself. Strings starting with
        /// the letter A go in a new list in array[0], B strings go in array[1] etc.
        /// Warning: the resulting lists in the array are not sorted.
        /// Warning: input entries that do not start with a capital English alphabet letter 
        /// are ignored.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>An array of size 26, filled with sublists of strings.</returns>
        public List<string>[] GetAlphabetizedArray(
            List<string> list)
        {
            //char[] alpha = SimpleConstants.Alphabet.ToCharArray();

            List<string>[] listArray = new List<string>[26];

            for (int i = 0; i < 26; i++)
            {
                listArray[i] = new List<string>() { };
            }

            foreach (string item in list)
            {
                foreach (string c in Enum.GetNames(typeof (AlphabetEnum)))
                {
                    if (item.ElementAt(0).ToString().Equals(c))
                    {
                        // convert enum name to number
                        int i = (int)Enum.Parse(typeof (AlphabetEnum), c);

                        // add to a list in the array
                        //dict[c].Add(item);
                        listArray[i].Add(item);
                    }
                }
            }

            return listArray;
        }

        /// <summary>
        /// Break a list into smaller lists to become the alphabetical categories
        /// for a dictionary, where the keys are the letters of the alphabet.
        /// This method was designed for use in a random name generator with the
        /// special quality of picking a different starting letter for every name.
        /// Warning: the resulting lists in the dictionary are not sorted.
        /// Warning: input entries that do not start with a capital English alphabet letter 
        /// are ignored.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>A dictionary with lists for values and chars for keys.</returns>
        public Dictionary<char, List<string>> GetAlphabetDict(
            List<string> list)
        {
            // "To order a sequence by the values of the elements themselves, 
            // specify the identity function (x => x) for keySelector"
            //var sortedList = list.OrderBy(x => x).ToList();

            // never mind sorting
            char[] alpha = SimpleConstants.Alphabet.ToCharArray();
            var dict = new Dictionary<char, List<string>>();

            foreach (char c in alpha)
            {
                List<string> l = new List<string>() { };
                dict.Add(c, l);
            }

            foreach (string item in list)
            {
                foreach (char c in alpha)
                {
                    if (item.ElementAt(0).Equals(c))
                    {
                        dict[c].Add(item);
                    }
                }
            }

            return dict;
        }
    }
}
