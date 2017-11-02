using UnityEngine;
using UnityEditor;

namespace VisualDebugging.Example {
[CustomEditor(typeof(TestExample))]
public class TestExampleEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Run"))
        {
            (target as TestExample).Run();
        }
    }
}
}