using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

namespace VisualDebugging
{
    /*
     * Contains methods for drawing lines
     */

	public partial class VisualDebugger : MonoBehaviour
	{

        [Conditional(runningInUnityEditor)]
        public void DrawLineSegment(Vector3 lineStart, Vector3 lineEnd)
        {
            AddArtistToCurrentFrame(new LineArtist(currentColour, new Vector3[] { lineStart, lineEnd }));
        }

        [Conditional(runningInUnityEditor)]
        public void DrawLineSegments(IEnumerable<Vector3> lineSegments)
        {
            AddArtistToCurrentFrame(new LineArtist(currentColour, lineSegments));
        }

        [Conditional(runningInUnityEditor)]
        public void DrawLineSegments(IEnumerable<Vector2> lineSegments)
        {
            DrawLineSegments(lineSegments.Select(v => new Vector3(v.x, v.y, 0)));
        }

        [Conditional(runningInUnityEditor)]
        public void DrawLine(IEnumerable<Vector3> points, bool joinFirstAndLast = false)
        {
            Vector3[] pointsArray = points.ToArray();
            List<Vector3> lineSegments = new List<Vector3>();
            for (int i = 0; i < pointsArray.Length - 1; i++)
            {
                lineSegments.Add(pointsArray[i]);
                lineSegments.Add(pointsArray[i + 1]);
            }
            if (joinFirstAndLast)
            {
                lineSegments.Add(pointsArray[pointsArray.Length - 1]);
                lineSegments.Add(pointsArray[0]);
            }
            DrawLineSegments(lineSegments);
        }

        [Conditional(runningInUnityEditor)]
        public void DrawLine(IEnumerable<Vector2> points, bool joinFirstAndLast = false)
        {
            DrawLine(EnumerableVector2ToVector3(points),joinFirstAndLast);
        }

		[Conditional(runningInUnityEditor)]
        public void DrawLine(bool joinFirstAndLast, params Vector3[] points)
		{
			DrawLine(points, joinFirstAndLast);
		}

		[Conditional(runningInUnityEditor)]
		public void DrawLine(bool joinFirstAndLast, params Vector2[] points)
		{
			DrawLine(EnumerableVector2ToVector3(points), joinFirstAndLast);
		}
    }
}