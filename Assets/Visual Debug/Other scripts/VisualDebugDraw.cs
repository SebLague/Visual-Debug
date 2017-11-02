using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;
using VisualDebugging.Internal;

namespace VisualDebugging
{
    public static partial class VisualDebug
    {

        /*
         * Draw points 
         */
        
		[Conditional(runningInUnityEditor)]
		public static void DrawPoint(Vector3 position, float radius, bool wireframe = false)
		{
			DrawPoints(new Vector3[] { position }, radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawPoints(IEnumerable<Vector3> points, float radius, bool wireframe = false)
		{
            AddArtistToCurrentFrame(new SphereArtist(points, radius, wireframe));
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawPoints(IEnumerable<Vector2> points, float radius, bool wireframe = false)
		{
			DrawPoints(EnumerableVector2ToVector3(points), radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawPoints(float radius, bool wireframe, params Vector2[] points)
		{
			DrawPoints(EnumerableVector2ToVector3(points), radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawPoints(float radius, bool wireframe, params Vector3[] points)
		{
			DrawPoints(points, radius, wireframe);
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawPointWithLabel(Vector3 position, float radius, string text, int fontSize, bool wireframe = false)
		{
            bool dontShowInBackground = debugData.dontShowNextElementWhenFrameIsInBackground;
			DrawPoint(position, radius, wireframe);
            debugData.dontShowNextElementWhenFrameIsInBackground = dontShowInBackground;
            DrawTextWithHeightOffset(position + Vector3.up * radius, text, debugData.currentFontSize, true,1);
		}

        
		[Conditional(runningInUnityEditor)]
		public static void DrawPointWithLabel(Vector3 position, float radius, string text, bool wireframe = false)
		{
            DrawPointWithLabel(position, radius, text, debugData.currentFontSize, wireframe);
		}


		/*
         * Draw Lines
         */

		[Conditional(runningInUnityEditor)]
		public static void DrawLineSegment(Vector3 lineStart, Vector3 lineEnd)
		{
            AddArtistToCurrentFrame(new LineArtist(new Vector3[] { lineStart, lineEnd }));
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawLineSegmentWithLabel(Vector3 lineStart, Vector3 lineEnd, string text)
		{
            bool dontShowInBackground = debugData.dontShowNextElementWhenFrameIsInBackground;
            Vector3 textCentre = (lineStart + lineEnd) / 2f;
            DrawText(textCentre, text, true);
            debugData.dontShowNextElementWhenFrameIsInBackground = dontShowInBackground;
			AddArtistToCurrentFrame(new LineArtist(new Vector3[] { lineStart, lineEnd }));
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawLineSegments(IEnumerable<Vector3> lineSegments)
		{
			AddArtistToCurrentFrame(new LineArtist(lineSegments));
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawLineSegments(IEnumerable<Vector2> lineSegments)
		{
			DrawLineSegments(lineSegments.Select(v => new Vector3(v.x, v.y, 0)));
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawLine(IEnumerable<Vector3> points, bool joinFirstAndLast = false)
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
		public static void DrawLine(IEnumerable<Vector2> points, bool joinFirstAndLast = false)
		{
			DrawLine(EnumerableVector2ToVector3(points), joinFirstAndLast);
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawLine(bool joinFirstAndLast, params Vector3[] points)
		{
			DrawLine(points, joinFirstAndLast);
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawLine(bool joinFirstAndLast, params Vector2[] points)
		{
			DrawLine(EnumerableVector2ToVector3(points), joinFirstAndLast);
		}

		/*
         * Labels
         */

		[Conditional(runningInUnityEditor)]
		public static void DrawTextWithHeightOffset(Vector3 position, string text, int fontSize, bool centreAlign, float heightOffset)
		{
            AddArtistToCurrentFrame(new LabelArist(position, text, centreAlign, fontSize,heightOffset));
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawText(Vector3 position, string text, int fontSize, bool centreAlign = true)
		{
            DrawTextWithHeightOffset(position, text, fontSize, centreAlign,0);
		}

		[Conditional(runningInUnityEditor)]
		public static void DrawText(Vector3 position, string text, bool centreAlign = true)
		{
            DrawText(position, text, debugData.currentFontSize, centreAlign);
		}

		/*
         * Misc
         */

		[Conditional(runningInUnityEditor)]
		public static void DrawCube(Vector3 centre, float size)
		{
			AddArtistToCurrentFrame(new CubeArtist(centre, size));
		}
	}
}
