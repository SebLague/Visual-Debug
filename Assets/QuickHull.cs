#define VISUAL_DEBUG_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VisualDebugging;

public class QuickHull : IHull
{


    public List<Vector2> pointsOnHull { get; set; }
    public List<Vector2> pointsNotOnHull { get; set; }

    // Debug info
    VisualDebugging.VisualDebugger debugger;
    const float debugHullPointRadius = 2.5f;
    const float debugRegularPointRadius = 1.3f;
    const float debugHighlightedPointRadius = 1.75f;

    Vector2[] allPoints;


    public QuickHull(Vector2[] points)
    {
        Recalculate(points);
    }

    public void Recalculate(Vector2[] points)
    {
        
#if VISUAL_DEBUG_MODE
    	debugger = Object.FindObjectOfType<VisualDebugger>();
        debugger.Initialize();
#endif

        GetHullPoints(points);

#if VISUAL_DEBUG_MODE
        debugger.BeginFrame("Finished");
        debugger.SetColour(Colours.lightRed);
        debugger.DrawLine(pointsOnHull, true);
#endif
	}


	void GetHullPoints(Vector2[] points)
	{
		this.allPoints = points;
		pointsOnHull = new List<Vector2>();
		pointsNotOnHull = new List<Vector2>();

#if VISUAL_DEBUG_MODE
		debugger.BeginFrame("Set of points from which to calculate hull", true);
        debugger.SetColour(Colours.lightGrey);
        debugger.DrawPoints(points, debugRegularPointRadius);
#endif

        int leftmostIndex = 0;
		int rightmostIndex = 0;
		float leftmostXVal = float.MaxValue;
		float rightmostXVal = float.MinValue;
		Vector2 leftMostVec = Vector2.zero;

		for (int i = 0; i < points.Length; i++)
		{
			if (points[i].x < leftmostXVal)
			{
				leftmostXVal = points[i].x;
				leftmostIndex = i;
				leftMostVec = points[i];
			}

			if (points[i].x > rightmostXVal)
			{
				rightmostXVal = points[i].x;
				rightmostIndex = i;
			}
		}

		pointsOnHull.Add(allPoints[leftmostIndex]);
		pointsOnHull.Add(allPoints[rightmostIndex]);

#if VISUAL_DEBUG_MODE
        debugger.BeginFrame("Add left and rightmost points to hull", true);
        debugger.SetColour(Color.black);
        debugger.DrawPoints(debugHullPointRadius, false, allPoints[leftmostIndex], allPoints[rightmostIndex]);
#endif

        Vector2 dir = (points[leftmostIndex] - points[rightmostIndex]).normalized;
		List<int> sideOne = new List<int>();
		List<int> sideTwo = new List<int>();

		for (int i = 0; i < points.Length; i++)
		{
			if (i != rightmostIndex && i != leftmostIndex)
			{
				if (Geometry.SideOfLine(allPoints[rightmostIndex], allPoints[leftmostIndex], allPoints[i]) >= 0)
				{
					sideOne.Add(i);
				}
				else
				{
					sideTwo.Add(i);
				}
			}
		}

#if VISUAL_DEBUG_MODE
		debugger.BeginFrame("Find points on either side of line formed by left and rightmost points", false);

		debugger.SetColour(Colours.lightRed);
		debugger.DrawPoints(.5f, false, allPoints[leftmostIndex], allPoints[rightmostIndex]);
        debugger.DrawLineSegment(allPoints[leftmostIndex], allPoints[rightmostIndex]);

        debugger.SetColour(Colours.lightBlue);
        debugger.DrawPoints(sideOne.Select(x=>allPoints[x]), debugHighlightedPointRadius);
        debugger.SetColour(Colours.lightGreen);
        debugger.DrawPoints(sideTwo.Select(x => allPoints[x]), debugHighlightedPointRadius);
#endif

		if (sideOne.Count > 0)
        {
            FindHull(sideOne, leftmostIndex, rightmostIndex);
        }
        if (sideTwo.Count > 0)
        {
            FindHull(sideTwo, rightmostIndex, leftmostIndex);
        }

		pointsOnHull.Sort((x, y) => Sort(x, y, leftMostVec));
	}


