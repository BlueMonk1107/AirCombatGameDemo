using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        List<AudioVolume> volumes = new List<AudioVolume>();
        foreach (FileInfo fileInfo in fileInfos)
        {
            if(fileInfo.Name.EndsWith(".meta"))
                continue;
            string name = Path.GetFileNameWithoutExtension(fileInfo.Name);
            var temp = new AudioVolume();
            temp.Name = name;
            temp.Volume = 0.5;
            volumes.Add(temp);
        }
        
        string json = JsonMapper.ToJson(volumes);
        string path = Paths.CONFIG_AUDIO_VOLUME_CONFIG;

        if (File.Exists(path))
        {
            if (EditorUtility.DisplayDialog("警告", "是否覆盖AudioVolume配置文件", "确认", "取消"))
            {
                File.WriteAllText(path,json);
                Debug.Log("成功生成AudioVolume配置文件");
            }
        }
        else
        {
            File.WriteAllText(path,json);
            Debug.Log("成功生成AudioVolume配置文件");
        }
        
        AssetDatabase.Refresh();
       
    }
}

public class AudioVolume
{
    public string Name { get; set; }
    public double Volume { get; set; }
}
