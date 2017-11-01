
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging
{
    public class LineArtist : SceneArtist
    {
        public Vector3[] lineSegments;

        public LineArtist(Color colour, IEnumerable<Vector3> lineSegments) : base(colour)
        {
            artistType = typeof(LineArtist).ToString();
            this.lineSegments = lineSegments.ToArray();
        }

        public override void Draw()
        {
            base.Draw();
            #if UNITY_EDITOR
            Handles.DrawLines(lineSegments);
            #endif
        }
    }
}
