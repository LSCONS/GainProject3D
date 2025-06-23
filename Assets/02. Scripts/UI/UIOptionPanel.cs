using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIOptionPanel : UIPopup, ITextChanger
{
    [field: SerializeField] private TextMeshProUGUI TextOptionTitle { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextApply { get; set; }
    [field: SerializeField] private TextMeshProUGUI TextCancel { get; set; }
    [field: SerializeField] private Button BtnOptionApply { get; set; }
    [field: SerializeField] private Button BtnOptionCancel { get; set; }
    [field: SerializeField] private UIOptionLanguageSetting UIOptionLanguageSetting { get; set; }


    private void OnEnable()
    {
        UIOptionLanguageSetting.OnEnable();
        BtnOptionApply.interactable = false;
    }


    /// <summary>
    /// 첫 프리팹 생성 시 실행할 초기화 메서드
    /// </summary>
    public override void Init()
    {
        UIOptionLanguageSetting?.Init();
        closeScaleVector = Vector3.zero;
        InitText();
        AddBtnEvent();
        base.Init();
    }
    

    /// <summary>
    /// 텍스트를 언어에 맞춰 초기화하는 메서드
    /// </summary>
    public void InitText()
    {
        TextOptionTitle.text    = ManagerHub.Instance.TextManager[ETextInfo.Option_Title];
        TextApply.text          = ManagerHub.Instance.TextManager[ETextInfo.Text_Aplly];
        TextCancel.text         = ManagerHub.Instance.TextManager[ETextInfo.Text_Cancel];
        UIOptionLanguageSetting?.InitText();
    }


    /// <summary>
    /// 버튼에 이벤트를 등록하는 메서드
    /// </summary>
    private void AddBtnEvent()
    {
        UIOptionLanguageSetting?.AddBtnEvent();
        AddBtnCancelEvent(UIClose);
        AddBtnApplyEvent(IsChangeSetting);
    }


    /// <summary>
    /// 적용 버튼에 이벤트를 등록시켜주는 메서드
    /// </summary>
    /// <param name="action">등록할 이벤트</param>
    public void AddBtnApplyEvent(UnityAction action)
    {
        BtnOptionApply.onClick.AddListener(action);
    }


    /// <summary>
    /// 취소 버튼에 이벤트를 등록시켜주는 메서드
    /// </summary>
    /// <param name="action">등록할 이벤트</param>
    public void AddBtnCancelEvent(UnityAction action)
    {
        BtnOptionCancel.onClick.AddListener(action);
    }


    /// <summary>
    /// 옵션의 세팅 값 중 바뀐 것이 있는지 확인하고 버튼을 활성화/비활성화 해주는 메서드
    /// </summary>
    public void IsChangeSetting()
    {
        bool isChange =
            (
                UIOptionLanguageSetting.IsChangeLanguage()
            );

        BtnOptionApply.interactable = isChange ? true : false;
    }
}
