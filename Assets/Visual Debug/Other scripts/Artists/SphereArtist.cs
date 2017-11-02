
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace VisualDebugging.Internal
{

    public class SphereArtist : SceneArtist
    {
        public Vector3[] points;
        public float radius;
        public bool drawWireframe;

        public SphereArtist(IEnumerable<Vector3> points, float radius, bool drawWireframe = false)
        {
            this.artistType = typeof(SphereArtist).ToString();
            this.points = points.ToArray();
            this.radius = radius;
            this.drawWireframe = drawWireframe;
        }

        public override void Draw(bool isActive)
        {
#if UNITY_EDITOR
			base.Draw(isActive);

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
                    float size = radius * 2;
                    Handles.SphereHandleCap(0, v, Quaternion.identity, size, EventType.Repaint);
                }
            }
#endif
        }
    }
}
