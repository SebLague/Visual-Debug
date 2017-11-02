using UnityEngine;
using System.Collections.Generic;

namespace VisualDebugging.Internal
{
    [System.Serializable]
    public class Frame
    {
        public string description;

        [SerializeField] bool keepInBackground; // should this frame be erased before drawing the next?
        [SerializeField] int myFrameIndex;
        [System.NonSerialized] public List<SceneArtist> artists;
       

        public Frame(string description, bool dontErase, int frameIndex)
        {
            this.description = description;
            this.keepInBackground = dontErase;
            this.myFrameIndex = frameIndex;
        }

        public void Draw(int currentFrameIndex)
        {
            bool isCurrentFrame = currentFrameIndex == myFrameIndex;
            bool showFrame = isCurrentFrame || (keepInBackground && currentFrameIndex > myFrameIndex);
            if (showFrame)
            {
                if (artists != null)
                {
                    foreach (SceneArtist artist in artists)
                    {
                        if (isCurrentFrame || artist.showWhenInBackground)
                        {
                            artist.Draw(isCurrentFrame);
                        }
                    }
                }

            }
        }

        public void AddArtist(SceneArtist artist)
        {
            if (artists == null)
            {
                artists = new List<SceneArtist>();
            }
            artists.Add(artist);
        }
    }
}