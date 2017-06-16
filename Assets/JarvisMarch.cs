using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarvisMarch : IHull {

	public List<Vector2> pointsOnHull { get; set; }
	public List<Vector2> pointsNotOnHull {get; set; }

	public JarvisMarch(Vector2[] points) {
		Recalculate (points);
	}

	public void Recalculate(Vector2[] points) {
		GetHullPoints (points);
	}

	void GetHullPoints(Vector2[] points) {
		pointsOnHull = new List<Vector2> ();
		pointsNotOnHull = new List<Vector2> (points);

		float leftmostX = float.MaxValue;
		int firstHullPointIndex = 0;

		// find leftmost point (guaranteed to be hull point)
		for (int i = 0; i < points.Length; i++) {
			if (points [i].x < leftmostX) {
				leftmostX = points [i].x;
				firstHullPointIndex = i;
			}
		}

		int pointOnHullIndex = firstHullPointIndex;

		while (true) {


			pointsOnHull.Add (points[pointOnHullIndex]);
			pointsNotOnHull.Remove (points[pointOnHullIndex]);
			int endpointIndex = 0;

			for (int i = 1; i < points.Length; i++) {
				if (endpointIndex == pointOnHullIndex || Geometry.SideOfLine(points[endpointIndex],points[pointOnHullIndex],points[i]) == -1) {
					endpointIndex = i;
				}
			}

			pointOnHullIndex = endpointIndex;

			if (endpointIndex == firstHullPointIndex) {
				break;
			}
		}
	}



}
