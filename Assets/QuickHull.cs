using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickHull : IHull {

	Vector2[] allPoints;
	List<int> hullPoints;


	public List<int> GetHullPoints(Vector2[] points) {
		this.allPoints = points;
		hullPoints = new List<int> ();

		int leftmostIndex = 0;
		int rightmostIndex = 0;
		float leftmostXVal = float.MaxValue;
		float rightmostXVal = float.MinValue;

		for (int i = 0; i < points.Length; i++) {
			if (points [i].x < leftmostXVal) {
				leftmostXVal = points [i].x;
				leftmostIndex = i;
			}

			if (points [i].x > rightmostXVal) {
				rightmostXVal = points [i].x;
				rightmostIndex = i;
			}
		}

		hullPoints.Add (leftmostIndex);
		hullPoints.Add (rightmostIndex);


		Vector2 dir = (points [leftmostIndex] - points [rightmostIndex]).normalized;
		List<int> sideOne = new List<int> ();
		List<int> sideTwo = new List<int> ();

		for (int i = 0; i < points.Length; i++) {
			if (i != rightmostIndex && i != leftmostIndex) {
				if (Geometry.SideOfLine (allPoints [rightmostIndex], allPoints [leftmostIndex], allPoints [i]) >= 0) {
					sideOne.Add (i);
				} else {
					sideTwo.Add (i);
				}
			}
		}


		FindHull (sideOne, leftmostIndex, rightmostIndex);
		FindHull (sideTwo, leftmostIndex, rightmostIndex);

		return hullPoints;
	
	}


	void FindHull(List<int> pointIndices, int indexA, int indexB) {
		if (pointIndices.Count == 0) {
			return;
		}
		// find furthest point from line AB
		float furthestVal = float.MinValue;
		int furthestIndex = 0;
		foreach (int i in pointIndices) {
			float pseudoDst = Geometry.PseudoDistanceFromPointToLine (allPoints [indexA], allPoints [indexB], allPoints[i]);
			if (pseudoDst > furthestVal) {
				furthestVal = pseudoDst;
				furthestIndex = i;
			}
		}
			


		// Call FindHull on points not in triangle ABP (where P is furthest point).
		List<int> sideOne = new List<int> ();
		List<int> sideTwo = new List<int> ();

		// do test to find what sign of point to left of line AP is. TODO: Find more elegant solution.
		Vector2 triCentre = (allPoints[indexA]+allPoints[indexB]+allPoints [furthestIndex])/3f;
		Vector2 testPoint = allPoints [indexA] + (triCentre - allPoints [indexB]);
		int signToLeftOfAP = Geometry.SideOfLine (allPoints [indexA], allPoints [indexB], testPoint);

		foreach (int i in pointIndices) {
			if (i != furthestIndex) {
				if (!Geometry.PointInTriangle (allPoints [indexA], allPoints [indexB], allPoints [furthestIndex], allPoints[i])) {
					if (Geometry.SideOfLine (allPoints [indexA], allPoints [furthestIndex], allPoints [i]) == signToLeftOfAP) {
						sideOne.Add (i);
					}else {
						sideTwo.Add (i);
					}
				}
			}
		}


		hullPoints.Add (furthestIndex);


		if (sideOne.Count > 0) {
			FindHull (sideOne, indexA, furthestIndex);
		}
		if (sideTwo.Count > 0) {
			FindHull (sideTwo, indexB, furthestIndex);
		}

	}


}
