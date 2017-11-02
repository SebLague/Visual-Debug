using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace VisualDebugging.Internal
{
    [CustomEditor(typeof(DebugViewer))]
    public class VisualDebugEditor : Editor
    {

        DebugViewer viewer;
        bool isPlaying;
        float previousFrameTime;

        public override void OnInspectorGUI()
        {

            Frame currentFrame = viewer.CurrentFrame;

            GUILayout.Space(5);
            GUILayout.Label(string.Format("Frame {0} of {1}", viewer.frameIndex + 1, viewer.NumFrames));

            // Frame number slider
            GUI.enabled = !isPlaying && viewer.NumFrames > 0;
            viewer.frameIndex = EditorGUILayout.IntSlider(viewer.frameIndex + 1, 0, viewer.NumFrames) - 1;
            GUILayout.Space(6);

            // Play/pause button
            GUILayout.BeginHorizontal();
            string playPauseButtonText = (isPlaying) ? "Pause" : "Play";
            GUI.enabled = viewer.NumFrames > 0;
            if (GUILayout.Button(playPauseButtonText))
            {
                isPlaying = !isPlaying;
            }

            // Time between frames and loop toggle
            GUI.enabled = true;
            float defaultLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 35;
            EditorGUIUtility.fieldWidth = 20;
            viewer.timeBetweenFrames = EditorGUILayout.FloatField("Time", viewer.timeBetweenFrames);
            viewer.timeBetweenFrames = Mathf.Clamp(viewer.timeBetweenFrames, 0, float.MaxValue);

            viewer.loop = EditorGUILayout.Toggle("Loop", viewer.loop);
            EditorGUIUtility.labelWidth = defaultLabelWidth;
            GUILayout.EndHorizontal();

            // Next/previous frame buttons
            GUI.enabled = !isPlaying && viewer.NumFrames > 0;
            if (GUILayout.Button("Next Frame"))
            {
                if (viewer.frameIndex < viewer.NumFrames - 1)
                {
                    viewer.frameIndex++;
                }
            }

            if (GUILayout.Button("Previous Frame"))
            {
                if (viewer.frameIndex >= 0)
                {
                    viewer.frameIndex--;
                }
            }

            // Description box
            GUI.enabled = true;
            string description = (currentFrame == null) ? "" : currentFrame.description;
            GUILayout.Label("Description:");
            EditorGUILayout.HelpBox(description, MessageType.None);

            if (GUI.changed)
            {
                SceneView.RepaintAll();
            }
        }

		private void OnSceneGUI()
		{
			Event guiEvent = Event.current;

            // Draw all frames up to current frame index
			if (guiEvent.type == EventType.Repaint)
			{
				for (int i = 0; i <= viewer.frameIndex; i++)
				{
					if (i < viewer.NumFrames)
					{
						viewer.frames[i].Draw(viewer.frameIndex);

					}
				}
			}
		}

        void EditorUpdate()
        {
            if (SaveLoad.HasNewSaveWaiting)
            {
                viewer.Load();
            }

            // Handle playback
            if (isPlaying && EditorApplication.timeSinceStartup > previousFrameTime + viewer.timeBetweenFrames)
            {
                viewer.frameIndex = (viewer.frameIndex + 1) % viewer.NumFrames;
                previousFrameTime = (float)EditorApplication.timeSinceStartup;
                SceneView.RepaintAll();

                if (viewer.frameIndex == viewer.NumFrames - 1 && !viewer.loop)
                {
                    isPlaying = false;
                }
            }
        }

        private void OnEnable()
        {
            viewer = target as DebugViewer;
            viewer.Load();

            EditorApplication.update += EditorUpdate;
            Tools.hidden = true;
        }

        private void OnDisable()
        {
            Tools.hidden = false;
            EditorApplication.update -= EditorUpdate;
        }

    }
}