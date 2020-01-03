using System.Collections.Generic;
using UnityEngine;

public class ReaderMgr : NormalSingleton<ReaderMgr>
{
    private readonly Dictionary<string, IReader> _readersDic = new Dictionary<string, IReader>();

    public IReader GetReader(string path)
    {
        IReader reader = null;
        if (_readersDic.ContainsKey(path))
        {
            reader = _readersDic[path];
        }
        else
        {
            reader = ReaderConfig.GetReader(path);
            LoadMgr.Single.LoadConfig(path, data => reader.SetData(data));
            if (reader != null)
                _readersDic[path] = reader;
            else
                Debug.LogError("未获取到对应reader，路径：" + path);
        }

        return reader;
    }
}