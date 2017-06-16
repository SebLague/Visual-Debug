using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrahamScan : IHull {


	public List<Vector2> pointsOnHull { get; set; }
	public List<Vector2> pointsNotOnHull { get; set; }


	public GrahamScan(Vector2[] points) {
		Recalculate (points);
	}

	public void Recalculate(Vector2[] points) {
		GetHullPoints (points);
	}


	void GetHullPoints(Vector2[] points) {
		pointsOnHull = new List<Vector2> ();
		pointsNotOnHull = new List<Vector2> ();

		//find lowest point
		float lowestY = float.MaxValue;
		int lowestIndex = 0;
		for (int i = 0; i < points.Length; i++) {
			if (points [i].y < lowestY) {
				lowestY = points [i].y;
				lowestIndex = i;
			}
		}

		Vector2 lowestPoint = points [lowestIndex];

		List<Vector2> sortedPoints = new List<Vector2> (points);
		sortedPoints.Sort ((a, b) => Sort (a, b, lowestPoint));

		pointsOnHull.Add (sortedPoints[0]);
		pointsOnHull.Add (sortedPoints[1]);
		int numHullPoints = 2;

		for (int i = 2; i < points.Length; i++) {

			while (numHullPoints >= 2 && Geometry.SideOfLine(pointsOnHull[numHullPoints-2],pointsOnHull[numHullPoints-1], sortedPoints[i]) < 0) { // invalidates previous point
				pointsOnHull.RemoveAt(numHullPoints-1);
				numHullPoints--;
				pointsNotOnHull.Add (points [i - 1]);
			}

			pointsOnHull.Add (sortedPoints [i]);
			numHullPoints++;

		
		}

	}

	int Sort(Vector2 a, Vector2 b, Vector2 root) {

		if (a == b) {
			return 0;
		}
		if (a==root) {
			return -1;
		}
		if (b==root) {
			return 1;
		}

		Vector2 ar = a - root;
		Vector2 br = b - root;
	
		//sreturn 0;
		float aS = ar.x/ar.magnitude;
		float bS = br.x/br.magnitude;
		return (aS > bS) ? -1 : 1;
	}




}
