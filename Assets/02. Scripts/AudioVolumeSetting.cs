using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeSetting
{
    public AudioMixerGroup groupBGM { get; private set; }
    public AudioMixerGroup groupSFX { get; private set; }
    public AudioMixerGroup groupVoice { get; private set; }

    public void Init()
    {
        AudioMixer audioMixer = ManagerHub.Instance.ResourceManager.AudioMixer;
        groupBGM = audioMixer.FindMatchingGroups($"{ReadonlyData.AudioGroupName_Master}/{ReadonlyData.AudioGroupName_BGM}")[0];
        groupSFX = audioMixer.FindMatchingGroups($"{ReadonlyData.AudioGroupName_Master}/{ReadonlyData.AudioGroupName_SFX}")[0];
        groupVoice = audioMixer.FindMatchingGroups($"{ReadonlyData.AudioGroupName_Master}/{ReadonlyData.AudioGroupName_Voice}")[0];
    }

}
