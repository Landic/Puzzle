using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzle
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var fragments = File.ReadAllLines("source.txt").Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

            if (!fragments.Any())
            {
                Console.WriteLine("Файл порожній або не містить жодного числа.");
                return;
            }
            string longestSequence = BuildLongestSequence(fragments);

            Console.WriteLine("Найдовша послідовність:");
            Console.WriteLine(longestSequence);
            Console.WriteLine("Довжина послідовності в символах:");
            Console.WriteLine(longestSequence.Length);
        }

        static string BuildLongestSequence(List<string> fragments)
        {
            var result = new List<string> { fragments[0] };
            fragments.RemoveAt(0);

            while (fragments.Count > 0)
            {
                bool found = false;

                for (int i = 0; i < fragments.Count; i++)
                {
                    string current = result.Last();
                    string fragment = fragments[i];
                    if (current[^2..] == fragment[..2])
                    {
                        result.Add(fragment);
                        fragments.RemoveAt(i);
                        found = true;
                        break;
                    }
                    else if (current[..2] == fragment[^2..])
                    {
                        result.Insert(0, fragment);
                        fragments.RemoveAt(i);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    break;
                }
            }
            return string.Join("", result);
        }
    }
}
