using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using UnityEditor;
using UnityEngine;

public class JsonDataTool
{
    [MenuItem("Assets/CreateJson")]
    private static void CreateJson()
    {
        var ids = Selection.assetGUIDs;
        var path = AssetDatabase.GUIDToAssetPath(ids[0]);
        AudioJson(path);
    }

    private static void AudioJson(string selectedPath)
    {
        if (!selectedPath.EndsWith(Paths.AUDIO_FOLDER))
            return;

        var info = new DirectoryInfo(selectedPath);
        var fileInfos = info.GetFiles("*", SearchOption.AllDirectories);


        var path = Paths.CONFIG_AUDIO_VOLUME_CONFIG;
        var volumes = new List<AudioVolume>();

        if (File.Exists(path))
        {
            var data = JsonMapper.ToObject<AudioVolume[]>(File.ReadAllText(path));


            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Name.EndsWith(".meta"))
                    continue;
                var name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                var temp = new AudioVolume();
                temp.Name = name;
                temp.Volume = GetVolume(data, name);
                volumes.Add(temp);
            }
        }
        else
        {
            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Name.EndsWith(".meta"))
                    continue;
                var name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                var temp = new AudioVolume();
                temp.Name = name;
                temp.Volume = 0.5f;
                volumes.Add(temp);
            }
        }

        var json = JsonMapper.ToJson(volumes);
        File.WriteAllText(path, json);
        Debug.Log("成功生成AudioVolume配置文件");

        AssetDatabase.Refresh();
    }

    private static double GetVolume(AudioVolume[] data, string key)
    {
        var item = data.Where(u => u.Name == key).FirstOrDefault();
        if (item != null)
            return item.Volume;
        return 0.5f;
    }
}

public class AudioVolume
{
    public string Name { get; set; }
    public double Volume { get; set; }
}