using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

/*
  * Contains methods for drawing misc items
  */

namespace VisualDebugging
{
	public partial class VisualDebugger : MonoBehaviour
	{


		[Conditional(runningInUnityEditor)]
		public void DrawCube(Vector3 centre, float size)
		{
			AddArtistToCurrentFrame(new CubeArtist(currentColour, centre, size));
		}

		[Conditional(runningInUnityEditor)]
		public void DrawText(Vector3 position, string text, int fontSize, bool centreAlign = true)
		{
			AddArtistToCurrentFrame(new LabelArist(currentColour, position, text, centreAlign, fontSize));
		}

		[Conditional(runningInUnityEditor)]
		public void DrawText(Vector3 position, string text, bool centreAlign = true)
		{
            DrawText(position, text, defaultFontSize, centreAlign);
		}

	

	
	}

}