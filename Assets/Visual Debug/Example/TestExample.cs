using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExample : MonoBehaviour {


	void Start()
	{
        ExampleAlgorithm algorithm = new ExampleAlgorithm();

	
	}

    Vector2[] GeneratePoints(int numPoints, float radius)
    {
        Vector2[] points = new Vector2[numPoints];
        for (int i = 0; i < numPoints; i++)
		{
			points[i] = Random.insideUnitCircle * radius;
		}
        return points;
    }
}
