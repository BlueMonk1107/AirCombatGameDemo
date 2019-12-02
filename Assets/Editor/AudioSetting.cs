using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioSetting : AssetPostprocessor {

	private void OnPostprocessAudio(AudioClip clip)
	{
		var audio = (AudioImporter) assetImporter;
		AudioImporterSampleSettings setting = new AudioImporterSampleSettings();
		if (clip.length < 1)
		{
			setting.loadType = AudioClipLoadType.DecompressOnLoad;
		}
		else
		{
			setting.loadType = AudioClipLoadType.Streaming;
		}

		audio.preloadAudioData = false;
		audio.defaultSampleSettings = setting;
	}
}
