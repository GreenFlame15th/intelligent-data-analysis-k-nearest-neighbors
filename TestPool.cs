using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class TestPool
    {
        private List<TextParametryzised> allTexts;
        private List<TextParametryzised> known;
        private List<TextParametryzised> toTest;

        public TestPool(String path, int minchar, List<Characteristic> characteristics, bool normalize, float knownPopulatsion)
        {
            allTexts = TextMaker.MakeTexts(path, minchar);
            Console.WriteLine("Texts paramterazised");

            //vecotr genresion
            new VectorMaker(normalize).MakeVecotrsOnTexts(allTexts, characteristics);
            //pop split
            Console.WriteLine("Splitting populatsion");
            (List<TextParametryzised>, List<TextParametryzised>) popSplit = allTexts.RandomeSplitList<TextParametryzised>(knownPopulatsion);
            known = popSplit.Item1;
            toTest = popSplit.Item2;
            Console.WriteLine("Known: " + known.Count);
            Console.WriteLine("To test: " + toTest.Count);
            Console.WriteLine("Total: " + allTexts.Count);
        }

        public int getToTestCount() { return toTest.Count; }

        public Dictionary<places, Dictionary<bool, int>> MakeGuessesWithFreshSplit(places[] testingPlaces, int nebourCount, float knownPopulatsion)
        {
            Console.WriteLine("FreshSplit");
            (List<TextParametryzised>, List<TextParametryzised>) popSplit = allTexts.RandomeSplitList<TextParametryzised>(knownPopulatsion);
            List<TextParametryzised>  freshKnown = popSplit.Item1;
            List<TextParametryzised> freshtoTest = popSplit.Item2;
            Console.WriteLine("Known: " + freshKnown.Count);
            Console.WriteLine("To test: " + freshtoTest.Count);
            Console.WriteLine("Total: " + allTexts.Count);
            return MakeGuesses(testingPlaces, nebourCount, freshtoTest, freshKnown);
        }
        public Dictionary<places, Dictionary<bool, int>> MakeGuesses(places[] testingPlaces, int nebourCount)
        {
            return MakeGuesses(testingPlaces, nebourCount, toTest, known);
        }

        public Dictionary<places, Dictionary<bool, int>> MakeGuesses(places[] testingPlaces, int nebourCount, List<TextParametryzised> toTest, List<TextParametryzised> known)
        {
            Dictionary<places, Dictionary<bool, int>> guessQuiltyCount
                = PlaceGusser.MakeGuessesAndGetGuessQuiltyCount(toTest, known, testingPlaces, nebourCount);
            //assesing results
            Console.WriteLine("results:");
            foreach (places place in testingPlaces)
            {
                int sum = getToTestCount();
                Console.WriteLine(place.ToString() + ": " + guessQuiltyCount[place][true] + "/" + sum
                    + "[" + 100f * (float)guessQuiltyCount[place][true] / (float)sum + "%]");
            }
            return guessQuiltyCount;
        }
    }
}
