using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Characteristics
{
    class MedianWorldLenightDelta : Characteristic
    {
        public override float ProduceValue(TextParametryzised text)
        {
            List<int> gap = new List<int>();
            List<String> words = text.words;
            for (int i = 0; i < words.Count-1; i++)
            {
                gap.Add(Math.Abs(words[i].Length - words[i + 1].Length));
            }
            gap.Sort();
            int count = gap.Count;
            if (count == 0)
                return 0;
            else if (count % 2 == 1)
                return gap[count / 2];
            else
                return ((float)gap[count / 2] + (float)gap[(count + 1) / 2]) / 2f;
        }
    }
}
