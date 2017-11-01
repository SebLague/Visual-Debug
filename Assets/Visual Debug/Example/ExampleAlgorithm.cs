using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAlgorithm {

    // Returns an array containing the points nearest to one another out of the given set
    Vector2[] FindClosestPairOfPoints(Vector2[] points)
    {
        Vector2[] closestPointPair = new Vector2[2];
        float bestDst = float.MaxValue;

        for (int i = 0; i < points.Length; i++)
        {
            for (int j = i; j < points.Length; j++)
            {
                float dst = Vector2.Distance(points[i], points[j]);
                if (dst < bestDst)
                {
                    bestDst = dst;
                    closestPointPair[0] = points[i];
                    closestPointPair[1] = points[j];
                }
            }
        }

        return closestPointPair;
    }
}
