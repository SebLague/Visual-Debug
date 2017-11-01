using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickHullTest : MonoBehaviour {

    public int numPoints = 50;
    public int radius = 20;
    public int seed = 74;
    Vector2[] points;

    [ContextMenu("Run QuickHull")]
    void X()
    {
		points = new Vector2[numPoints];
		Random.InitState(seed);
		for (int i = 0; i < numPoints; i++)
		{
            //points[i] = new Vector2(Random.value * 2 - 1, Random.value * 2 - 1) * radius;
            float r = Random.value * Mathf.PI * 2;
            points[i] = new Vector2(Mathf.Sin(r), Mathf.Cos(r)) * Random.value * radius;
		}
        QuickHull q = new QuickHull(points);
        string sq = q.ToString();
        sq = Do(sq);
    }

	string Do(string s)
	{
        return s + "A";
	}
}




public interface IHull {
	List<Vector2> pointsOnHull { get; set; }
	List<Vector2> pointsNotOnHull { get; set;}

	void Recalculate(Vector2[] points);
}
