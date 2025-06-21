using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChanger : MonoBehaviour, ITextChanger
{
    [field : SerializeField] private Button BtnChangeKO {  get; set; }
    [field: SerializeField] private Button BtnChangeEN { get; set; }
    [field: SerializeField] private Button BtnChangeJP { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextKorean { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextEnglish { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextJapan { get; set; }

    private void Start()
    {
        ManagerHub.Instance.UIManager.ListTextChanger.Add(this);
        AddEventBtn(BtnChangeKO, ELanguage.Korean, "Korean");
        AddEventBtn(BtnChangeEN, ELanguage.English, "English");
        AddEventBtn(BtnChangeJP, ELanguage.Japan, "Japan");
        InitText();
    }


    /// <summary>
    /// 버튼의 클릭 이벤트를 등록해주는 메서드
    /// </summary>
    /// <param name="button">이벤트를 등록할 버튼</param>
    /// <param name="eLanguage">등록할 언어</param>
    /// <param name="value">매개변수로 어떤 언어를 선택할건지 출력할 string</param>
    private void AddEventBtn(Button button, ELanguage eLanguage, string value)
    {
        button.onClick.AddListener(() =>
        {
            ManagerHub.Instance.TextManager.AddDictTextStrKeyToObj(ETextStrKey.Language, value);
            ManagerHub.Instance.UIManager.InitUIPopupSelect(
                () => ManagerHub.Instance.UIManager.ChangeLanguage(eLanguage),
                null,
                ManagerHub.Instance.TextManager[ETextInfo.Popup_LanguageChanger_Title],
                ManagerHub.Instance.TextManager[ETextInfo.Popup_LanguageChanger_Description],
                Vector3.up);
        });
    }


    /// <summary>
    /// 설정 언어에 맞춰 텍스트를 변환해주는 메서드
    /// </summary>
    public void InitText()
    {
        TextKorean.text = ManagerHub.Instance.TextManager[ETextInfo.Btn_Changer_KO];
        TextEnglish.text = ManagerHub.Instance.TextManager[ETextInfo.Btn_Changer_EN];
        TextJapan.text = ManagerHub.Instance.TextManager[ETextInfo.Btn_Changer_JP];
    }
}
