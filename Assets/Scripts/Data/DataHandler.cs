using JsonExtended;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace FileData
{
    public class DataHandler
    {
        #region Variable & Constructor related
        private string datapath = "";
        public DataHandler(string datapath)
        {
            this.datapath = datapath;
        }
        #endregion
        public T LoadText<T>(string FolderPath, string FileName)
        {
            string textPath = Path.Combine(datapath, FolderPath, FileName);
            string data2load = "";
            if (!File.Exists(textPath))return default;
            try
            {
                using (StreamReader reader = new StreamReader(textPath))
                {
                    data2load = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when trying to get all Path from file: " + textPath + "\n" + ex);
            }
            Debug.Log(data2load);
            return JsonUtility.FromJson<T>(data2load);
        }
        public void SaveText<T>(T data, string FolderName, string FileName)
        {
            string fullPath = Path.Combine(datapath, FolderName, FileName);
            Debug.Log(fullPath);
            try
            {
                if (!File.Exists(fullPath)) Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string data2store = JsonUtility.ToJson(data);
                using (StreamWriter writer = new StreamWriter(fullPath, false))
                {
                    writer.Write(data2store);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + ex);
            }
        }
    }

}


