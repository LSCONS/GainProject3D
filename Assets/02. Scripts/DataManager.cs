using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : Singleton<DataManager>
{
    protected override void Awake()
    {
        base.Awake();

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
            Debug.LogError($"��巹���� �ε� ����: {temp.OperationException?.Message}");
        }
        return data;
    }
}
