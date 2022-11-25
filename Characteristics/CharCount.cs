using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Characteristics
{
    class CharCount : Characteristic
    {
        private char key;
        public override float ProduceValue(TextParametryzised text)
        {
            int length = text.text.body.Length;
            int count = 0;

            //Counts each character except space  
            for (int i = 0; i < length; i++)
            {
                if (text.text.body[i] == key)
                    count++;
            }

            return count;
        }

        public CharCount(char key) { this.key = key; }

        public override string ToString()
        {
            return base.ToString() + " (" + key + ") ";
        }
    }
}
