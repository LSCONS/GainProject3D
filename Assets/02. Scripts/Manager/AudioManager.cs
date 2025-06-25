using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private AudioClipData NextBGMAudioClipData = null;
    private AudioSource AudioBGM;
    private Queue<AudioSource> QueueAudioSFX;
    private Coroutine coroutineFadeOutBGM = null;
    private Coroutine coroutineFadeInBGM = null;
    private ManagerHub Hub => ManagerHub.Instance;


    public void Awake()
    {
        AudioBGM = GameObject.Instantiate(Hub.ResourceManager.AudioSource, Hub.TrAudioPool);
    }


    /// <summary>
    /// Fade In/Out처리와 함께 BGM을 재생하는 메서드.
    /// </summary>
    /// <param name="eAudioClip">실행할 BGM enum</param>
    public void PlayAudioBGM(EAudioClip eAudioClip)
    {
        AudioClipData audioClipData = GetAudioClipData(eAudioClip);
        if (audioClipData == null) return;
        if (audioClipData.AudioClip == AudioBGM.clip) return;

        NextBGMAudioClipData = audioClipData;

        if (coroutineFadeOutBGM != null) return;
        if (coroutineFadeInBGM != null)
        {
            Hub.StopCoroutine(coroutineFadeInBGM);
            coroutineFadeInBGM = null;
        }
        coroutineFadeOutBGM = Hub.StartCoroutine(PlayAudioFadeOut());
    }


    /// <summary>
    /// SFX 재생해주는 메서드.
    /// </summary>
    public void PlayAudioSFX(EAudioClip eAudioClip)
    {
        AudioClipData data = GetAudioClipData(eAudioClip);
        AudioSource source = GetPoolAudioSource();
        Hub.StartCoroutine(PlaySFXCoroutine(source, data));
    }


    /// <summary>
    /// 점점 오디오 볼륨을 높여가며 BGM을 재생하는 메서드.
    /// </summary>
    private IEnumerator PlayAudioFadeIn()
    {
        AudioBGM.clip = NextBGMAudioClipData.AudioClip;
        AudioBGM.pitch = NextBGMAudioClipData.Pitch;
        AudioBGM.loop = true;
        AudioBGM.volume = 0;
        AudioBGM.Play();
        while (AudioBGM.volume < NextBGMAudioClipData.Volume)
        {
            AudioBGM.volume = Mathf.Min(AudioBGM.volume + Time.deltaTime, NextBGMAudioClipData.Volume);
            yield return null;
        }
        coroutineFadeInBGM = null;
    }


    /// <summary>
    /// SFX용 AudioSource를 재생하고 끝나면 풀에 반환하는 코루틴.
    /// </summary>
    /// <param name="source">풀에서 꺼낸 AudioSource</param>
    /// <param name="data">재생할 AudioClipData</param>
    private IEnumerator PlaySFXCoroutine(AudioSource source, AudioClipData data)
    {
        source.clip = data.AudioClip;
        source.pitch = data.Pitch;
        source.volume = data.Volume;
        source.loop = false;
        source.Play();

        float playTime = data.AudioClip.length / Mathf.Max(data.Pitch, 0.01f);
        yield return new WaitForSeconds(playTime);

        source.Stop();
        source.clip = null;
        QueueAudioSFX.Enqueue(source);
    }


    /// <summary>
    /// 점차 오디오 볼륨을 낮춰가며 BGM을 정지하는 메서드.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAudioFadeOut()
    {
        while (AudioBGM.volume > 0)
        {
            AudioBGM.volume = Mathf.Max(AudioBGM.volume - Time.deltaTime, 0);
            yield return null;
        }
        AudioBGM.Stop();
        coroutineFadeOutBGM = null;
        if (NextBGMAudioClipData != null)
        {
            if (coroutineFadeInBGM != null)             
            {
                Hub.StopCoroutine(coroutineFadeInBGM);
                coroutineFadeInBGM = null;
            }
            coroutineFadeInBGM = Hub.StartCoroutine(PlayAudioFadeIn());
        }
    }


    /// <summary>
    /// Pool에 있는 AudioSource를 하나 반환해주는 메서드.
    /// 없을 경우에는 새로 생성해서 반환.
    /// </summary>
    /// <returns>반환하는 AudioSource</returns>
    private AudioSource GetPoolAudioSource()
    {
        if (QueueAudioSFX.TryDequeue(out AudioSource audio)) return audio;
        return GameObject.Instantiate(Hub.ResourceManager.AudioSource, Hub.TrAudioPool);
    }


    /// <summary>
    /// DictEClipToData에서 EAudioClip에 해당하는 AudioClipData를 반환하는 메서드.
    /// </summary>
    /// <param name="eAudioClip">찾을 오디오의 enum</param>
    /// <returns>반환받을 AudioClipData</returns>
    private AudioClipData GetAudioClipData(EAudioClip eAudioClip)
    {
        if (Hub.ResourceManager.DictEClipToData.TryGetValue(eAudioClip, out AudioClipData audioClipData))
        {
            return audioClipData;
        }
        Debug.LogError($"AudioClipData for {eAudioClip} not found.");
        return null;
    }
}
