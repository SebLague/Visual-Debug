using UnityEngine;
using System.Collections.Generic;

namespace VisualDebugging
{
    [System.Serializable]
    public class Frame
    {
        public string description;

        [SerializeField] bool dontErase; // should this frame be erased before drawing the next?
        [SerializeField] int myFrameIndex;
        [System.NonSerialized] public List<SceneArtist> artists;

        public Frame(string description, bool dontErase, int frameIndex)
        {
            this.description = description;
            this.dontErase = dontErase;
            this.myFrameIndex = frameIndex;
        }

        public void Draw(int currentFrameIndex)
        {
            if (currentFrameIndex == myFrameIndex || (dontErase && currentFrameIndex > myFrameIndex))
            {
                if (artists != null)
                {
                    foreach (SceneArtist artist in artists)
                    {
                        artist.Draw();
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