using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/Audio")]
public class AudioConfigSO : ScriptableObject
{
    [SerializeField] private List<AudioEvent> _events = new();

    public IReadOnlyList<AudioEvent> Events => _events;

    public AudioEvent GetEvent(string key)
    {
        return _events.Find(audio => audio.EventKey == key);
    }

    [Serializable]
    public class AudioEvent
    {
        public string EventKey = "Click";                  
        public AudioClip Clip;    
        [Range(0f,1f)] public float Volume = 1f;
        [Range(0.1f,3f)] public float Pitch = 1f;
        public bool Loop = false;
    }
}