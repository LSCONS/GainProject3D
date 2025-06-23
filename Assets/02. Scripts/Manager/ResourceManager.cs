using System.Threading.Tasks;
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
        UIPopupSelect = await LoadResource<UIPopupSelect>("UIPopupSelect");
        Btn_SelectLanguage = await LoadResource<Button>("Btn_SelectLanguage");
        UIOptionOpen = await LoadResource<UIOptionOpen>("Img_OptionOpen");
        UIOptionPanel = await LoadResource<UIOptionPanel>("Img_OptionPanel");
        return;
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<T> LoadResource<T>(string adress)
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
}
