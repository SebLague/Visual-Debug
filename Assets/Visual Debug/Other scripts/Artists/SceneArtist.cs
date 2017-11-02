using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualDebugging.Internal
{
	public class SceneArtist
	{

		public string artistType;
		[SerializeField] protected Color activeDrawColour;
		[SerializeField] protected Color inactiveDrawColour;
        public bool showWhenInBackground = true;

		public void SetColour(Color activeDrawColour, Color backgroundDrawColour)
		{
			this.activeDrawColour = activeDrawColour;
			this.inactiveDrawColour = backgroundDrawColour;
		}


		public virtual void Draw(bool isActive)
		{
#if UNITY_EDITOR
			Handles.color = (isActive) ? activeDrawColour : inactiveDrawColour;
#endif
		}
	}
}