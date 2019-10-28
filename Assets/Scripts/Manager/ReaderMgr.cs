using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaderMgr : NormalSingleton<ReaderMgr>
{
    private Dictionary<string, IReader> _readersDic = new Dictionary<string, IReader>();

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
            LoadMgr.Single.LoadConfig(path,(data)=>reader.SetData(data));
            _readersDic[path] = reader;
        }

        return reader;
    }
}
