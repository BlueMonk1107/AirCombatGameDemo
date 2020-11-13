using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class GuideGenerateIDTool
{
    private static readonly string BUSSINESS_FOLDER = Application.dataPath + "/Scripts/Guide/Business/";
    private static readonly string GROUP_FOLDER = BUSSINESS_FOLDER + "Group/";
    private static readonly string GUIDE_FOLDER = BUSSINESS_FOLDER + "Guide/";

    private const string GROUP_REPLACE_CONTENT_OLD = "protected override int GroupId { get; }";
    private const string GROUP_REPLACE_CONTENT_NEW = "protected override int GroupId {{ get {{ return {0}; }} }}";
    private const string GUIDE_REPLACE_CONTENT_OLD = "protected override int GuideId { get; }";
    private const string GUIDE_REPLACE_CONTENT_NEW = "protected override int GuideId {{ get {{ return {0}; }} }}";

    private static string ID_DATA_KEY = "SaveIdLastOne";

    [MenuItem("Tools/生成新手引导部分ID代码")]
    public static void GenerateId()
    {
        GenerateGroupId();
        GenerateGuideId();
        Debug.Log("完成ID替换");
    }

    private static int IdData
    {
        get { return PlayerPrefs.GetInt(ID_DATA_KEY, 0); }
        set { PlayerPrefs.SetInt(ID_DATA_KEY, value); }
    }

    private static void GenerateGroupId()
    {
        ReplaceId(GROUP_FOLDER, GROUP_REPLACE_CONTENT_OLD, GROUP_REPLACE_CONTENT_NEW);
    }
    
    private static void GenerateGuideId()
    {
        ReplaceId(GUIDE_FOLDER, GUIDE_REPLACE_CONTENT_OLD, GUIDE_REPLACE_CONTENT_NEW);
    }

    private static void ReplaceId(string directoryPath,string oldContent,string newContent)
    {
        if (JudgeDirectory(directoryPath))
        {
            string filePath = "";
            string content = "";
            List<string> paths = new List<string>();
            GetFilePaths(directoryPath, paths);
            foreach (string file in paths)
            {
                if (file.Contains(".cs") && !file.Contains(".meta"))
                {
                    filePath = file;
                    content = File.ReadAllText(filePath);
                    content = Regex.Replace(content, oldContent, (match)=>Repalce(match,oldContent,newContent));
                    File.WriteAllText(filePath, content);
                }
            }
        }
        else
        {
            Debug.LogError("当前脚本文件夹不存在，路径：" + directoryPath);
        } 
    }

    private static List<string> GetFilePaths(string rootPath,List<string> paths)
    {
        foreach (string file in Directory.GetFiles(rootPath,"*.cs"))
        {
            paths.Add(file);
        }

        foreach (string directory in Directory.GetDirectories(rootPath))
        {
            GetFilePaths(directory, paths);
        }

        return paths;
    }

    private static string Repalce(Match match,string oldContent,string newContent)
    {
        int id = IdData;
        IdData++;
        return match.Groups[0].Value.Replace(oldContent, string.Format(newContent, id));
    }

    private static bool JudgeDirectory(string path)
    {
        return Directory.Exists(path);
    }
}