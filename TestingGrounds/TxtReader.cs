using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestingGrounds
{
    class TxtReader
    {
        private readonly string _filepath;
        public TxtReader(string filepath)
        {
            _filepath = filepath;
        }

        /// <summary>
        /// Read lines from a file, following the path that was passed into the constructor.
        /// </summary>
        /// <returns>A list of strings, of a max length of 300 unless specified otherwise.
        /// </returns>
        public List<string> ReadFirstNLinesFromFile(int limit = 300)
        {
            List<string> names = new List<string>() { };

            string csvLine;

            using (StreamReader reader = new StreamReader(_filepath)) {

                int count = 0;
                while ((csvLine = reader.ReadLine()) != null && count < limit)
                {
                    // note: reading is performed within while loop's boolean expression

                    if (!string.IsNullOrWhiteSpace(csvLine))
                    {
                        names.Add(csvLine);
                        count++;
                    }
                }
            }

            return names;
        }
    }
}
