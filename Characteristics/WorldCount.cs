using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Characteristics
{
    class WorldCount : Characteristic
    {
        public override float ProduceValue(TextParametryzised text)
        {
            return text.words.Count();
        }
    }
}
