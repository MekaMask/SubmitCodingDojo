using System;

namespace Homework05.Lib
{
    public class HomeWorkLib05 : IHomework05
    {

        private const string ledStringPlattern = @"[*0] [*1] [*2] [*3] [*4] [*5] [*6] [*7] [*8] [*9]
 1   2   3   4   5   6   7   8   9   A";

        public bool[] Switches { get; set; }

        public HomeWorkLib05()
        {
            Switches = new bool[] { false, false, false, false, false, false, false, false, false, false };
        }

        private void SetLED(string ledNo)
        {
            var index = ledNo.ToUpper() == "A" ? 9 : int.Parse(ledNo) - 1;
            Switches[index] = !Switches[index];
        }

        private string GetLEDStringFromSwitches()
        {
            var ledString = ledStringPlattern;
            for (int i = 0; i < Switches.Length; i++)
            {
                var switchDisplay = Switches[i] ? "!" : " ";
                ledString = ledString.Replace($"*{i.ToString()}", switchDisplay);
            }
            return ledString;
        }

        public string DisplayLEDOnScreen(string ledNo)
        {
            SetLED(ledNo);
            return GetLEDStringFromSwitches();
        }
    }
}
