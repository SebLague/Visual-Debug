
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging.Internal
{
    public class LineArtist : SceneArtist
    {
        public Vector3[] lineSegments;

        public LineArtist(IEnumerable<Vector3> lineSegments)
        {
            artistType = typeof(LineArtist).ToString();
            this.lineSegments = lineSegments.ToArray();

        }

        public override void Draw(bool isActive)
        {
#if UNITY_EDITOR
			base.Draw(isActive);
            Handles.DrawLines(lineSegments);
#endif
        }
    }
}
