using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public enum Algorithm {QuickHull,JarvisMarch, GrahamScan
	};

	public Algorithm algorithm;

	public int numPoints = 50;
	public float radius = 20;
	public int seed;
	public bool drawLines;
	public bool logTime;
	public int iterations;

	Vector2[] points;
	IHull hull;

	void Start () {
		
		points = new Vector2[numPoints];
		Random.InitState (seed);
		for (int i = 0; i < numPoints; i++) {
			points [i] = Random.insideUnitCircle * radius;
		}
			
		switch (algorithm) {
		case Algorithm.JarvisMarch:
			hull = new JarvisMarch (points);
			break;
		case Algorithm.QuickHull:
			hull = new QuickHull (points);
			break;
		case Algorithm.GrahamScan:
			hull = new GrahamScan (points);
			break;
		}


		if (logTime) {
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();
			for (int i = 0; i < iterations; i++) {
				hull.Recalculate (points);
			}
			sw.Stop ();
			print ("time: " + sw.ElapsedMilliseconds + "  iterations: " + iterations);
		}
	}
	

	void OnDrawGizmos () {
		if (hull != null) {
			Gizmos.color = Color.red;
			foreach (Vector2 v in hull.pointsOnHull) {
				Gizmos.DrawSphere (v, .5f);
			}

			Gizmos.color = Color.white;
			foreach (Vector2 v in hull.pointsNotOnHull) {
				Gizmos.DrawSphere (v, .25f);
			}

			if (drawLines) {
				Gizmos.color = Color.green;
				for (int i = 0; i < hull.pointsOnHull.Count; i++) {
					Gizmos.DrawLine(hull.pointsOnHull[i], hull.pointsOnHull[(i+1)%hull.pointsOnHull.Count]);
				}
			}
		}
	}
}

public interface IHull {
	List<Vector2> pointsOnHull { get; set; }
	List<Vector2> pointsNotOnHull { get; set;}

	void Recalculate(Vector2[] points);
}
