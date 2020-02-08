using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoSingleton<AudioMgr>, IInit
{
    private readonly List<AudioSource> _activeSources = new List<AudioSource>();
    private Action _changeVolume;
    private readonly Dictionary<string, AudioSource> _clipAndSourceMap = new Dictionary<string, AudioSource>();

    private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();
    private readonly float _defaultVolume = 0.5f;
    private readonly List<AudioSource> _inactiveSources = new List<AudioSource>();
    private AudioSource _source;
    private readonly Dictionary<string, float> _volumes = new Dictionary<string, float>();

    public void Init()
    {
        _changeVolume = null;

        InitClips();
        
        _source = gameObject.AddComponent<AudioSource>();

        InitVolume();

        InitListener();
    }

    private void InitClips()
    {
        var clips = LoadMgr.Single.LoadAll<AudioClip>(Paths.AUDIO_FOLDER);

        foreach (var clip in clips)
        {
            _clips.Add(clip.name, clip);
        }
    }

    private void InitVolume()
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_AUDIO_VOLUME_CONFIG);
        var name = "";
        float volume = 0;
        reader.Count(count =>
        {
            for (var i = 0; i < count; i++)
            {
                TaskQueueMgr.Single.AddQueue<string>(() => reader[i][DataKeys.AUDIO_NAME]);
                TaskQueueMgr.Single.AddQueue<float>(() => reader[i][DataKeys.AUDIO_Volume]);
                TaskQueueMgr.Single.Execute(datas => { _volumes.Add((string) datas[0], (float) datas[1]); });
            }

            if (_changeVolume != null)
                _changeVolume();
            _changeVolume = null;
        });
    }

    private void InitListener()
    {
        MessageMgr.Single.AddListener(MsgEvent.EVENT_GAME_PAUSE,PasueAllAudio);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_GAME_CONTINUE,ContinueAllAudio);
    }

    public void PasueAllAudio(object[] paras)
    {
        foreach (var source in _clipAndSourceMap)
        {
            source.Value.Pause();
        }
        _source.Pause();
    }
    
    public void ContinueAllAudio(object[] paras)
    {
        foreach (var source in _clipAndSourceMap)
        {
            source.Value.Play();
        }
        _source.Play();
    }

    public AudioClip GetClip(string name)
    {
        if (_clips.ContainsKey(name)) return _clips[name];

        Debug.LogError("无法找到当前音频，名称：" + name);
        return null;
    }

    private void SetVolume(string name, AudioSource source)
    {
        if (_volumes.Count == 0)
            _changeVolume += () => source.volume = GetVolumeValue(name);
        else
            source.volume = GetVolumeValue(name);
    }

    private float GetVolumeValue(string name)
    {
        if (_volumes.ContainsKey(name))
        {
            return _volumes[name];
        }

        Debug.LogError("配置中没有对应名称的音频，名称：" + name);
        return _defaultVolume;
    }

    public void PlayBG(BGAudio audio)
    {
        _source.clip = GetClip(audio.ToString());
        SetVolume(audio.ToString(), _source);
        _source.loop = true;
        _source.Play();
    }

    public void PlayOnce(string name)
    {
        var clip = GetClip(name);
        _source.PlayOneShot(clip, GetVolumeValue(name));
    }

    public void Play(string name, bool loop = false)
    {
        if(name == "Null")
            return;
        var source = GetSource();
        var clip = GetClip(name);
        if(clip == null)
            return;
        source.clip = clip;
        SetVolume(name, source);
        source.loop = loop;
        source.Play();
        _clipAndSourceMap[name] = source;

        CoroutineMgr.Single.ExecuteOnce(Wait(name, loop));
    }

    private IEnumerator Wait(string name, bool loop)
    {
        var clip = GetClip(name);
        yield return new WaitForSeconds(clip.length);

        if (!loop)
            Stop(name);
    }

    public void Stop(string name)
    {
        if (_clipAndSourceMap.ContainsKey(name))
        {
            var source = _clipAndSourceMap[name];
            source.Stop();
            source.clip = null;
            _activeSources.Remove(source);
            _inactiveSources.Add(source);
            _clipAndSourceMap.Remove(name);
        }
    }

    public void PauseNow(string name)
    {
        if (_clipAndSourceMap.ContainsKey(name))
        {
            var source = _clipAndSourceMap[name];
            source.Pause();
        }
    }

    public void PauseDelay(string name)
    {
        if (_clipAndSourceMap.ContainsKey(name))
        {
            var source = _clipAndSourceMap[name];
            source.loop = false;
        }
    }

    public void Replay(string name, bool loop = true)
    {
        if (_clipAndSourceMap.ContainsKey(name))
        {
            var source = _clipAndSourceMap[name];
            source.loop = loop;
            if(GameStateModel.Single.GameState == GameState.START || GameStateModel.Single.GameState == GameState.CONTINUE)
                source.Play();
        }
    }

    private AudioSource GetSource()
    {
        AudioSource source;
        if (_inactiveSources.Count > 0)
        {
            source = _inactiveSources[0];
            _inactiveSources.Remove(source);
        }
        else
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        _activeSources.Add(source);
        return source;
    }
}