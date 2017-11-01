
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging
{
    public class CubeArtist : SceneArtist
    {
        public Vector3 centre;
        public float size;

        public CubeArtist(Color colour, Vector3 centre, float size) : base(colour)
        {
            artistType = typeof(CubeArtist).ToString();
            this.centre = centre;
            this.size = size;
        }

        public override void Draw()
        {
            base.Draw();
#if UNITY_EDITOR
            Handles.CubeHandleCap(0, centre, Quaternion.identity, size, EventType.Repaint);
#endif
        }
    }
}
