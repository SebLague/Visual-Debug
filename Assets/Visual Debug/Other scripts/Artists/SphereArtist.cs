
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace VisualDebugging
{

    public class SphereArtist : SceneArtist
    {
        public Vector3[] points;
        public float radius;
        public bool drawWireframe;

        public SphereArtist(Color colour, IEnumerable<Vector3> points, float radius, bool drawWireframe = false) : base(colour)
        {
            this.artistType = typeof(SphereArtist).ToString();
            this.points = points.ToArray();
            this.radius = radius;
            this.drawWireframe = drawWireframe;
        }

        public override void Draw()
        {
            base.Draw();
            #if UNITY_EDITOR
            foreach (Vector3 v in points)
            {
                if (drawWireframe)
                {
                    Camera sceneCamera = SceneView.GetAllSceneCameras()[0];
                    Vector3 dirToCam = (sceneCamera.transform.position - v).normalized;
                    Handles.DrawWireDisc(v, dirToCam, radius);
                }
                else
                {
                    Handles.SphereHandleCap(0, v, Quaternion.identity, radius, EventType.Repaint);
                }
            }
            #endif
        }
    }
}
