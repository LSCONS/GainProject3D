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
        BtnChangeKO.onClick.AddListener(() =>
        {
            ManagerHub.Instance.UIManager.InitUIPopupSelect(
                () => ManagerHub.Instance.UIManager.ChangeLanguage(ELanguage.Korean),
                null,
                ETextInfo.Popup_LanguageChanger_Title,
                ETextInfo.Popup_LanguageChanger_Description,
                Vector3.up);
        });
        BtnChangeEN.onClick.AddListener(() =>
        {
            ManagerHub.Instance.UIManager.InitUIPopupSelect(
                () => ManagerHub.Instance.UIManager.ChangeLanguage(ELanguage.English),
                null,
                ETextInfo.Popup_LanguageChanger_Title,
                ETextInfo.Popup_LanguageChanger_Description,
                Vector3.up);
        });
        BtnChangeJP.onClick.AddListener(() =>
        {
            ManagerHub.Instance.UIManager.InitUIPopupSelect(
                () => ManagerHub.Instance.UIManager.ChangeLanguage(ELanguage.Japan),
                null,
                ETextInfo.Popup_LanguageChanger_Title,
                ETextInfo.Popup_LanguageChanger_Description,
                Vector3.up);
        });
        InitText();
    }

    public void InitText()
    {
        TextKorean.text = ManagerHub.Instance.TextManager[ETextInfo.Btn_Changer_KO];
        TextEnglish.text = ManagerHub.Instance.TextManager[ETextInfo.Btn_Changer_EN];
        TextJapan.text = ManagerHub.Instance.TextManager[ETextInfo.Btn_Changer_JP];
    }
}
