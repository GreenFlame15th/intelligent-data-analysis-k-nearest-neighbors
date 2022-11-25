using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    static class PlaceGusser
    {
        static public Dictionary<places, Dictionary<bool, int>> MakeGuessesAndGetGuessQuiltyCount
            (List<TextParametryzised> toTest, List<TextParametryzised> known, places[] testingPlaces, int nebourCount)
        {
            Dictionary<places, Dictionary<bool, int>> guessQuiltyCount = new Dictionary<places, Dictionary<bool, int>>();
            foreach (places place in testingPlaces)
            {
                guessQuiltyCount.Add(place, new Dictionary<bool, int>());
                guessQuiltyCount[place].Add(false, 0);
                guessQuiltyCount[place].Add(true, 0);
            }
            foreach (TextParametryzised testing in toTest)
            {
                List<places> gussedPlaces = PlaceGusser.MakeGuesses(testing, known, testingPlaces, nebourCount);
                foreach (places place in testingPlaces)
                {
                    guessQuiltyCount[place][gussedPlaces.Contains(place) == testing.places.Contains(place)]++;
                }


            }
            return guessQuiltyCount;
        }
        static public List<places> MakeGuesses(TextParametryzised point, List<TextParametryzised> known, places[] placeToGuess, int nebourCount)
        {
            List<places> toReturn = new List<places>();
            List<NebourEntry> PotensionalNeighbours = GetPotensionalNeighbours(point, known);
            List<NebourEntry> NeirestNergours = GetNeirestNergours(PotensionalNeighbours, nebourCount);
            foreach(places place in placeToGuess)
            {
                if (MakeGuess(place, NeirestNergours))
                    toReturn.Add(place);
            }
            return toReturn;
        }
        static public Boolean MakeGuess(places place, List<NebourEntry> nearestNeighbours)
        {
            int have = 0;
            //count haves
            foreach (NebourEntry nearestNeighbour in nearestNeighbours)
            {
                if (nearestNeighbour.text.places.Contains(place))
                    have++;
            }
            //compare haves to haves nots
            if (have * 2 > nearestNeighbours.Count)
                return true;
            else
                return false;
        }
        static public List<NebourEntry> GetPotensionalNeighbours(TextParametryzised point, List<TextParametryzised> known)
        {
            List<NebourEntry> potensionalNeighbours = new List<NebourEntry>();
            foreach (TextParametryzised neighbour in known)
            {
                //get distance
                float distnaceSquere = 0;
                for (int i = 0; i < neighbour.vector.Count; i++)
                {
                    float characteristicsdistnace = (neighbour.vector[i] - point.vector[i]);
                    distnaceSquere += characteristicsdistnace * characteristicsdistnace;
                }
                potensionalNeighbours.Add(new NebourEntry(neighbour, distnaceSquere));
            }
            return potensionalNeighbours;
        }
        static public List<NebourEntry> GetNeirestNergours(List<NebourEntry> potensionalNeighbours, int nebourCount)
        {
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
                while (i < nebourCount && !found)
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
            return nearestNeighbours;
        }
    }
}
