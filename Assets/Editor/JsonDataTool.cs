using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using UnityEditor;
using UnityEngine;

public class JsonDataTool {

    [MenuItem("Assets/CreateJson")]
    private static void CreateJson()
    {
        var ids = Selection.assetGUIDs;
        string path = AssetDatabase.GUIDToAssetPath(ids[0]);
        AudioJson(path);
    }

    private static void AudioJson(string selectedPath)
    {
        if(!selectedPath.EndsWith(Paths.AUDIO_FOLDER))
            return;
        
        DirectoryInfo info = new DirectoryInfo(selectedPath);
        FileInfo[] fileInfos = info.GetFiles("*", SearchOption.AllDirectories);
        
        
        string path = Paths.CONFIG_AUDIO_VOLUME_CONFIG;
        List<AudioVolume> volumes = new List<AudioVolume>();
        
        if (File.Exists(path))
        {
            AudioVolume[] data = JsonMapper.ToObject<AudioVolume[]>(File.ReadAllText(path));
            
           
            foreach (FileInfo fileInfo in fileInfos)
            {
                if(fileInfo.Name.EndsWith(".meta"))
                    continue;
                string name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                var temp = new AudioVolume();
                temp.Name = name;
                temp.Volume = GetVolume(data, name);
                volumes.Add(temp);
            }

        }
        else
        {
            foreach (FileInfo fileInfo in fileInfos)
            {
                if(fileInfo.Name.EndsWith(".meta"))
                    continue;
                string name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                var temp = new AudioVolume();
                temp.Name = name;
                temp.Volume = 0.5f;
                volumes.Add(temp);
            }
        }

        string json = JsonMapper.ToJson(volumes);
        File.WriteAllText(path,json);
        Debug.Log("成功生成AudioVolume配置文件");
        
        AssetDatabase.Refresh();
       
    }
    
    private static double GetVolume(AudioVolume[] data,string key)
    {
        var item = data.Where(u => u.Name == key).FirstOrDefault();
        if (item != null)
        {
            return item.Volume;
        }
        else
        {
            return 0.5f;
        }
    }
}

public class AudioVolume
{
    public string Name { get; set; }
    public double Volume { get; set; }
}
