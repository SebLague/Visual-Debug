using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public int numPoints = 50;
	public float radius = 20;
	public int seed;
	Vector2[] points;
	List<int> hullPointIndices;

	void Start () {
		points = new Vector2[numPoints];
		Random.InitState (seed);
		for (int i = 0; i < numPoints; i++) {
			points [i] = Random.insideUnitCircle * radius;
		}

		IHull hull = new QuickHull ();
		hullPointIndices = hull.GetHullPoints (points);

	}
	

	void OnDrawGizmos () {
		if (points != null && hullPointIndices != null) {
			for (int i = 0; i < points.Length; i++) {
				Gizmos.color = (hullPointIndices.Contains(i))?Color.red:Color.white;
				Gizmos.DrawSphere (points[i], .3f);
			}
		}
	}
}

public interface IHull {
	List<int> GetHullPoints(Vector2[] points);
}
