using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace VisualDebugging
{
    [CustomEditor(typeof(VisualDebugger))]
    public class VisualDebugEditor : Editor
    {

        VisualDebugger visualDebugger;
        bool isPlaying;
        float previousFrameTime;

        public override void OnInspectorGUI()
        {
            Frame currentFrame = visualDebugger.CurrentFrame;

            GUILayout.Space(5);
            GUILayout.Label(string.Format("Frame {0} of {1}", visualDebugger.frameIndex + 1, visualDebugger.frames.Count));

            GUI.enabled = !isPlaying;
            visualDebugger.frameIndex = EditorGUILayout.IntSlider(visualDebugger.frameIndex + 1, 0, visualDebugger.frames.Count) - 1;
            GUILayout.Space(6);

            GUI.enabled = true;
            GUILayout.BeginHorizontal();
            string playPauseButtonText = (isPlaying) ? "Pause" : "Play";
            if (GUILayout.Button(playPauseButtonText))
            {
                isPlaying = !isPlaying;
            }

            float defaultLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 35;
            EditorGUIUtility.fieldWidth = 20;
            visualDebugger.timeBetweenFrames = EditorGUILayout.FloatField("Time", visualDebugger.timeBetweenFrames);
            visualDebugger.loop = EditorGUILayout.Toggle("Loop", visualDebugger.loop);
            EditorGUIUtility.labelWidth = defaultLabelWidth;
            GUILayout.EndHorizontal();

            GUI.enabled = !isPlaying;
            if (GUILayout.Button("Next Frame"))
            {
                if (visualDebugger.frameIndex < visualDebugger.frames.Count - 1)
                {
                    visualDebugger.frameIndex++;
                }
            }

            if (GUILayout.Button("Previous Frame"))
            {
                if (visualDebugger.frameIndex >= 0)
                {
                    visualDebugger.frameIndex--;
                }
            }

            if (GUILayout.Button("Clear"))
            {
                visualDebugger.frameIndex = -1;
                visualDebugger.Clear();
            }

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
            if (guiEvent.type == EventType.Repaint)
            {
                for (int i = 0; i <= visualDebugger.frameIndex; i++)
                {
                    if (i < visualDebugger.frames.Count)
                    {
                        visualDebugger.frames[i].Draw(visualDebugger.frameIndex);

                    }
                }
            }
        }

        void EditorUpdate()
        {
            if (isPlaying && EditorApplication.timeSinceStartup > previousFrameTime + visualDebugger.timeBetweenFrames)
            {
                visualDebugger.frameIndex = (visualDebugger.frameIndex + 1) % visualDebugger.frames.Count;
                previousFrameTime = (float)EditorApplication.timeSinceStartup;
                SceneView.RepaintAll();

                if (visualDebugger.frameIndex == visualDebugger.frames.Count - 1 && !visualDebugger.loop)
                {
                    isPlaying = false;
                }
            }
        }

        private void OnEnable()
        {
            visualDebugger = target as VisualDebugger;

            if (SaveLoad.HasSaveFile())
            {
                visualDebugger.frames = SaveLoad.Load().ToList();
            }

            if (visualDebugger.frameIndex >= visualDebugger.frames.Count)
            {
                visualDebugger.frameIndex = -1;
            }
            EditorApplication.update += EditorUpdate;
            Tools.hidden = true;
        }



        private void OnDisable()
        {
            Tools.hidden = false;
            EditorApplication.update -= EditorUpdate;

            if (visualDebugger.requiresSave)
            {
                SaveLoad.Save(visualDebugger.frames.ToArray());
            }
        }

    }
}