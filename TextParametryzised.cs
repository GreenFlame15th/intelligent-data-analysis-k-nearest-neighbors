using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class TextParametryzised
    {
        public Text text;
        public List<places> places = new List<places>();
        public List<String> words;
        public List<float> vector = new List<float>();

        public TextParametryzised(Text orgin)
        {
            text = orgin;
            //places enumrated
            foreach (String str in orgin.places)
            {
                switch (str)
                {
                    case "west-germany":
                        places.Add(lab.places.west_germany);
                        break;
                    case "usa":
                        places.Add(lab.places.usa);
                        break;
                    case "france":
                        places.Add(lab.places.france);
                        break;
                    case "uk":
                        places.Add(lab.places.uk);
                        break;
                    case "canada":
                        places.Add(lab.places.canada);
                        break;
                    case "japan":
                        places.Add(lab.places.japan);
                        break;
                    default:
                        //Console.WriteLine("Found unsupported place: " +  str );
                        break;
                }
            }
            //word array
            words = new List<string>(orgin.body.Split(' '));
        }

        public String VectorAsString()
        {
            StringBuilder stringBuilder = new StringBuilder("[");
            foreach(float value in vector)
            {
                stringBuilder.Append(value.ToString("n3") + " ");
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}