	void FindHull(List<int> pointIndices, int indexA, int indexB)
	{

        Vector2 pointA = allPoints[indexA];
        Vector2 pointB = allPoints[indexB];

		// Find P = furthest point from line AB
		float furthestVal = float.MinValue;
		int furthestIndex = 0;
		foreach (int i in pointIndices)
		{
			float pseudoDst = Geometry.PseudoDistanceFromPointToLine(allPoints[indexA], allPoints[indexB], allPoints[i]);
			if (pseudoDst > furthestVal)
			{
				furthestVal = pseudoDst;
				furthestIndex = i;
			}
		}

        Vector2 pointP = allPoints[furthestIndex];

#if VISUAL_DEBUG_MODE
		debugger.BeginFrame("Find furthest point from line on side 1", false);
        debugger.SetColour(Colours.lightRed);
        debugger.DrawLineSegment(pointA, pointB);
        debugger.DrawPoints(pointIndices.Select(x => (allPoints[x])), debugHighlightedPointRadius);


        Vector2 d1 = (pointA-pointB).normalized;
        int side = Geometry.SideOfLine(pointA, pointB, pointP);
        Vector2 dP = new Vector2(-d1.y*side, d1.x*side);
        float d = UnityEditor.HandleUtility.DistancePointToLineSegment(pointP, pointA, pointB);
        debugger.DrawLineSegment(pointP, pointP + dP * d);
        debugger.SetColour(Colours.darkRed);
		debugger.DrawPointWithLabel(pointP, debugHullPointRadius, string.Format("Furthest point ({0})", d));

#endif

		pointsOnHull.Add(allPoints[furthestIndex]);

#if VISUAL_DEBUG_MODE
		debugger.BeginFrame("Add furthest point to hull", true);
        debugger.SetColour(Color.black);
		debugger.DrawPoint(pointP, debugHullPointRadius);
#endif

		// Now call FindHull on points not in triangle ABP (where P is furthest point).
		List<int> sideOne = new List<int>();
		List<int> sideTwo = new List<int>();


		foreach (int i in pointIndices)
		{
			if (i != furthestIndex)
			{
                if (!Geometry.PointInTriangle(pointA, pointB, pointP, allPoints[i]))
				{
                    if (Geometry.SideOfLine(pointA.x,pointA.y, pointP.x, pointP.y, allPoints[i].x,allPoints[i].y) == -1)
					{
						sideOne.Add(i);
					}
					else
					{
						sideTwo.Add(i);
					}
				}
				else
				{
					pointsNotOnHull.Add(allPoints[i]);
				}
			}
		}

#if VISUAL_DEBUG_MODE
		debugger.BeginFrame("Find points on either side of triangle ABC. Points inside the triangle cannot be part of hull.", false);
        debugger.SetColour(Colours.lightBlue);
        debugger.DrawPoints(sideOne.Select(x=>allPoints[x]), debugHighlightedPointRadius);
        debugger.SetColour(Colours.lightGreen);
        debugger.DrawPoints(sideTwo.Select(x => allPoints[x]), debugHighlightedPointRadius);
        debugger.SetColour(Colours.lightRed);
        debugger.DrawLine(true, pointA, pointB, pointP);
#endif




		if (sideOne.Count > 0)
		{
			FindHull(sideOne, indexA, furthestIndex);
		}
		if (sideTwo.Count > 0)
		{
			FindHull(sideTwo, furthestIndex, indexB);
		}

	}



	int Sort(Vector2 a, Vector2 b, Vector2 root)
	{

		if (a == b)
		{
			return 0;
		}
		if (a == root)
		{
			return -1;
		}
		if (b == root)
		{
			return 1;
		}

		Vector2 ar = a - root;
		Vector2 br = b - root;

		float aS = ar.y / ar.magnitude;
		float bS = br.y / br.magnitude;
		return (aS > bS) ? -1 : 1;
	}

}
