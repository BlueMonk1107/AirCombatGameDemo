using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaderConfig
{

	private static readonly Dictionary<string, Func<string,IReader>> READER_DIC = new Dictionary<string, Func<string,IReader>>()
	{
		{".json", (path) => new JsonReader(path)},
	};

	public static IReader GetReader(string path)
	{
		foreach (var pair in READER_DIC)
		{
			if (path.Contains(pair.Key))
			{
				return pair.Value(path);
			}
		}

		Debug.LogError("未找到对应文件的读取器，文件路径："+path);
		return null;
	}
}
