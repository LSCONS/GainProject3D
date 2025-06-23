using TMPro;
using UnityEngine;

public class LoadingManager : MonoBehaviour, ITextChanger
{
    [field: SerializeField] private TextMeshProUGUI TextLoading {  get; set; }

    private void Awake()
    {
        InitFont();
        InitText();
    }


    public void InitFont()
    {
        TextLoading.font = ManagerHub.Instance.TextManager.nowFont;
    }

    public void InitText()
    {
        TextLoading.text = ManagerHub.Instance.TextManager[ETextInfo.Loading_IsLoading];
    }
}
