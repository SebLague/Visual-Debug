
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace VisualDebugging
{
    public class SceneArtist
    {
        public Color colour;
        public string artistType = typeof(SceneArtist).ToString();

        public SceneArtist(Color colour)
        {
            this.colour = colour;
        }

        public virtual void Draw()
        {
#if UNITY_EDITOR
            Handles.color = colour;
#endif
        }
    }
}
