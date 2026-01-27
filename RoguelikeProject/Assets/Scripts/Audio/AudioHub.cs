using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHub : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int sfxPoolSize = 8;
    [SerializeField] private bool stealIfFull = true;

    [Header("Refs")]
    [SerializeField] private AudioConfigSO _audioConfigSO;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _sfxGroup;
    [SerializeField] private AudioMixerGroup _musicGroup;

    [Header("Debug")]
    [SerializeField] private bool VerboseLogs = true;
    
    private readonly List<AudioSource> _pool = new();
    private readonly Dictionary<AudioSource, int> _indexBySource = new();
    private int _stealIndex;

    [Conditional("UNITY_EDITOR")] private void Log(string m){ if(VerboseLogs) UnityEngine.Debug.Log($"[AudioHub {Time.realtimeSinceStartup:0.000}] {m}"); }
    [Conditional("UNITY_EDITOR")] private void Warn(string m){ if(VerboseLogs) UnityEngine.Debug.LogWarning($"[AudioHub {Time.realtimeSinceStartup:0.000}] {m}"); }

    private void Awake()
    {
        if (_musicSource == null)
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.playOnAwake = false;
            _musicSource.loop = false;
        }
        if (_sfxGroup == null && _audioMixer != null)
        {
            var g = _audioMixer.FindMatchingGroups("SFX");
            if (g != null && g.Length > 0) _sfxGroup = g[0];
        }
        if (_musicGroup == null && _audioMixer != null)
        {
            var g = _audioMixer.FindMatchingGroups("Music");
            if (g != null && g.Length > 0) _musicGroup = g[0];
        }
    }
    

    public void Init()
    {
        _pool.Clear(); _indexBySource.Clear();

        int size = Mathf.Max(1, sfxPoolSize);
        for (int i = 0; i < size; i++)
        {
            var s = gameObject.AddComponent<AudioSource>();
            s.playOnAwake = false;
            s.loop = false;
            s.spatialBlend = 0f;
            s.outputAudioMixerGroup = _sfxGroup;
            _pool.Add(s);
            _indexBySource[s] = i;
        }
        DumpMixer();
        DumpMusicSource("Init");
    }

    private AudioSource GetSrc()
    {
        for (int i = 0; i < _pool.Count; i++)
            if (!_pool[i].isPlaying) return _pool[i];

        if (!stealIfFull) { Warn("GetSrc: no free channels"); return null; }
        int idx = (_stealIndex++) % _pool.Count;
        var src = _pool[idx];
        src.Stop();
        return src;
    }

    private static float DbToLin(float db) => Mathf.Pow(10f, db / 20f);

    public void SetMusicVolume(float v)
    {
        if (_audioMixer == null) return;
        float clamped = Mathf.Clamp(v, 0.0001f, 1f);
        float db = Mathf.Log10(clamped) * 20f;
        _audioMixer.SetFloat("MusicVolume", db);
    }

    public void SetSFXVolume(float v)
    {
        if (_audioMixer == null) return;
        float clamped = Mathf.Clamp(v, 0.0001f, 1f);
        float db = Mathf.Log10(clamped) * 20f;
        _audioMixer.SetFloat("SFXVolume", db);
    }

    public void PlaySFX(string key)
    {
        AudioConfigSO.AudioEvent audioEvent = _audioConfigSO.GetEvent(key);
        if (audioEvent == null) { Warn("PlaySFX: Event null: " + key); return; }

        var src = GetSrc(); if (src == null) return;
        float pitch = Mathf.Clamp(audioEvent.Pitch, .1f, 3f);
        float vol   = Mathf.Clamp01(audioEvent.Volume);
        if (src.outputAudioMixerGroup == null) src.outputAudioMixerGroup = _sfxGroup;
        
        src.pitch = pitch;
        src.PlayOneShot(audioEvent.Clip, vol);
    }

    public void PlayMusic(string key)
    {
        AudioConfigSO.AudioEvent audioEvent = _audioConfigSO.GetEvent(key);

        _musicSource.loop   = audioEvent.Loop;
        _musicSource.pitch  = Mathf.Clamp(audioEvent.Pitch, .1f, 3f);
        _musicSource.volume = Mathf.Clamp01(audioEvent.Volume);
        _musicSource.clip   = audioEvent.Clip;
        if (_musicSource.outputAudioMixerGroup == null && _musicGroup != null)
            _musicSource.outputAudioMixerGroup = _musicGroup;
        
        _musicSource.Stop();
        _musicSource.Play();
        DumpMusicSource("AfterPlay");
    }

    private void DumpMixer()
    {
        if (_audioMixer == null) { Log("Mixer: null"); return; }
        bool mh = _audioMixer.GetFloat("MusicVolume", out float mv);
        bool sh = _audioMixer.GetFloat("SFXVolume", out float sv);
        string mvStr = mh ? $"{mv:0.0} dB ({DbToLin(mv):0.000})" : "N/A";
        string svStr = sh ? $"{sv:0.0} dB ({DbToLin(sv):0.000})" : "N/A";
    }

    private void DumpMusicSource(string tag)
    {
        if (_musicSource == null) { Log("MusicSource: null (" + tag + ")"); return; }
        string clipName = _musicSource.clip != null ? _musicSource.clip.name : "null";
        string timeStr  = (_musicSource.clip != null) ? _musicSource.time.ToString("0.000") : "-";
    }
}
