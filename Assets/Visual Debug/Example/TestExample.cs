using UnityEngine;

namespace VisualDebugging.Example
{
    public class TestExample : MonoBehaviour
    {

        public int numPoints = 8;
        public float radius = 1.6f;
        public int seed = 387;

        void Start()
        {
            Run();
        }

        public void Run()
        {
            ExampleAlgorithm.FindClosestPairOfPoints(GeneratePoints());
        }

        Vector3[] GeneratePoints()
        {
            Random.InitState(seed);
            Vector3[] points = new Vector3[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                points[i] = Random.insideUnitSphere * radius;
            }
            return points;
        }
    }
}