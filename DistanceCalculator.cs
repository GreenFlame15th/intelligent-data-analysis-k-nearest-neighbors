using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math.Distances;

namespace lab
{
    static class DistanceCalculator
    {
        public static float getDistance(DistanceTypes distanceType, List<float> vectorA, List<float> vecotorB)
        {
            switch (distanceType)
            {
                case DistanceTypes.euclideanSquere:
                    return getEuclideanSquere(vectorA, vecotorB);
                case DistanceTypes.manhattan:
                    return getManhattan(vectorA, vecotorB);
                case DistanceTypes.cosine:
                    return getCosine(vectorA, vecotorB);
                default:
                    return 0;
            }
        }

        public static float getEuclideanSquere(List<float> vectorA, List<float> vecotorB)
        {
            float distnaceSquere = 0;
            for (int i = 0; i < vectorA.Count; i++)
            {
                float characteristicsdistnace = (vectorA[i] - vecotorB[i]);
                distnaceSquere += characteristicsdistnace * characteristicsdistnace;
            }
            return distnaceSquere;
        }

        public static float getManhattan(List<float> vectorA, List<float> vecotorB)
        {
            float distnace = 0;
            for (int i = 0; i < vectorA.Count; i++)
            {
                float diffrance = (vectorA[i] - vecotorB[i]);
                if (diffrance > 0)
                    distnace += diffrance;
                else
                    distnace -= diffrance;
            }
            return distnace;
        }

        public static float getCosine(List<float> vectorA, List<float> vecotorB)
        {
            List<double> doubleA = new List<double>();
            List<double> doubleB = new List<double>();
            vectorA.ForEach(f => doubleA.Add(f));
            vecotorB.ForEach(f => doubleB.Add(f));
            Cosine cos = new Cosine();
            double distance = cos.Distance(doubleA.ToArray(), doubleB.ToArray());
            return (float)distance;
        }
    }
}
