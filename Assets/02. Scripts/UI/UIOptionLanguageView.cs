using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionLanguageView
{
    private Button BtnLanguage;
    private TextMeshProUGUI TextLanguage;
    private ELanguage ELanguage;


    /// <summary>
    /// 첫 프리팹 생성 시 실행할 초기화 메서드
    /// </summary>
    /// <param name="btnLanguage">버튼 오브젝트</param>
    /// <param name="textLanguage">텍스트 오브젝트</param>
    /// <param name="eLanguage">해당 버튼/텍스트에 담을 언어 enum</param>
    public void Init(Button btnLanguage, TextMeshProUGUI textLanguage, ELanguage eLanguage, GameObject ObjELanguageScrollView = null, UIOptionLanguageView selectLanguageChangeView = null)
    {
        BtnLanguage = btnLanguage;
        TextLanguage = textLanguage;
        SetTextELanguage(eLanguage);
        if (ObjELanguageScrollView == null) return;
        BtnLanguage.onClick.RemoveAllListeners();
        BtnLanguage.onClick.AddListener(() =>
        {
            ObjELanguageScrollView.SetActive(false);
            selectLanguageChangeView.SetTextELanguage(eLanguage);
            ManagerHub.Instance.UIManager.ReturnDictUIBaseToT<UIOptionPanel>().IsChangeSetting();
        });
    }


    /// <summary>
    /// 텍스트에 언어 enum을 적용하는 메서드
    /// </summary>
    /// <param name="eLanguage">적용할 언어 enum</param>
    public void SetTextELanguage(ELanguage eLanguage)
    {
        ELanguage = eLanguage;
        TextLanguage.text = eLanguage.ToString();
    }


    /// <summary>
    /// 실제 플레이에 해당 언어 enum을 적용시키는 메서드
    /// </summary>
    public void SetPrefELanguage()
    {
        ManagerHub.Instance.DataManager.SetNowLanguage(ELanguage);
    }


    /// <summary>
    /// 현재 View에 설정된 ELanguage를 반환하는 메서드
    /// </summary>
    public ELanguage GetELanguage()
    {
        return ELanguage;
    }
}
