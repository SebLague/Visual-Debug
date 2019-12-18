
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging.Internal
{
    //Not to be confused with con artists
    public class ConeArtist : SceneArtist
    {
        public Vector3 position;
        public Quaternion rotation;
        public float size;

        public ConeArtist( Vector3 position, Vector3 up, float size )
        {
            artistType = typeof(ConeArtist).ToString();
            this.position = position - up * 0.7f * size;
            rotation = Quaternion.LookRotation(up);
            this.size = size;
        }

        public override void Draw( bool isActive )
        {
#if UNITY_EDITOR
            base.Draw(isActive);
            Handles.ConeHandleCap(0, position, rotation, size, EventType.Repaint);
#endif
        }
    }
}
