
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging.Internal
{
    public class CubeArtist : SceneArtist
    {
        public Vector3 centre;
        public float size;

        public CubeArtist(Vector3 centre, float size)
        {
            artistType = typeof(CubeArtist).ToString();
            this.centre = centre;
            this.size = size;
        }

        public override void Draw(bool isActive)
        {
#if UNITY_EDITOR
			base.Draw(isActive);
            Handles.CubeHandleCap(0, centre, Quaternion.identity, size, EventType.Repaint);
#endif
        }
    }
}
