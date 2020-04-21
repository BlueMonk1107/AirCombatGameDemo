using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class TextureSetting : AssetPostprocessor
{
    private static FolderData _playerData;

    private void OnPreprocessTexture()
    {
        NamingConventions();
        SetTexturePara();
    }

    private void NamingConventions()
    {
        PlayerNaming();
    }

    private void PlayerNaming()
    {
        if (assetPath.Contains(Paths.PICTURE_PLAYER_PICTURE_FOLDER))
        {
            var name = Path.GetFileNameWithoutExtension(Path.GetFileName(assetPath));

            var pattern = "^[0-9]+_[0-9]+$";

            var result = Regex.Match(name, pattern);
            if (result.Value == "")
            {
                if (_playerData == null)
                {
                    _playerData = new FolderData();
                    _playerData.FolderPath = Paths.PICTURE_PLAYER_PICTURE_FOLDER;
                    _playerData.NameTip = "0_0";
                }

                Debug.LogError("当前导入资源名称错误，名称为：" + name);
                NameMgrWindowData.Add(_playerData, assetPath);
                NameMgrWindow.ShowWindow();
            }
        }
    }

    private void SetTexturePara()
    {
        var importer = (TextureImporter) assetImporter;
        importer.textureType = TextureImporterType.Sprite;
        importer.mipmapEnabled = false;
    }
}