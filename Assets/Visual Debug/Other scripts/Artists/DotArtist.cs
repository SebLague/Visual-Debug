
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace VisualDebugging.Internal
{
    public class DotArtist : SceneArtist
    {
        public Vector3[] points;
        public float radius;

        public DotArtist( IEnumerable<Vector3> points, float radius, bool drawWireframe = false )
        {
            this.artistType = typeof(DotArtist).ToString();
            this.points = points.ToArray();
            this.radius = radius;
        }

        public override void Draw( bool isActive )
        {
#if UNITY_EDITOR
            base.Draw(isActive);
            foreach (Vector3 v in points)
            {
                float size = radius * 2;
                Handles.DotHandleCap(0, v, Quaternion.identity, size, EventType.Repaint);
            }
#endif
        }
    }
}
