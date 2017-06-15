using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public enum Algorithm {QuickHull,JarvisMarch
	};

	public Algorithm algorithm;

	public int numPoints = 50;
	public float radius = 20;
	public int seed;
	public bool drawLines;
	public bool logTime;
	public int iterations;

	Vector2[] points;
	List<int> hullPointIndices;

	void Start () {
		points = new Vector2[numPoints];
		Random.InitState (seed);
		for (int i = 0; i < numPoints; i++) {
			points [i] = Random.insideUnitCircle * radius;
		}

		//IHull hull = new QuickHull ();
		IHull hull = new JarvisMarch();

		switch (algorithm) {
		case Algorithm.JarvisMarch:
			hull = new JarvisMarch ();
			break;
		case Algorithm.QuickHull:
			hull = new QuickHull ();
			break;
		}
		hullPointIndices = hull.GetHullPoints (points);

		if (logTime) {
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();
			for (int i = 0; i < iterations; i++) {
				hull.GetHullPoints (points);
			}
			sw.Stop ();
			print ("time: " + sw.ElapsedMilliseconds + "  iterations: " + iterations);
		}
	}
	

	void OnDrawGizmos () {
		if (points != null && hullPointIndices != null) {
			for (int i = 0; i < points.Length; i++) {
				Gizmos.color = (hullPointIndices.Contains(i))?Color.red:Color.white;
				Gizmos.DrawSphere (points[i], .3f);
			}

			if (drawLines) {
				Gizmos.color = Color.green;
				for (int i = 0; i < hullPointIndices.Count; i++) {
					Gizmos.DrawLine(points[hullPointIndices[i]],points[hullPointIndices[(i+1)%hullPointIndices.Count]]);
				}
			}
		}
	}
}

public interface IHull {
	List<int> GetHullPoints(Vector2[] points);
}
