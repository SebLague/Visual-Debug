using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public int numPoints = 50;
	public float radius = 20;
	public int seed;
	Vector2[] points;
	Vector2[] hullPoints;
	QuickHull hull;
	void Start () {
		points = new Vector2[numPoints];
		Random.InitState (seed);
		for (int i = 0; i < numPoints; i++) {
			points [i] = Random.insideUnitCircle * radius;
		}

		hull = new QuickHull (points,this);
		//hullPoints
	}
	

	void OnDrawGizmos () {
		if (points != null) {
			Gizmos.color = Color.white;
			foreach (Vector2 p in points) {
				Gizmos.DrawSphere (p, .3f);
			}
		}
		if (hull != null) {
			Gizmos.color = Color.red;
			foreach (Vector2 p in hull.hullPoints) {
				Gizmos.DrawSphere (p, .4f);
			}
		}
	}
}
