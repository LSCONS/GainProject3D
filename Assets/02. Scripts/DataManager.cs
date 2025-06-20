using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager
{
    public UIPopupSelect UIPopupSelect { get; private set; }

    public async void Awake()
    {
        UIPopupSelect = await LoadData<UIPopupSelect>("UIPopupSelect");
    }

    private async Task<T> LoadData<T>(string adress)
    {
        T data = default;
        var temp = Addressables.LoadAssetAsync<GameObject>(adress);
        await temp.Task;
        if(temp.Status == AsyncOperationStatus.Succeeded)
        {
            //if(typeof(T) != typeof(GameObject))
            data = temp.Result.GetComponent<T>();
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return data;
    }
}
