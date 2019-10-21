using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMgr : NormalSingleton<ConfigMgr>
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
            _readersDic[path] = reader;
        }

        return reader;
    }
}
