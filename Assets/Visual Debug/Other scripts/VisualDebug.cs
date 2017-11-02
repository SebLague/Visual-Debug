using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VisualDebugging.Internal;

namespace VisualDebugging
{
    public static partial class VisualDebug
    {

		const string runningInUnityEditor = "UNITY_EDITOR";

        static DebugData debugData = new DebugData();

		/// <summary>
		/// Save visual debug data. Call this when finished creating frames.
		/// </summary>
		[Conditional(runningInUnityEditor)]
		public static void Save()
		{
            SaveLoad.Save(debugData.frames.ToArray());
		}

		/// <summary>
		/// Clear any previously loaded data. This should be done before calling BeginFrame() for the first time.
        /// This is identical to calling Clear().
		/// </summary>
		[Conditional(runningInUnityEditor)]
		public static void Initialize()
		{
            Clear();
		}

		/// <summary>
		/// Clear any previously loaded data. This should be done before calling BeginFrame() for the first time.
		/// This is identical to calling Initialize().
		/// </summary>
		[Conditional(runningInUnityEditor)]
		public static void Clear()
		{
            debugData = new DebugData();
		}

		/// <summary>
		/// Begin a new frame.
		/// </summary>
		/// <param name="description">Description.</param>
		/// <param name="keepInBackground">If true, this frame will remain in the background when the next frames are drawn, i.e. it won't be erased.</param>
		[Conditional(runningInUnityEditor)]
        public static void BeginFrame(string description, bool keepInBackground = false)
		{
            debugData.dontShowNextElementWhenFrameIsInBackground = false;
            debugData.frames.Add(new Frame(description, keepInBackground, debugData.frames.Count));

		}

		[Conditional(runningInUnityEditor)]
		public static void BeginFrame()
		{
			BeginFrame("");
		}

		/// <summary>
		/// The next element drawn to the current frame will not be shown when the frame is in the background.
        /// (Only applicable to frames where keepInBackground is true).
		/// </summary>
		[Conditional(runningInUnityEditor)]
        public static void DontShowNextElementWhenFrameIsInBackground()
        {
            debugData.dontShowNextElementWhenFrameIsInBackground = true;
        }

        /// <summary>
        /// Set the active and inactive colours.
        /// </summary>
        /// <param name="activeColour">Colour to use when frame is active.</param>
        /// <param name="backgroundColour">Colour to use when frame is in background (only applies to frames where keepInBackground is true).</param>
		[Conditional(runningInUnityEditor)]
        public static void SetColour(Color activeColour, Color backgroundColour)
		{
			debugData.currentActiveColour = activeColour;
            debugData.currentBackgroundColour = backgroundColour;
		}


		/// <summary>
		/// Set the active and inactive colours.
		/// </summary>
		/// <param name="activeHexColour">Colour to use when frame is active.</param>
		/// <param name="backgroundHexColour">Colour to use when frame is in background (only applies to frames where keepInBackground is true).</param>
		[Conditional(runningInUnityEditor)]
		public static void SetColour(string activeHexColour, string backgroundHexColour)
		{
            SetColour(Colours.HexStringToColour(activeHexColour), Colours.HexStringToColour(backgroundHexColour));
		}

		[Conditional(runningInUnityEditor)]
		public static void SetColour(Color colour)
		{
            SetColour(colour, colour);
		}

		[Conditional(runningInUnityEditor)]
        public static void SetColour(string hexColour)
		{
			SetColour(Colours.HexStringToColour(hexColour), Colours.HexStringToColour(hexColour));
		}

		[Conditional(runningInUnityEditor)]
        public static void SetDefaultFontSize(int fontSize)
		{
            debugData.currentFontSize = fontSize;
		}

		[Conditional(runningInUnityEditor)]
		public static void ResetDefaultFontSize()
		{
            debugData.currentFontSize = DebugData.defaultFontSize;
		}

		static void AddArtistToCurrentFrame(SceneArtist artist)
		{
			if (debugData.frames.Count == 0)
			{
				BeginFrame();
			}

            artist.SetColour(debugData.currentActiveColour,debugData.currentBackgroundColour);
            artist.showWhenInBackground = !debugData.dontShowNextElementWhenFrameIsInBackground;
			debugData.frames[debugData.frames.Count - 1].AddArtist(artist);
		}

		static IEnumerable<Vector3> EnumerableVector2ToVector3(IEnumerable<Vector2> v2)
		{
			return v2.Select(v => new Vector3(v.x, v.y, 0));
		}
	}
}