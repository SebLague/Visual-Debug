using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrahamScan : IHull {


	public List<Vector2> pointsOnHull { get; set; }
	public List<Vector2> pointsNotOnHull { get; set; }

	IEnumerator A(List<Vector2> sorted) {
		pointsNotOnHull = new List<Vector2> (sorted);
		foreach (Vector2 v in sorted) {
			yield return new WaitForSeconds (.2f);
			pointsOnHull.Add (v);
			pointsNotOnHull.Remove (v);
		}
	}

	public GrahamScan(Vector2[] points) {
		Recalculate (points);
	}

	public void Recalculate(Vector2[] points) {
		Object.FindObjectOfType<Test>().StartCoroutine(GetHullPoints (points));
	}


	IEnumerator GetHullPoints(Vector2[] points) {
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

		//Object.FindObjectOfType<Test> ().StartCoroutine (A (sortedPoints));
		//pointsOnHull.Add (points [lowestIndex]);
		//return;
		pointsNotOnHull = new List<Vector2>(points);
		yield return new WaitForSeconds (2);
		pointsOnHull.Add (sortedPoints[0]);
		pointsOnHull.Add (sortedPoints[1]);
		int numHullPoints = 2;

		for (int i = 2; i < points.Length; i++) {
			bool legal = true;
		

			while (numHullPoints >= 2 && Geometry.SideOfLine(pointsOnHull[numHullPoints-2],pointsOnHull[numHullPoints-1], sortedPoints[i]) < 0) { // invalidates previous point
				Debug.DrawLine (pointsOnHull [numHullPoints - 1], sortedPoints [i],Color.red,t);
				DrawHull ();
				yield return new WaitForSeconds (t);
				legal = false;
				pointsOnHull.RemoveAt(numHullPoints-1);
				numHullPoints--;
				pointsNotOnHull.Add (points [i - 1]);


			}


			Debug.DrawLine (pointsOnHull [numHullPoints - 1], sortedPoints [i],Color.yellow,t);
		

			DrawHull ();
			yield return new WaitForSeconds (t);

			Debug.Log ("Add: " + i + "  ");
			pointsOnHull.Add (sortedPoints [i]);
			numHullPoints++;

		
		}
		t = 20;
		DrawHull ();
		Debug.DrawLine (pointsOnHull[pointsOnHull.Count-1],pointsOnHull[0], Color.green, t);
	}
	float t = .3f;
	void DrawHull() {
		for (int i = 0; i < pointsOnHull.Count-1; i++) {
			Debug.DrawLine (pointsOnHull[i],pointsOnHull[i+1], Color.green, t);
			//bool isAllowed = !(numHullPoints >= 2 && Geometry.SideOfLine (sortedPoints [numHullPoints - 2], sortedPoints [numHullPoints - 1], sortedPoints [i]) < 0);
			//Debug.DrawLine (sortedPoints[numHullPoints-1],sortedPoints[i], (isAllowed)?Color.yellow:Color.red, 2);
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
