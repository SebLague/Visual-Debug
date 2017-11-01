using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

namespace VisualDebugging
{
    public partial class VisualDebugger : MonoBehaviour
    {
		[Conditional(runningInUnityEditor)]
		public void DrawPoint(Vector3 position, float radius, bool wireframe = false)
		{
			DrawPoints(new Vector3[] { position }, radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public void DrawPoints(IEnumerable<Vector3> points, float radius, bool wireframe = false)
		{
			AddArtistToCurrentFrame(new SphereArtist(currentColour, points, radius, wireframe));
		}

		[Conditional(runningInUnityEditor)]
		public void DrawPoints(IEnumerable<Vector2> points, float radius, bool wireframe = false)
		{
            DrawPoints(EnumerableVector2ToVector3(points), radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public void DrawPoints(float radius, bool wireframe, params Vector2[] points)
		{
			DrawPoints(EnumerableVector2ToVector3(points), radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public void DrawPoints(float radius, bool wireframe, params Vector3[] points)
		{
			DrawPoints(points, radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public void DrawPointWithLabel(Vector3 position, float radius, string text, int fontSize, bool wireframe = false)
		{
			DrawPoint(position, radius, wireframe);
			DrawText(position + Vector3.up * (radius + 1), text, true);
		}

		[Conditional(runningInUnityEditor)]
        public void DrawPointWithLabel(Vector3 position, float radius, string text, bool wireframe = false)
		{
            DrawPointWithLabel(position, radius, text, defaultFontSize, wireframe);
		}

	
    }

}