using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Characteristics
{
    class ArtckleCount : Characteristic
    {
        public override float ProduceValue(TextParametryzised text)
        {
            int count = 0;
            foreach (String world in text.words)
            {
                String worldLowerCase = world.ToLower();
                if (worldLowerCase == "the" || worldLowerCase == "a" || worldLowerCase == "an")
                    count++;
            }
            return count;
        }
    }
}
