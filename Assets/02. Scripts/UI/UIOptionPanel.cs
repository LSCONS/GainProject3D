using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIOptionPanel : UIPopup, ITextChanger, IBlockerCanCloseUIPopup
{
    [field: SerializeField] private TextMeshProUGUI         TextOptionTitle         { get; set; }
    [field: SerializeField] private TextMeshProUGUI         TextApply               { get; set; }
    [field: SerializeField] private TextMeshProUGUI         TextReset               { get; set; }
    [field: SerializeField] private TextMeshProUGUI         TextCancel              { get; set; }
    [field: SerializeField] private Button                  BtnOptionApply          { get; set; }
    [field: SerializeField] private Button                  BtnOptionReset          { get; set; }
    [field: SerializeField] private Button                  BtnOptionCancel         { get; set; }
    [field: SerializeField] private List<IOptionMenu>       ListOptionMenu          { get; set; } = new();
    [field: SerializeField] public Transform                TrOptionMenuContent     { get; set; }
    [field: SerializeField] public Transform                TrOptionPanelContent    { get; set; }
    private IOptionMenu curOptionMenu;

    private void OnEnable()
    {
        curOptionMenu?.OnEnable();
        BtnOptionApply.interactable = false;
    }


    /// <summary>
    /// 첫 프리팹 생성 시 실행할 초기화 메서드
    /// </summary>
    public override void Init()
    {
        base.Init();
        InitListOptionMenu();
        closeScaleVector = Vector3.zero;
        InitFont();
        InitText();
        AddBtnEvent();
    }

    /// <summary>
    /// ListOptionMenu를 초기화하는 메서드.
    /// </summary>
    private void InitListOptionMenu()
    {
        List<IOptionMenu> ListIOptionMenuResource = ManagerHub.Instance.ResourceManager.ListOptionMenu;

        foreach (IOptionMenu optionMenu in ListIOptionMenuResource)
        {
            MonoBehaviour prefab = optionMenu as MonoBehaviour;
            if (prefab == null)
            {
                Debug.LogError($"optionMenu가 MonoBehaviour를 상속받지 않았습니다. {optionMenu.GetType()}");
                continue;
            }

            IOptionMenu menu = Instantiate(prefab, TrOptionPanelContent) as IOptionMenu;
            if (menu == null)
            {
                Debug.LogError($"{prefab.name} 인스턴스가 IOptionMenu를 구현하지 않습니다.");
                continue;
            }
            ListOptionMenu.Add(menu);
            menu.Init();
        }
    }


    /// <summary>
    /// 텍스트의 폰트를 초기화하는 메서드
    /// </summary>
    public void InitFont()
    {
        TextOptionTitle.font = ManagerHub.Instance.TextManager.nowFont;
        TextApply.font = ManagerHub.Instance.TextManager.nowFont;
        TextReset.font = ManagerHub.Instance.TextManager.nowFont;
        TextCancel.font = ManagerHub.Instance.TextManager.nowFont;
        foreach (IOptionMenu optionMenu in ListOptionMenu)
        {
            if (optionMenu is ITextChanger textChanger) textChanger.InitFont();
        }
    }


    /// <summary>
    /// 텍스트를 언어에 맞춰 초기화하는 메서드
    /// </summary>
    public void InitText()
    {
        TextOptionTitle.text    = ManagerHub.Instance.TextManager[ETextInfo.Option_Title];
        TextApply.text          = ManagerHub.Instance.TextManager[ETextInfo.Text_Aplly];
        TextReset.text          = ManagerHub.Instance.TextManager[ETextInfo.Text_Reset];
        TextCancel.text         = ManagerHub.Instance.TextManager[ETextInfo.Text_Cancel];
        foreach (IOptionMenu optionMenu in ListOptionMenu)
        {
            if (optionMenu is ITextChanger textChanger) textChanger.InitText();
        }
    }


    /// <summary>
    /// 현재 선택한 메뉴를 바꿔주는 메서드
    /// </summary>
    /// <param name="optionMenu"></param>
    public void ChangeOptionMenu(IOptionMenu optionMenu)
    {
        if (optionMenu == curOptionMenu) return;
        curOptionMenu?.UIClose();
        curOptionMenu = optionMenu;
        curOptionMenu.UIOpen();
    }


    /// <summary>
    /// 버튼에 이벤트를 등록하는 메서드
    /// </summary>
    private void AddBtnEvent()
    {
        foreach (IOptionMenu optionMenu in ListOptionMenu)
        {
            optionMenu.AddBtnEvent();
        }
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
    /// 리셋 버튼에 이벤트를 등록시켜주는 메서드
    /// </summary>
    /// <param name="action"></param>
    public void AddBtnResetEvent(UnityAction action)
    {
        BtnOptionReset.onClick.AddListener(action);
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
        foreach (IOptionMenu optionMenu in ListOptionMenu)
        {
            if (optionMenu.IsChangeSetting())
            {
                BtnOptionApply.interactable = true;
                return;
            }
        }
        BtnOptionApply.interactable = false;
    }


    /// <summary>
    /// 가림막을 눌러 해당 창을 닫을 수 있도록 설정.
    /// </summary>
    public void BlockerClose()
    {
        BtnOptionCancel.onClick?.Invoke();
    }


    /// <summary>
    /// ListOptionMenu의 요소를 추가하는 메서드.
    /// </summary>
    public void AddListOptionMenu(IOptionMenu optionMenu)
    {
        ListOptionMenu.Add(optionMenu);
    }
}
