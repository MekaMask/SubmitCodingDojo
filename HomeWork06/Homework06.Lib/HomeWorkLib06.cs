using System;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Homework06.Lib
{
    public class HomeWorkLib06 : IHomework06
    {
        private const string ledStringPlattern = @"[*0] [*1] [*2] [*3] [*4] [*5] [*6] [*7] [*8] [*9]
 1   2   3   4   5   6   7   8   9   A";
        private const string saveledStringPlattern = @"[*0]<*0>[*1]<*1>[*2]<*2>[*3]<*3>[*4]<*4>[*5]<*5>[*6]<*6>[*7]<*7>[*8]<*8>[*9]<*9>
 1 <*0> 2 <*1> 3 <*2> 4 <*3> 5 <*4> 6 <*5> 7 <*6> 8 <*7> 9 <*8> A";
        public const string spacingStr = "";

        public bool[] Switches { get; set; }
        public string GetYaml { get; set; }
        public string FileYamlPath { get; set; }
        public string FilePath { get; set; }
        public string SaveStateLED { get; set; }

        public List<string> StatusLED { get; set; }

        public HomeWorkLib06()
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

        public string ReadYaml()
        {
            FileYamlPath = @"config.yaml";
            var read = new StreamReader(FileYamlPath);
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(read);
            var buildToJson = new SerializerBuilder()
                .JsonCompatible()
                .Build();

            var json = buildToJson.Serialize(yamlObject);
            var reviseJson = json.Replace("-", "");
            var jsonToObj = JsonConvert.DeserializeObject<ConfigLED>(reviseJson);
            var serializer = new YamlDotNet.Serialization.Serializer();

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, jsonToObj);
                var yaml = writer.ToString();
                GetYaml = yaml;
            }
            return GetYaml;
        }

        public string DisplayLEDOnScreen(string ledNo)
        {
            SetLED(ledNo);
            return GetLEDStringFromSwitches();
        }

        public string LoadState()
        {
            string resultFormRead;
            FilePath = @"State.txt";
            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    resultFormRead = reader.ReadToEnd();
                }
            }
            else
            {
                resultFormRead = "NoFile";
            }
            return resultFormRead;
        }

        public void SaveCurrentState()
        {
            FilePath = @"State.txt";
            SaveStateLED = String.Join(" ", StatusLED);
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                writer.Write(SaveStateLED);
            }
        }

        //TODO :
        public void SetAppConfigurations(string onSymbol, string offSymbol, int spacing)
        {
            var ledString = saveledStringPlattern;
            int j = 0;
            while (j < spacing)
            {
                spacingStr += " ";
                j++;
            }
            for (int i = 0; i < Switches.Length; i++)
            {
                var switchDisplay = Switches[i] ? onSymbol : offSymbol;
                ledString = ledString.Replace($"*{i.ToString()}", switchDisplay);
                ledString = ledString.Replace($"<{i.ToString()}>", " ");
            }
        }
    }
}
