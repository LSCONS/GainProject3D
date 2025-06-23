using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioClipDataSO", menuName = "SO/AudioClipDataSO")]
public class AudioClipDataSO : ScriptableObject
{
    [field: SerializeField] public List<AudioCLipEnumData> ListAudioEnumData { get; private set; } = new();
}


[Serializable]
public class AudioCLipEnumData
{
    [field: SerializeField] public AudioClipData AudioClipData {  get; private set; }
    [field: SerializeField] public EAudioClip EAudioClip { get; private set; } = EAudioClip.None;
}


[Serializable]
public class AudioClipData
{
    [field: SerializeField] public AudioClip AudioClip { get; private set; }
    [field: SerializeField] public float Pitch { get; private set; } = 1;
    [field: SerializeField] public float Volume { get; private set; } = 1;
}


public enum EAudioClip
{
    None = 0,
}
