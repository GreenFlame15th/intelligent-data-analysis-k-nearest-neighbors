using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    static class TextMaker
    {
        public static List<TextParametryzised> MakeTexts(String path, int minchar)
        {
            FileReader fileReader = new FileReader(path);
            Console.WriteLine("File Count: " + fileReader.GetFiles().Count);
            List<Text> textsWithStrings = fileReader.FilesToTextWithStrings(fileReader.GetFiles(), minchar);
            Console.WriteLine("Test Vount: " + textsWithStrings.Count);
            List<TextParametryzised> texts = new List<TextParametryzised>();
            foreach (Text textWithStrings in textsWithStrings)
            {
                texts.Add(new TextParametryzised(textWithStrings));
            }
            return texts;
        }
    }
}
