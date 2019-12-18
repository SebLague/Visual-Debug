
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging.Internal
{
    public class PolygonArtist : SceneArtist
    {
        public Vector3[] points;

        public PolygonArtist( IEnumerable<Vector3> points )
        {
            artistType = typeof(PolygonArtist).ToString();
            this.points = points.ToArray();
        }

        public override void Draw(bool isActive)
        {
#if UNITY_EDITOR
			base.Draw(isActive);
            Handles.DrawAAConvexPolygon(points);
#endif
        }
    }
}
