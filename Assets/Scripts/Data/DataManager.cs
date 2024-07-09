using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FileData
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;
        private DataHandler handler;
        #region Variables
        //List Folder yg mau dipake
        //private readonly Dictionary<(string,string), datatype> TypeFolder = new();
        private readonly Dictionary<(string, string), PresetData> PresetFolder = new();

        //=============================================================================//
        //List Object yg punya fungsi save
        //private List<ISaveFolder<datatype>> TypeObjects;
        private List<ISaveFolder<PresetData>> PresetObjects;
        #endregion
        public void Start()
        {
            instance = this;
            handler = new(Application.persistentDataPath);
            FindInterfaceObjects(ref PresetObjects);
            LoadGame();
        }
        public void NewGame()
        {
            Debug.Log("new");
        }
        public void LoadGame()
        {
            Load(PresetObjects, PresetFolder, AddFolder<PresetData, ISaveFolder<PresetData>>);
        }
        public void SaveGame()
        {
            GetDatas(PresetObjects, PresetFolder, handler.SaveText);
        }
        private void OnApplicationQuit()
        {
            SaveGame();
        }
        public void FindInterfaceObjects<T>(ref List<T> obj)
        {
            IEnumerable<T> objects = FindObjectsOfType<MonoBehaviour>().OfType<T>();
            obj = new(objects);
        }
        /// <summary>
        /// Load File from folder; add data to Dictionary
        /// </summary>
        /// <typeparam name="T">DataType</typeparam>
        /// <typeparam name="T1">Interface</typeparam>
        /// <param name="Objects">TypeObject</param>
        /// <param name="Folders">TypeFolder</param>
        /// <param name="Add">Function to use</param>
        public void Load<T, T1>(List<T1> Objects, Dictionary<(string, string), T> Folders, Func<(string, string), Dictionary<(string, string), T>, T> Add) where T1 : ISaveFolder<T> where T : new()
        {
            T curr_data = new();
            foreach (T1 data in Objects)
            {
                if (data.Folder == null) { Debug.LogWarning(data.ToString() + "Has not been applied yet"); return; }
                string FolderName = data.Folder.Folder_Name;
                string FileName = data.Filename;
                if (!Folders.ContainsKey((FolderName, FileName))) curr_data = Add((FolderName, FileName), Folders);
                if (curr_data != null) data.Load(curr_data);
            }
        }
        public T AddFolder<T, T1>((string, string) FolderName, Dictionary<(string, string), T> Folders) where T : new()
        {
            Folders.Add(FolderName, handler.LoadText<T>(FolderName.Item1, FolderName.Item2));
            return Folders[FolderName];
        }
        public void GetDatas<T, T1>(List<T1> Objects, Dictionary<(string, string), T> Folders, Action<T, string, string> SaveFunction) where T1 : ISaveFolder<T> where T : new()
        {
            foreach (T1 data in Objects)
            {
                if (data.Folder == null) { Debug.LogWarning(data.ToString() + "Has not been applied yet"); return; }
                string FolderName = data.Folder.Folder_Name;
                string FileName = data.Filename;
                T curr_file = new();

                if (!Folders.ContainsKey((FolderName, FileName))) Debug.LogError((FolderName, FileName) + " Not found");
                curr_file = Folders[(FolderName, FileName)] ?? curr_file;

                data.Save(ref curr_file);
                Folders[(FolderName, FileName)] = curr_file;
            }
            foreach (((string, string) FolderName, T data) in Folders)
            {
                SaveFunction?.Invoke(data, FolderName.Item1, FolderName.Item2);
            }
        }
    }
}


