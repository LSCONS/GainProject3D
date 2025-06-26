using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeSetting
{
    public AudioMixerGroup GroupBGM { get; private set; }
    public AudioMixerGroup GroupSFX { get; private set; }
    public AudioMixerGroup GroupVoice { get; private set; }

    public void Init()
    {
        AudioMixer    audioMixer = ManagerHub.Instance.ResourceManager.AudioMixer;
        GroupBGM    = audioMixer.FindMatchingGroups($"{ReadonlyData.AudioGroupName_Master}/{ReadonlyData.AudioGroupName_BGM}")[0];
        GroupSFX    = audioMixer.FindMatchingGroups($"{ReadonlyData.AudioGroupName_Master}/{ReadonlyData.AudioGroupName_SFX}")[0];
        GroupVoice  = audioMixer.FindMatchingGroups($"{ReadonlyData.AudioGroupName_Master}/{ReadonlyData.AudioGroupName_Voice}")[0];
    }

}
