using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public abstract class Characteristic
    {
        public abstract float ProduceValue(TextParametryzised text);

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
