using UnityEngine;
using System.IO;
using System.Linq;

namespace VisualDebugging.Internal
{

    /*
     * Handles saving/loading of Frame arrays.
     */
    
    public static class SaveLoad
    {

        public static bool HasNewSaveWaiting { get; private set; }

        public static void Save(Frame[] frames)
        {
			HasNewSaveWaiting = true;

            SaveData saveData = new SaveData(frames);
            string saveString = JsonUtility.ToJson(saveData);

            StreamWriter writer = new StreamWriter(SavePath, false);
            writer.Write(saveString);
            writer.Close();
        }

        public static bool HasSaveFile()
        {
            return File.Exists(SavePath);
        }

        public static Frame[] Load()
        {
            HasNewSaveWaiting = false;

            StreamReader reader = new StreamReader(SavePath);
            string saveString = reader.ReadToEnd();
            reader.Close();

            SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);
            saveData.ProcessLoadedData();

            return saveData.frameSaveData.Select(f => f.frame).ToArray();

        }

        static string SavePath
        {
            get
            {
                string folderPath = Application.persistentDataPath + "/VisualDebugSaveData";
                Directory.CreateDirectory(folderPath); // create folder (if it doesn't already exist)

                string fileName = "VisualDebugData.txt";
                return folderPath + "/" + fileName;
            }
        }

        [System.Serializable]
        public class FrameSaveData
        {
            public Frame frame;
            public string[] artistJsonStrings;

            public FrameSaveData(Frame frame)
            {
                this.frame = frame;
                if (frame.artists != null)
                {
                    artistJsonStrings = new string[frame.artists.Count];
                    for (int i = 0; i < frame.artists.Count; i++)
                    {
                        artistJsonStrings[i] = JsonUtility.ToJson(frame.artists[i]);
                    }
                }
            }

        }


        [System.Serializable]
        public class SaveData
        {
            public FrameSaveData[] frameSaveData;

            public SaveData(Frame[] frames)
            {
                frameSaveData = frames.Select(f => new FrameSaveData(f)).ToArray();
            }

            public void ProcessLoadedData()
            {
                foreach (FrameSaveData f in frameSaveData)
                {
                    foreach (string artistSaveString in f.artistJsonStrings)
                    {
                        SceneArtist baseArtist = JsonUtility.FromJson<SceneArtist>(artistSaveString);

                        if (!string.IsNullOrEmpty(baseArtist.artistType) && System.Type.GetType(baseArtist.artistType) != null)
                        {
                            SceneArtist artist = JsonUtility.FromJson(artistSaveString, System.Type.GetType(baseArtist.artistType)) as SceneArtist;
                            f.frame.AddArtist(artist);
                        }
                    }
                }


            }
        }
    }
}