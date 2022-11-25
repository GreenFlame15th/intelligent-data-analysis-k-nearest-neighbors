using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class VectorMaker
    {
        private bool normalize;

        public VectorMaker(bool normalize)
        { this.normalize = normalize; }

        public void MakeVecotrsOnTexts(List<TextParametryzised> texts, List<Characteristic> characteristics)
        {
            MakeRawVecotrsOnTexts(texts, characteristics);
            if (normalize)
                Normalize(texts);
        }
        private static void MakeRawVecotrsOnTexts(List<TextParametryzised> texts, List<Characteristic> characteristics)
        {
            Console.WriteLine("Making vectors");
            foreach (Characteristic characteristic in characteristics)
            {
                Console.WriteLine("Assesing: " + characteristic.ToString());
                foreach (TextParametryzised text in texts)
                {
                    text.vector.Add(characteristic.ProduceValue(text));
                }
            }
        }

        private static void Normalize(List<TextParametryzised> texts)
        {
            Console.WriteLine("normalizing");
            if(texts.Count >= 1)
            for (int i = 0; i < texts[0].vector.Count; i++)
            {
                float max = 0;
                foreach (TextParametryzised text in texts)
                {
                    if (text.vector[i] > max)
                        max = text.vector[i];
                }
                foreach (TextParametryzised text in texts)
                {
                    text.vector[i] /= max;
                }
            }
        }
    }
}
