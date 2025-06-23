using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIOptionLanguageSetting : ITextChanger
{
    [field: SerializeField] private TextMeshProUGUI TextSelectLanguage { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextSettingLanguage { get; set; }
    [field: SerializeField] private Button BtnSelectLanguage { get; set; }
    [field: SerializeField] private Transform TrELanguageContent { get; set; }
    [field: SerializeField] private GameObject ObjELanguageScrollView { get; set; }
    private UIOptionLanguageView selectLanguageView;
    private List<UIOptionLanguageView> ListLanguageViews { get; set; } = new();
    private UIOptionPanel uiOptionPanel => ManagerHub.Instance.UIManager.ReturnDictUIBaseToT<UIOptionPanel>();


    public void OnEnable()
    {
        selectLanguageView?.SetTextELanguage(ManagerHub.Instance.DataManager.NowLanguage);
    }


    /// <summary>
    /// 첫 프리팹 생성 시 실행할 초기화 메서드
    /// </summary>
    public void Init()
    {
        CreateLanguageChangeView();
    }


    /// <summary>
    /// 텍스트를 언어에 맞춰 초기화하는 메서드
    /// </summary>
    public void InitText()
    {
        TextSettingLanguage.text = ManagerHub.Instance.TextManager[ETextInfo.Option_LanguageText];
    }


    /// <summary>
    /// 버튼에 이벤트를 등록하는 메서드
    /// </summary>
    public void AddBtnEvent()
    {
        BtnSelectLanguage.onClick.AddListener(() => ObjELanguageScrollView.SetActive(true));

        uiOptionPanel.AddBtnApplyEvent(selectLanguageView.SetPrefELanguage);
        uiOptionPanel.AddBtnApplyEvent(() => ObjELanguageScrollView.SetActive(false));

        uiOptionPanel.AddBtnCancelEvent(() => ObjELanguageScrollView.SetActive(false));
    }


    /// <summary>
    /// 언어 세팅이 바뀌었는지 확인하고 반환하는 메서드
    /// </summary>
    /// <returns>바뀌었다면 true, 그대로면 false</returns>
    public bool IsChangeLanguage()
    {
        bool isChange = selectLanguageView.GetELanguage() != ManagerHub.Instance.DataManager.NowLanguage;
        return isChange;
    }


    /// <summary>
    /// LanguageChangeView클래스들을 생성해주는 메서드
    /// </summary>
    private void CreateLanguageChangeView()
    {
        selectLanguageView = new();
        selectLanguageView.Init(BtnSelectLanguage, TextSelectLanguage, ManagerHub.Instance.DataManager.NowLanguage);

        foreach (ELanguage eLanguage in Enum.GetValues(typeof(ELanguage)))
        {
            UIOptionLanguageView view = new UIOptionLanguageView();
            Button BtnLanguage = GameObject.Instantiate(ManagerHub.Instance.ResourceManager.Btn_SelectLanguage, TrELanguageContent);
            TextMeshProUGUI TextLanguage = BtnLanguage.GetComponentInChildren<TextMeshProUGUI>();
            view.Init(BtnLanguage, TextLanguage, eLanguage, ObjELanguageScrollView, selectLanguageView);
            ListLanguageViews.Add(view);
        }
    }
}
