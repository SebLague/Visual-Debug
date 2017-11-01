using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;


namespace VisualDebugging
{
    public partial class VisualDebugger : MonoBehaviour
    {
        const string runningInUnityEditor = "UNITY_EDITOR";

        const int defaultFontSize = 12;

        public List<Frame> frames = new List<Frame>();
        public int frameIndex = -1;
        public float timeBetweenFrames = .5f;
        public bool loop;
        public bool requiresSave;

        Color currentColour = Color.white;

        void AddArtistToCurrentFrame(SceneArtist artist)
        {
            if (frames.Count == 0)
            {
                BeginFrame();
            }
            frames[frames.Count - 1].AddArtist(artist);
        }

        public Frame CurrentFrame
        {
            get
            {
                if (frameIndex >= 0 && frameIndex < frames.Count)
                {
                    return frames[frameIndex];
                }
                return null;
            }

        }

        public IEnumerable<Vector3> EnumerableVector2ToVector3(IEnumerable<Vector2> v2)
        {
            return v2.Select(v => new Vector3(v.x, v.y, 0));
        }

	}

}