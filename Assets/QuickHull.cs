using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickHull {

	Vector2[] allPoints;
	public List<Vector2> hullPoints;
	int callNum;
	MonoBehaviour m;
	int globCn;
	float t = 2.5f;

	public QuickHull(Vector2[] points,MonoBehaviour m) {
		this.allPoints = points;
		hullPoints = new List<Vector2> ();
		this.m = m;

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

		hullPoints.Add (allPoints [leftmostIndex]);
		hullPoints.Add (allPoints [rightmostIndex]);


		Vector2 dir = (points [leftmostIndex] - points [rightmostIndex]).normalized;
		List<int> sideOne = new List<int> ();
		List<int> sideTwo = new List<int> ();

		for (int i = 0; i < points.Length; i++) {
			if (i != rightmostIndex && i != leftmostIndex) {
				if (SideOfLine (allPoints [rightmostIndex], allPoints [leftmostIndex], allPoints [i]) >= 0) {
					sideOne.Add (i);
				} else {
					sideTwo.Add (i);
				}
			}
		}


		m.StartCoroutine(FindHull (sideOne, leftmostIndex, rightmostIndex));
		m.StartCoroutine(FindHull (sideTwo, leftmostIndex, rightmostIndex));

	
	
	}


	IEnumerator FindHull(List<int> pointIndices, int indexA, int indexB) {
		callNum++;
		globCn++;
		int cN = callNum;
		yield return new WaitForSeconds ((callNum-1) * t);
		//Debug.Log("Start: " +cN + "after: " + ((cN-1) * t));
		//Debug.Log("End: " +cN);
		if (pointIndices.Count == 0) {
			yield break;
		}
		// find furthest point from line AB
		float furthestVal = float.MinValue;
		int furthestIndex = 0;
		foreach (int i in pointIndices) {
			float pseudoDst = PseudoDistanceFromPointToLine (allPoints [indexA], allPoints [indexB], allPoints[i]);
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
		int signToLeftOfAP = SideOfLine (allPoints [indexA], allPoints [indexB], testPoint);

		foreach (int i in pointIndices) {
			if (i != furthestIndex) {
				if (!PointInTriangle (allPoints [indexA], allPoints [indexB], allPoints [furthestIndex], allPoints[i])) {
					Color col = Color.white;
					if (SideOfLine (allPoints [indexA], allPoints [furthestIndex], allPoints [i]) == signToLeftOfAP) {
						col = Color.green;
						sideOne.Add (i);
					}else {
						col = Color.red;
						sideTwo.Add (i);
					}
			

					Debug.DrawLine (allPoints [i]-Vector2.up *.5f, allPoints [i]+Vector2.up *.5f, col, t);
					Debug.DrawLine (allPoints [i]-Vector2.right *.5f, allPoints [i]+Vector2.right *.5f, col, t);
				}
			}
		}


		hullPoints.Add (allPoints [furthestIndex]);
		if (cN != 1)
			callNum--;

		Debug.Log (globCn);
		Debug.DrawLine (allPoints [indexA], allPoints [indexB], Color.white, t);
		Debug.DrawLine (allPoints [indexA], allPoints [furthestIndex], Color.green, t);
		Debug.DrawLine (allPoints [indexB], allPoints [furthestIndex], Color.red, t);



		if (sideOne.Count > 0) {
			m.StartCoroutine(FindHull (sideOne, indexA, furthestIndex));
		}
		if (sideTwo.Count > 0) {
			m.StartCoroutine(FindHull (sideTwo, indexB, furthestIndex));
		}

	}

	public static float PseudoDistanceFromPointToLine(Vector2 a, Vector2 b, Vector2 c)
	{
		return Mathf.Abs((c.x - a.x) * (-b.y + a.y) + (c.y - a.y) * (b.x - a.x));
	}

	public static int SideOfLine(Vector2 a, Vector2 b, Vector2 c)
	{
		return Mathf.RoundToInt(Mathf.Sign((c.x - a.x) * (-b.y + a.y) + (c.y - a.y) * (b.x - a.x)));
	}

	public static bool PointInTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 p)
	{
		float s1 = c.y - a.y;
		float s2 = c.x - a.x;
		float s3 = b.y - a.y;
		float s4 = p.y - a.y;

		double w1 = (a.x * s1 + s4 * s2 - p.x * s1) / (s3 * s2 - (b.x-a.x) * s1);
		double w2 = (s4- w1 * s3) / s1;
		return w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1;
	}


	//

}
