using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ResourceManager
{
    public UIPopupSelect UIPopupSelect { get; private set; }
    public Button Btn_SelectLanguage { get; private set; }
    public UIOptionOpen UIOptionOpen { get; private set; }
    public UIOptionPanel UIOptionPanel { get; private set; }

    public async Task Awake()
    {
        UIPopupSelect       = await LoadObjectResource<UIPopupSelect>("UIPopupSelect");
        Btn_SelectLanguage  = await LoadObjectResource<Button>("Btn_SelectLanguage");
        UIOptionOpen        = await LoadObjectResource<UIOptionOpen>("Img_OptionOpen");
        UIOptionPanel       = await LoadObjectResource<UIOptionPanel>("Img_OptionPanel");
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
}
