using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ResourceManager
{
    public UIPopupSelect UIPopupSelect { get; private set; }
    public Button Btn_SelectLanguage { get; private set; }
    public Button Btn_OptionMenu { get; private set; }
    public UIOptionOpen UIOptionOpen { get; private set; }
    public UIOptionPanel UIOptionPanel { get; private set; }
    public AudioSource AudioSource { get; private set; }
    public AudioMixer AudioMixer { get; private set; }
    public List<IOptionMenu> ListOptionMenu { get; private set; } = new();
    public Dictionary<EAudioClip, AudioClipData> DictEClipToData { get; private set; } = new();

    public async Task Awake()
    {
        UIPopupSelect       = await LoadObjectResource<UIPopupSelect>("UIPopupSelect");
        Btn_SelectLanguage  = await LoadObjectResource<Button>("Btn_SelectLanguage");
        Btn_OptionMenu      = await LoadObjectResource<Button>("Btn_OptionMenu");
        UIOptionOpen        = await LoadObjectResource<UIOptionOpen>("Img_OptionOpen");
        UIOptionPanel       = await LoadObjectResource<UIOptionPanel>("Img_OptionPanel");
        AudioSource         = await LoadObjectResource<AudioSource>("Audio_Prefab");

        AudioMixer          = await LoadTypeResource<AudioMixer>("AudioMixer");

        ListOptionMenu      = await LoadArrayObjectResource<IOptionMenu>("OptionMenu");

        DictEClipToData     = await LoadAudioDictionary("AudioClipSO");
        return;
    }


    /// <summary>
    /// 폰트를 어드레서블 로딩 후 반환하는 메서드
    /// </summary>
    /// <param name="fontName"></param>
    /// <returns></returns>
    public async Task<TMP_FontAsset> LoadFont(ELanguage eLanguage)
    {
        return await LoadTypeResource<TMP_FontAsset>(eLanguage.ToString());
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<T> LoadObjectResource<T>(string adress)
    {
        T data = default;
        var temp = Addressables.LoadAssetAsync<GameObject>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            data = temp.Result.GetComponent<T>();
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return data;
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<List<T>> LoadArrayObjectResource<T>(string adress)
    {
        GameObject[] objArray = default;
        List<T> dataArray = new();
        var temp = Addressables.LoadAssetsAsync<GameObject>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            objArray = temp.Result.ToArray();
            if(typeof(T) == typeof(GameObject))
            {
                dataArray = objArray.Cast<T>().ToList();
            }
            else
            {
                foreach(GameObject obj in objArray)
                {
                    if(obj.TryGetComponent<T>(out T component))
                    {
                        dataArray.Add(component);
                    }
                    else
                    {
                        Debug.LogError($"어드레서블 로딩 실패: {obj.name}에 {typeof(T).Name} 컴포넌트가 없습니다.");
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return dataArray;
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<T> LoadTypeResource<T>(string adress)
    {
        T data = default;
        var temp = Addressables.LoadAssetAsync<T>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            data = temp.Result;
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return data;
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<T[]> LoadTypeResources<T>(string adress)
    {
        T[] data = default;
        var temp = Addressables.LoadAssetsAsync<T>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            data = temp.Result.ToArray();
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return data;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="adress"></param>
    /// <returns></returns>
    private async Task<Dictionary<EAudioClip, AudioClipData>> LoadAudioDictionary(string adress)
    {
        AudioClipDataSO[] data = await LoadTypeResources<AudioClipDataSO>(adress);
        Dictionary<EAudioClip, AudioClipData> result = new();
        foreach (AudioClipDataSO dataSO in data)
        {
            foreach (AudioCLipEnumData audioCLipEnumData in dataSO.ListAudioEnumData)
            {
                if (!result.ContainsKey(audioCLipEnumData.EAudioClip))
                {
                    result[audioCLipEnumData.EAudioClip] = audioCLipEnumData.AudioClipData;
                }
                else
                {
                    Debug.LogError($"동일한 AudioClipEnum이 있습니다. {audioCLipEnumData.EAudioClip.ToString()}");
                }
            }
        }
        return result;
    }
}
