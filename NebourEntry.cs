using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab 
{
    class NebourEntry
    {
        public TextParametryzised text;
        public float distnaceSquere;

        public NebourEntry(TextParametryzised text, float distnaceSquere)
        {
            this.text = text;
            this.distnaceSquere = distnaceSquere;
        }
    }

   
}
