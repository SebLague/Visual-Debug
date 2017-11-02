using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualDebugging.Internal
{
    [System.Serializable]
    public class DebugData
    {
        public const int defaultFontSize = 18;

        public List<Frame> frames;
        public Color currentActiveColour;
        public Color currentBackgroundColour;
        public int currentFontSize;
        public bool dontShowNextElementWhenFrameIsInBackground;

        public DebugData()
        {
            frames = new List<Frame>();
            currentActiveColour = Color.white;
            currentBackgroundColour = currentActiveColour;

            currentFontSize = defaultFontSize;
        }
    }
}