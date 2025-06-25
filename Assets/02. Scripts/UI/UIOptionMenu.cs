using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 옵션 메뉴의 버튼을 생성하고 텍스트를 설정해주는 클래스.
/// </summary>
public class UIOptionMenu : ITextChanger
{
    //해당 ITextChanger 인터페이스는 UIManager의 List에 추가되지 않음.
    public Button BtnOptionMenu { get; private set; }
    public TextMeshProUGUI TextOptionMenu { get; private set; }
    private ETextInfo eTextInfoOptionMenu = ETextInfo.None;

    public UIOptionMenu(ETextInfo eTextInfo)
    {
        eTextInfoOptionMenu = eTextInfo;
    }


    /// <summary>
    /// 클래스를 처음 생성했을 때 실행할 메서드.
    /// </summary>
    public void Init()
    {
        BtnOptionMenu = GameObject.Instantiate(
            ManagerHub.Instance.ResourceManager.Btn_OptionMenu,
            ManagerHub.Instance.UIManager.GetDictUIBaseToT<UIOptionPanel>().TrOptionMenuContent);
        TextOptionMenu = BtnOptionMenu.GetComponentInChildren<TextMeshProUGUI>();
    }


    /// <summary>
    /// 텍스트의 폰트를 초기화하는 메서드
    /// </summary>
    public void InitFont()
    {
        TextOptionMenu.font = ManagerHub.Instance.TextManager.nowFont;
    }


    /// <summary>
    /// 언어 설정에 맞춰 텍스트를 초기화하는 메서드
    /// </summary>
    public void InitText()
    {
        TextOptionMenu.text = ManagerHub.Instance.TextManager[eTextInfoOptionMenu];
    }
}
