using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging
{
    public class LabelArist : SceneArtist
    {
        public Vector3 position;
        public string text;
        public bool centreAlign;
        public int fontSize;

        public LabelArist(Color colour, Vector3 position, string text, bool centreAlign, int fontSize) : base(colour)
        {
            artistType = typeof(LabelArist).ToString();
            this.position = position;
            this.text = text;
            this.centreAlign = centreAlign;
            this.fontSize = fontSize;
        }

        public override void Draw()
        {
            base.Draw();
#if UNITY_EDITOR

            Vector3 alignment = Vector3.zero;
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.fontSize = fontSize;

            if (centreAlign)
            {
                Camera sceneCamera = SceneView.GetAllSceneCameras()[0];
                Vector2 labelSize = labelStyle.CalcScreenSize(labelStyle.CalcSize(new GUIContent(text)));

                float screenWidthWorld = 0;
                if (sceneCamera.orthographic)
                {
                    screenWidthWorld = sceneCamera.orthographicSize * 2 * sceneCamera.aspect;
                }
                else
                {
					float frustHeight = 2 * Vector3.Distance(sceneCamera.transform.position, position) * Mathf.Tan(sceneCamera.fieldOfView * .5f * Mathf.Deg2Rad);
					screenWidthWorld = sceneCamera.aspect * frustHeight;
                }
               
                float labelWidthAsPercentOfScreenWidth = labelSize.x / Screen.width;
                float labelWorldWidth = screenWidthWorld * labelWidthAsPercentOfScreenWidth;
                alignment = -sceneCamera.transform.right * labelWorldWidth / 2f;

            }


            Handles.Label(position + alignment, text,labelStyle);
           
#endif
        }
    }
}
