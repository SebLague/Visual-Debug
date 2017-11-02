using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging.Internal
{
    public class LabelArist : SceneArtist
    {
        public Vector3 position;
        public string text;
        public bool centreAlign;
        public int fontSize;
        public float heightOffset;

        public LabelArist(Vector3 position, string text, bool centreAlign, int fontSize, float heightOffset)
        {
            artistType = typeof(LabelArist).ToString();
            this.position = position;
            this.text = text;
            this.centreAlign = centreAlign;
            this.fontSize = fontSize;
            this.heightOffset = heightOffset;
        }

        public override void Draw(bool isActive)
        {

#if UNITY_EDITOR
			base.Draw(isActive);

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            int fontSizeScaledWithSceneZoom = Mathf.RoundToInt(fontSize / HandleUtility.GetHandleSize(position));
            labelStyle.fontSize = Mathf.Min(fontSize, Mathf.Max(fontSizeScaledWithSceneZoom,1));
                      
            Camera sceneCamera = SceneView.GetAllSceneCameras()[0];

            Vector2 labelSize = labelStyle.CalcSize(new GUIContent(text));

            float screenWidthWorld = 0;
            float screenHeightWorld = 0;
            if (sceneCamera.orthographic)
            {
                screenHeightWorld = sceneCamera.orthographicSize * 2;
                screenWidthWorld = screenHeightWorld * sceneCamera.aspect;
            }
            else
            {
				screenHeightWorld = 2 * Vector3.Distance(sceneCamera.transform.position, position) * Mathf.Tan(sceneCamera.fieldOfView * .5f * Mathf.Deg2Rad);
				screenWidthWorld = sceneCamera.aspect * screenHeightWorld;
            }

            Vector2 labelSizeAsPercentOfScreen = new Vector2(labelSize.x / Screen.width, labelSize.y / Screen.height);
            Vector2 labelWorldSize = new Vector2(labelSizeAsPercentOfScreen.x * screenWidthWorld, labelSizeAsPercentOfScreen.y * screenHeightWorld);

            Vector3 alignment = sceneCamera.transform.up * labelWorldSize.y * (.5f + heightOffset);
            if (centreAlign)
            {
                alignment += -sceneCamera.transform.right * labelWorldSize.x / 2f;
            }

            Handles.Label(position + alignment, text,labelStyle);
           
#endif
        }
    }
}
