using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Characteristics
{
    class CapitalWorldCount : Characteristic
    {
        public override float ProduceValue(TextParametryzised text)
        {
            int count = 0;
            foreach (String world in text.words)
            {
                if (world.Length > 0 && Char.IsUpper(world[0]))
                    count++;
            }
            return count;

        }
    }
}
