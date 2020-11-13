using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PrefabChangeSetting : AssetPostprocessor  {

    private static void OnPostprocessAllAssets(
        string[] importedAssets, 
        string[] deletedAssets, 
        string[] movedAssets,
        string[] movedFromAssetPaths)  
    {
        foreach (string asset in importedAssets)
        {
            PrefabChange(asset);
        }
    }

    private static void PrefabChange(string asset)
    {
        if(!JudgeExtension(asset,".prefab"))
            return;

        string fileName = Path.GetFileNameWithoutExtension(asset);
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(asset);
        if(prefab == null)
            return;
        string prefabName = prefab.name;
        if (fileName != prefabName)
        {
            prefab.name = fileName;
        }
    }

    private static bool JudgeExtension(string asset,string extension)
    {
        return Path.GetExtension(asset) == extension;
    }
}
