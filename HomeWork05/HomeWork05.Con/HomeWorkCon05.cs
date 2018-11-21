using System;
using Homework05.Lib;

namespace Homework05.Con
{
    class HomeWorkCon05
    {
        static void Main(string[] args)
        {
            var svc = new HomeWorkLib05();

            System.Console.WriteLine(@"[ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ]
 1   2   3   4   5   6   7   8   9   A");

            while (true)
            {
                System.Console.Write("Please choose LED to turn On/Off: ");

                var ledSwiche = Console.ReadLine();

                var isCorrectNumber = int.TryParse(ledSwiche, out var ledNoText) && ledNoText >= 1 && ledNoText <= 9;
                var isA = ledSwiche.ToUpper() == "A";

                if (isCorrectNumber || isA)
                {
                    Console.WriteLine(svc.DisplayLEDOnScreen(ledSwiche));
                }
                else
                {
                    Console.WriteLine("Please enter 1-9 or a/A ");
                }
            }
        }
    }
}
