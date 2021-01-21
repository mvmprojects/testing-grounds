using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingGrounds
{
    class RobotNameGenerator
    {
        private string[] _chosenNames;
        private readonly char[] _alphabet;
        private readonly Random _r;

        public RobotNameGenerator()
        {
            _chosenNames = new string[6];
            _alphabet = SimpleConstants.Alphabet.ToCharArray();
            _r = new Random();
        }

        /// <summary>
        /// Same as PickRobotSquadNames(List) but should be slightly more efficient with 
        /// memory usage because it doesn't need a dictionary object for its alphabetic 
        /// splitting process.
        /// </summary>
        /// <param name="names"></param>
        /// <returns>An array containing 6 strings for a "robot squad".</returns>
        public string[] PickRobotSquadNamesOptimized(List<string> names)
        {
            // call a class that can take the given list and break it into categories
            ListConverter lc = new ListConverter();
            var listArray = lc.GetAlphabetizedArray(names);

            // fill chosenNames array with 6 letters from the alphabet
            AlphabetLettersForNames();

            // use letter chosen in previous step as a key in alphabetized array of size 26.
            // pick a random name from the sublist found in the array, to replace the letter.
            // if a sublist in the array is empty, we leave the capitalized letter as the name.
            // robot names can be short, after all.
            int i = 0;
            foreach (string item in _chosenNames)
            {
                //var list = alphabetDict[item.ElementAt(0)];
                int k = (int)Enum.Parse(typeof(AlphabetEnum), item.ElementAt(0).ToString());
                var list = listArray[k];

                if (list.Count() > 0)
                {
                    _chosenNames[i] = list[_r.Next(list.Count())];
                }
                i++;
            }

            ApplyPrefix();
            ApplyPostfix();

            return _chosenNames;
        }

        /// <summary>
        /// Pick names for a squad of six robots. Uses names from the list provided.
        /// Base names are non-repeating and start with different letters.
        /// </summary>
        /// <param name="names"></param>
        /// <returns>An array containing 6 strings.</returns>
        public string[] PickRobotSquadNames(List<string> names)
        {
            // call a class that can take the given list and break it into categories.
            ListConverter lc = new ListConverter();
            Dictionary<char, List<string>> alphabetDict =
                lc.GetAlphabetDict(names);

            // fill chosenNames array with 6 letters from the alphabet
            AlphabetLettersForNames();

            // use letter chosen in previous step as a key in the alphabetic dictionary.
            // pick a random name from the sublist found in the dict, to replace the letter.
            // if a sublist in the dict is empty, we leave the capitalized letter as the name.
            // robot names can be short, after all.
            int i = 0;
            foreach (string item in _chosenNames)
            {
                var list = alphabetDict[item.ElementAt(0)];
                if (list.Count() > 0) 
                { 
                    _chosenNames[i] = list[_r.Next(list.Count())];
                }
                i++;
            }

            ApplyPrefix();
            ApplyPostfix();

            return _chosenNames;
        }

        /// <summary>
        /// Pick names for a squad of six robots. Names will be simple.
        /// Base names are non-repeating, choosing a different letter 
        /// from the alphabet each time.
        /// </summary>
        /// <returns>An array containing 6 strings.</returns>
        public string[] PickRobotSquadNames()
        {
            // if you didn't get a list then don't bother with ListConverter

            AlphabetLettersForNames();
            ApplyPrefix();
            ApplyPostfix();
            return _chosenNames;
        }

        private void AlphabetLettersForNames()
        {
            // even if the _alphabet field is private readonly, 
            // the object (char array) it refers to is NOT immutable.
            // to avoid altering the field, you should clone into a local var.
            char[] alpha = (char[])_alphabet.Clone();

            // if you forget the clone, it could cause horrific problems when 
            // you call this method multiple times from the same 
            // RobotNameGenerator instance.

            // pick six non-repeating letters from the alphabet.
            // store them in chosenNames.
            int count = 0;
            int random;
            while (count < 6)
            {
                // pick a random number 0-25, representing a letter from the alphabet.
                // see if this index is still available in alphabet[]
                random = _r.Next(26);
                if (!alpha[random].Equals('0'))
                {
                    // fill a slot with that letter, and increment counter c
                    // remove the same letter from alphabet array by marking it as '0'
                    _chosenNames[count] = alpha[random].ToString();
                    count++;
                    alpha[random] = '0';
                }
            }
        }

        private void ApplyPrefix()
        {
            int index = 0;
            int random;
            string[] prefixes = ("Little,Tiny,Brave,Valiant,Grey,Silver," +
                "Lucky,Happy,Shiny,Flashy").Split(",");
            string chosenPrefix;

            foreach (string item in _chosenNames)
            {
                // 40% chance to actually apply the prefix.
                // ignore prefix step if the name is only one character long.
                if (item.Length > 1 && _r.Next(1, 101) <= 40)
                {
                    random = _r.Next(10);

                    // force unique prefix within squad
                    while (prefixes[random].Equals("0"))
                    {
                        random = _r.Next(10);
                    }
                    
                    chosenPrefix = prefixes[random];

                    _chosenNames[index] = chosenPrefix + " " + _chosenNames[index];

                    // mark prefix as being in use
                    prefixes[random] = "0";
                }

                index++;
            }
        }

        private void ApplyPostfix()
        {
            int i = 0;
            string[] postfixes = "01,02,03,04,05,06,07,08,09,11,13,17,24,27,00".Split(",");
            string chosenPostfix;

            foreach (string item in _chosenNames)
            {
                chosenPostfix = postfixes[_r.Next(9)];

                // ignore next step if the name is only one character long
                if (item.Length > 1)
                {
                    // 40% chance to add a letter to the postfix
                    if (_r.Next(1, 101) <= 40)
                    {
                        char temp = _alphabet[_r.Next(26)];

                        // aesthetic decision
                        if (temp.Equals('O')) temp = '0';

                        chosenPostfix = temp + chosenPostfix;
                    }

                    // add a space in front of the postfix
                    chosenPostfix = " " + chosenPostfix;
                }

                // apply postfix
                _chosenNames[i] = _chosenNames[i] + chosenPostfix;
                i++;
            }
        }
    }
}
