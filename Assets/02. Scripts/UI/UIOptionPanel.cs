using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionPanel : UIPopup, ITextChanger
{
    [field: SerializeField] private TextMeshProUGUI TextOptionTitle { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextApply { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextCancel { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextSelectLanguage { get; set; }
    [field: SerializeField] private Button BtnOptionApply { get; set; }
    [field: SerializeField] private Button BtnOptionCancel { get; set; }
    [field: SerializeField] private Button BtnSelectLanguage { get; set; }
    [field: SerializeField] private Transform TrELanguageContent { get; set; }
    [field: SerializeField] private GameObject ObjELanguageScrollView { get; set; }
    private LanguageChangeView selectLanguageChangeView;
    private List<LanguageChangeView> LanguageChangeViews { get; set; }


    private void OnEnable()
    {
        selectLanguageChangeView?.SetTextELanguage(ManagerHub.Instance.DataManager.NowLanguage);
    }


    /// <summary>
    /// 첫 프리팹 생성 시 실행할 초기화 메서드
    /// </summary>
    public override void Init()
    {
        base.Init();
        closeScaleVector = Vector3.zero;
        ManagerHub.Instance.UIManager.UIOptionPanel = this;
        AddBtnEvent();
        InitText();
        selectLanguageChangeView = new();
        selectLanguageChangeView.Init(BtnSelectLanguage, TextSelectLanguage, ManagerHub.Instance.DataManager.NowLanguage);

        foreach (ELanguage eLanguage in Enum.GetValues(typeof(ELanguage)))
        {
            LanguageChangeView view = new LanguageChangeView();
            Button BtnLanguage = Instantiate(ManagerHub.Instance.ResourceManager.Btn_SelectLanguage, TrELanguageContent);
            BtnLanguage.onClick.AddListener(() =>
            {
                ObjELanguageScrollView.SetActive(false);
                selectLanguageChangeView.SetTextELanguage(eLanguage);
            });
            TextMeshProUGUI TextLanguage = BtnLanguage.GetComponentInChildren<TextMeshProUGUI>();
            view.Init(BtnLanguage, TextLanguage, eLanguage);
            LanguageChangeViews.Add(view);
        }

        BtnSelectLanguage.onClick.AddListener(() =>
        {
            ObjELanguageScrollView.SetActive(true);
        });

        BtnOptionApply.onClick.AddListener(() =>
        {
            selectLanguageChangeView.SetPrefELanguage();
            ObjELanguageScrollView.SetActive(false);
        });

        BtnOptionCancel.onClick.AddListener(() => 
        {
            ObjELanguageScrollView.SetActive(false);
        });
    }
    

    /// <summary>
    /// 텍스트를 언어에 맞춰 초기화하는 메서드
    /// </summary>
    public void InitText()
    {
        TextOptionTitle.text = ManagerHub.Instance.TextManager[ETextInfo.None];
        TextApply.text = ManagerHub.Instance.TextManager[ETextInfo.None];
        TextCancel.text = ManagerHub.Instance.TextManager[ETextInfo.None];
    }


    /// <summary>
    /// 버튼에 이벤트를 등록하는 메서드
    /// </summary>
    private void AddBtnEvent()
    {
        BtnOptionApply.onClick.AddListener(UIClose);
        BtnOptionCancel.onClick.AddListener(UIClose);
    }
}


public class LanguageChangeView
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
    public void Init(Button btnLanguage, TextMeshProUGUI textLanguage, ELanguage eLanguage)
    {
        BtnLanguage = btnLanguage;
        TextLanguage = textLanguage;
        SetTextELanguage(eLanguage);
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
}
