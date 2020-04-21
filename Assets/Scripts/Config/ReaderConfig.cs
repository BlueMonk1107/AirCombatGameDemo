using System;
using System.Collections.Generic;
using UnityEngine;

public class ReaderConfig
{
    private static readonly Dictionary<string, Func<IReader>> readersDic = new Dictionary<string, Func<IReader>>
    {
        {".json", () => new JsonReader()}
    };

    public static IReader GetReader(string path)
    {
        foreach (var pair in readersDic)
            if (path.Contains(pair.Key))
                return pair.Value();

        Debug.LogError("未找到对应文件的读取器，文件路径：" + path);
        return null;
    }
}