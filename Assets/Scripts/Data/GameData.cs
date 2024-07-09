using System;
using System.Collections.Generic;

namespace FileData
{
    public interface ISaveFolder<T>
    {
        public FolderInfo Folder { get; set; }
        public string Filename { get; set; }
        public void Load(T data);
        public void Save(ref T data);
    }

    [Serializable]
    public class PresetData { 
        public PresetData()
        {
            indexes = new();
        }
        public List<int> indexes;
    }
}
