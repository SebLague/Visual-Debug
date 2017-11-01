using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

/*
  * Contains command methods
  */

namespace VisualDebugging
{
	public partial class VisualDebugger : MonoBehaviour
	{

		/// <summary>
		/// This is the same as calling Clear(); it clears any previously loaded data. This should be done before calling BeginFrame() for the first time.
		/// </summary>
		[Conditional(runningInUnityEditor)]
		public void Initialize()
		{
            Clear();
		}

		/// <summary>
		/// Clear any previously loaded data. This should be done before calling BeginFrame() for the first time.
		/// </summary>
		[Conditional(runningInUnityEditor)]
		public void Clear()
		{
			requiresSave = true;
			frameIndex = -1;
            frames = new List<Frame>();
			SetColour(Color.white);
		}

        /// <summary>
        /// Begin a new frame.
        /// </summary>
        /// <param name="description">Description.</param>
        /// <param name="dontErase">If true, this frame won't be erased when the next frame is drawn.</param>
		[Conditional(runningInUnityEditor)]
		public void BeginFrame(string description, bool dontErase = false)
		{
			requiresSave = true;
			frames.Add(new Frame(description, dontErase, frames.Count));

		}

		[Conditional(runningInUnityEditor)]
		public void BeginFrame()
		{
			BeginFrame("");
		}



		[Conditional(runningInUnityEditor)]
		public void SetColour(Color colour)
		{
			currentColour = colour;
		}

		[Conditional(runningInUnityEditor)]
		public void SetColour(string hex)
		{
            currentColour = Colours.HexStringToColour(hex);
		}


	}

}