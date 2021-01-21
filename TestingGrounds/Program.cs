using System;
using System.Collections.Generic;
using System.IO;

namespace TestingGrounds
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO manually implement common data structures and (search) algorithms
            // in this solution

            // test "robot name generator"
            string hardcodedFilePath = @"C:\Users\keepe\Documents\Tests\listofnames.txt";
            List<string> names = new List<string>() { };

            if (File.Exists(hardcodedFilePath))
            {
                TxtReader reader = new TxtReader(hardcodedFilePath);

                names = reader.ReadFirstNLinesFromFile();
            }

            Console.WriteLine("\nRun the Robot Name Generator.\n");

            RobotNameGenerator generator = new RobotNameGenerator();
            string[] robotNames = generator.PickRobotSquadNames(names);

            foreach (string item in robotNames)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nRun the generator again.\n");

            string[] robotNames2 = generator.PickRobotSquadNames(names);

            foreach (string item in robotNames2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nRun the \"optimized\" generator method.\n");

            string[] robotNames3 = generator.
                PickRobotSquadNamesOptimized(names);

            foreach (string item in robotNames3)
            {
                Console.WriteLine(item);
            }
        }
    }
}
