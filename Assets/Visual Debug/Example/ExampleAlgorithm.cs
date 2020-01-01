#define DEBUG_EXAMPLE_ALGORITHM
using UnityEngine;

namespace VisualDebugging.Example
{
    public static class ExampleAlgorithm
    {
        // Returns an array containing the points nearest to one another out of the given set
        public static Vector3[] FindClosestPairOfPoints(Vector3[] points)
        {
#if DEBUG_EXAMPLE_ALGORITHM
            VisualDebug.Initialize();
            VisualDebug.BeginFrame("All points", true);
            VisualDebug.DrawPoints(points, .1f);
#endif
            Vector3[] closestPointPair = new Vector3[2];
            float bestDst = float.MaxValue;

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    float dst = Vector3.Distance(points[i], points[j]);
                    if (dst < bestDst)
                    {
                        bestDst = dst;
                        closestPointPair[0] = points[i];
                        closestPointPair[1] = points[j];
                    }
#if DEBUG_EXAMPLE_ALGORITHM
                    VisualDebug.BeginFrame("Compare dst", true);
                    VisualDebug.SetColour(Colours.lightRed, Colours.veryDarkGrey);
                    VisualDebug.DrawPoint(points[i], .1f);
                    VisualDebug.DrawArrow(points[i], points[j], .1f);
                    VisualDebug.DontShowNextElementWhenFrameIsInBackground();
                    VisualDebug.SetColour(Colours.lightGreen);
                    VisualDebug.DrawLineSegmentWithLabel(closestPointPair[0], closestPointPair[1], bestDst.ToString());
#endif
                }
            }

#if DEBUG_EXAMPLE_ALGORITHM
            VisualDebug.BeginFrame("Finished");
            VisualDebug.SetColour(Colours.lightGreen);
            VisualDebug.DrawPoints(closestPointPair, .15f);
            VisualDebug.DrawLineSegmentWithLabel(closestPointPair[0], closestPointPair[1], bestDst.ToString());
            VisualDebug.Save();
#endif
            return closestPointPair;
        }
    }
}