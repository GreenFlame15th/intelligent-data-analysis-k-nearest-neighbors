using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Characteristics
{
    class WorldsOverNChar : Characteristic
    {
        int n;

        public WorldsOverNChar(int n)
        {
            this.n = n;
        }

        public override float ProduceValue(TextParametryzised text)
        {
            int count = 0;
            foreach  (String word in text.words)
            {
                if (word.Length > n)
                    count++;
            }
            return count;
        }

        public override string ToString()
        {
            return base.ToString() + " ("+n+") ";
        }
    }
}
