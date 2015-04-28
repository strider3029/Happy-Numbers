using System;
using System.IO;
using System.Collections.Generic;

namespace HappyNumbers
{
    class HappyNumbers
    {
        static List<int> unhappyNumbers = new List<int>();

        static int Main(string[] args)
        {
            // Check the argument and file exists
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.Write("You failed to specify the file to read, or entered a non existant file path.\nPlease try again with the file name as the first argument.");
                return 0;
            }

            using (StreamReader reader = File.OpenText(args[0]))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (null == line) continue;

                    if ("" == line) continue;

                    int num = Convert.ToInt32(line);

                    PrintHappyOrNot(num);
                }
            }

            return 0;
        }

        /// <summary>
        /// A happy number is defined by the following process. Starting with any positive integer, replace the number by the sum of the squares of its digits,
        /// and repeat the process until the number equals 1 (where it will stay), or it loops endlessly in a cycle which does not include 1. 
        /// Those numbers for which this process ends in 1 are happy numbers, while those that do not end in 1 are unhappy numbers. 
        /// </summary>
        /// <param name="sumOfParts"></param>
        static void PrintHappyOrNot(int sumOfParts)
        {
            int num;
            List<int> processedNumbers = new List<int>();

            while (sumOfParts != 0 && sumOfParts != 1)
            {
                if(unhappyNumbers.Contains(sumOfParts) || processedNumbers.Contains(sumOfParts)) break;

                processedNumbers.Add(sumOfParts);

                // Get the sum of the parts of the current number
                string splitNum = sumOfParts.ToString();
                sumOfParts = 0;

                for (int index = 0; index < splitNum.Length; ++index)
                {
                    num = Convert.ToInt32(splitNum.Substring(index, 1));
                    sumOfParts += num * num;
                }
            }

            sumOfParts = (sumOfParts == 1) ? 1 : 0;

            // If it failed, add all the processed numbers to the list of numbers we know to be 'unhappy'
            if(sumOfParts == 0)
            {
                processedNumbers.ForEach( x =>
                    {
                        unhappyNumbers.Add(x);
                    });
            }

            Console.WriteLine(sumOfParts);
        }
    }
}
