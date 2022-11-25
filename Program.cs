using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using lab.Characteristics;

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
            //get texts
            FileReader fileReader = new FileReader(path);
            Console.WriteLine("File Count: " + fileReader.GetFiles().Count);
            List<Text> textsWithStrings = fileReader.FilesToTextWithStrings(fileReader.GetFiles(), minchar);
            Console.WriteLine("Test Vount: " + textsWithStrings.Count);
            List<TextParametryzised> texts = new List<TextParametryzised>();
            foreach(Text textWithStrings in textsWithStrings)
            {
                texts.Add(new TextParametryzised(textWithStrings));
            }
            Console.WriteLine("Texts paramterazised");
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
            //masure charcterstics
            foreach (Characteristic characteristic in characteristics)
            {
                Console.WriteLine("Assesing: " + characteristic.ToString());
                foreach (TextParametryzised text in texts)
                {
                    text.vector.Add(characteristic.ProduceValue(text));
                }
            }
            //mormalizng
            if(normalize) Console.WriteLine("normalizing");
            if(normalize) for (int i = 0; i < characteristics.Count; i++)
            {
                float max = 0;
                foreach (TextParametryzised text in texts)
                {
                        if(text.vector[i] > max)
                        max = text.vector[i];
                }
                foreach (TextParametryzised text in texts)
                {
                        text.vector[i] /= max;
                }
            }
            //shuffling
            Console.WriteLine("Shuffling");
            texts.Shuffle();
            //testing

            //pop split
            Console.WriteLine("Splitting populatsion");
            int splitPoint = (int)Math.Floor(knownPopulatsion * texts.Count);
            List<TextParametryzised> known = texts.GetRange(0, splitPoint);
            List<TextParametryzised> toTest = texts.GetRange(splitPoint, texts.Count - splitPoint); new List<TextParametryzised>(texts);
            Console.WriteLine("Known: " + known.Count);
            Console.WriteLine("To test: " + toTest.Count);
            Console.WriteLine("Total: " + texts.Count + "[" + (known.Count + toTest.Count) + "]");

            //testing each
            Console.WriteLine("Making guesses");
            //result analasis tool
            Dictionary<places, int> correct = new Dictionary<places, int>();
            Dictionary<places, int> incorrect = new Dictionary<places, int>();
            foreach (places place in Enum.GetValues(typeof(places)))
            {
            correct.Add(place, 0);
            incorrect.Add(place, 0);
            }
            
            foreach (TextParametryzised testing in toTest)
            {
            List<NebourEntry> potensionalNeighbours = new List<NebourEntry>();
            foreach (TextParametryzised neighbour in known)
                {

                    //get distance
                    float distnaceSquere = 0;
                    for (int i = 0; i < neighbour.vector.Count; i++)
                    {
                        float characteristicsdistnace = (neighbour.vector[i] - testing.vector[i]);
                        distnaceSquere += characteristicsdistnace * characteristicsdistnace;
                    }
                    potensionalNeighbours.Add(new NebourEntry(neighbour, distnaceSquere));
                    }
                    //get instisal nearestNeighbours
                    List<NebourEntry> nearestNeighbours = new List<NebourEntry>();
                    for (int i = 0; i < nebourCount; i++)
                    {
                        nearestNeighbours.Add(potensionalNeighbours[0]);
                        potensionalNeighbours.RemoveAt(0);
                   }
                    nearestNeighbours = nearestNeighbours.OrderBy(o => o.distnaceSquere).ToList();
                    //compare instisal nearestNeighbours to potensial nearestNeighbours
                    foreach (NebourEntry entry in potensionalNeighbours)
                    {
                        int i = 0;
                        bool found = false;
                        while(i < nebourCount && !found)
                        {
                            if (entry.distnaceSquere < nearestNeighbours[i].distnaceSquere)
                            {
                                nearestNeighbours[i] = entry;
                                found = true;
                            }
                            else
                            i++;
                        }
                    }
                    //evaluate Neighbous by place
                    foreach (places place in Enum.GetValues(typeof(places)))
                    {
                        //count haves
                    int have = 0;
                    foreach (NebourEntry nearestNeighbour in nearestNeighbours)
                        if (nearestNeighbour.text.places.Contains(place))
                            have++;
                        //compare haves to haves nots
                        bool guess;
                        if (have * 2 > nebourCount)
                            guess = true;
                        else
                            guess = false;
                        //see if correct
                        if (testing.places.Contains(place) == guess)
                            correct[place]= correct[place]+1;
                        else
                            incorrect[place] = incorrect[place] + 1;

                    }
                }
                //assesing results
                Console.WriteLine("results:");
                int allGuesses = 0;
                int allCoreecetGuesses = 0;
                foreach (places place in Enum.GetValues(typeof(places)))
                {
                    int sum = correct[place] + incorrect[place];
                    Console.WriteLine(place.ToString() + ": " + correct[place]+"/"+sum
                        +"["+100f*(float)correct[place]/(float)sum + "%]");

                    allCoreecetGuesses += correct[place];
                    allGuesses += sum;
                }

                Console.WriteLine("overall: " + allCoreecetGuesses + "/" + allGuesses
                        + "[" + 100f * (float)allCoreecetGuesses / (float)allGuesses + "%]");

            //read to keep program running
            Console.WriteLine("Done: " + (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToHumanTimeString());
            Console.Read();
        }
    }
  
}
