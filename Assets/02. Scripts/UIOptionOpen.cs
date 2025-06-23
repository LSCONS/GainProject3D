using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionOpen : UIPermanent, ITextChanger
{
    [field: SerializeField] private Button BtnOptionOpen { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextOptionOpen { get; set; }


    /// <summary>
    /// 프리팹 첫 생성에 실행할 메서드
    /// </summary>
    public override void Init()
    {
        base.Init();
        BtnOptionOpen.onClick.AddListener(() =>
        {
            ManagerHub.Instance.UIManager.ReturnDictUIBaseToT<UIOptionPanel>()?.UIOpen();
        });
        InitText();
    }


    /// <summary>
    /// 설정 언어에 맞춰 텍스트를 변환해주는 메서드
    /// </summary>
    public void InitText()
    {
        TextOptionOpen.text = ManagerHub.Instance.TextManager[ETextInfo.Menu_Options];
    }
}
