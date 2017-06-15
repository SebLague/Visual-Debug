using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarvisMarch : IHull {

	public List<int> GetHullPoints(Vector2[] points) {
		List<int> hullPoints = new List<int> ();

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


			hullPoints.Add (pointOnHullIndex);

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

		return hullPoints;
	}

}
