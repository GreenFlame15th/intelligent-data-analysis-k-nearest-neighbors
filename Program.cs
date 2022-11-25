using lab.Characteristics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace lab
{
    class Program
    {
        public const string path  = @"..\..\Files";
        public const int minchar = 100;
        public const int nebourCount = 5;
        public const float knownPopulatsion = 0.75f;
        public const bool normalize = true;
        static void Main(string[] args)
        {
            //get charcterstics
            List<Characteristic> characteristics = new List<Characteristic>();
            characteristics.Add(new CharPerWords());
            characteristics.Add(new MedianWorldLenightDelta());
            characteristics.Add(new AvrageWorldLenightDelta());
            characteristics.Add(new ArtckleCount());
            characteristics.Add(new CapitalWorldCount());
            characteristics.Add(new WorldCount());
            characteristics.Add(new CharCount('.'));
            characteristics.Add(new WorldsOverNChar(10));
            characteristics.Add(new WorldsOverNChar(100));
            //get texts
            TestPool testPool = new TestPool(@"..\..\Files", 100, characteristics, true, 0.8f);
            //testing
            Console.WriteLine("Making guesses");
            places[] testingPlaces = { places.usa , places.uk};
            foreach (DistanceTypes distanceType in Enum.GetValues(typeof(DistanceTypes)))
            {
                Console.WriteLine(distanceType.ToString());
                testPool.MakeGuesses(testingPlaces, 5, distanceType);
            }
            //read to keep program running
            Console.WriteLine("Done: " + (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToHumanTimeString());
            Console.Read();
        }
    }
  
}
