using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Characteristics
{
    class CharPerWords : Characteristic
    {
        public override float ProduceValue(TextParametryzised text)
        {
            return text.text.body.Length / text.words.Count;
        }
    }
}
