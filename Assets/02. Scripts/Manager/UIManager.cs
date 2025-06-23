using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager
{
    public Canvas UIPopupCanvas { get; private set; }
    public Canvas UIPermanentCanvas { get; private set; }
    private List<ITextChanger> ListTextChanger { get; set; } = new();
    public Dictionary<EUISibling, UIBase> DictEUIToUIBase { get; private set; } = new();
    public Dictionary<Type, UIBase> DictUIBaseToT { get; private set; } = new();
    public int openUIPopupCount = 0; //TODO 나중에 private로 변환

    public void Awake()
    {
        UIPopupCanvas       = GameObject.Find("UIPopupCanvas").GetComponent<Canvas>();
        UIPermanentCanvas   = GameObject.Find("UIPermanentCanvas").GetComponent<Canvas>();
        Init();
    }


    public void Init()
    {
        CreateUIPrefabs<UIOptionPanel>(ManagerHub.Instance.ResourceManager.UIOptionPanel);
        CreateUIPrefabs<UIOptionOpen>(ManagerHub.Instance.ResourceManager.UIOptionOpen);
        CreateUIPrefabs<UIPopupSelect>(ManagerHub.Instance.ResourceManager.UIPopupSelect);
        foreach(UIBase uIBase in DictUIBaseToT.Values)
        {
            uIBase.Init();
        }
        CheckIsUiPopupOpen();
    }


    /// <summary>
    /// 선택 팝업UI를 여는 메서드
    /// </summary>
    /// <param name="yesAction">확인 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="noAction">취소 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="Title">제목에 넣을 텍스트 enum</param>
    /// <param name="Description">설명에 넣을 텍스트 enum</param>
    /// <param name="closeScaleVec">닫치는 연출을 설정할 Scale벡터</param>
    public void InitUIPopupSelect(UnityAction yesAction, UnityAction noAction, string Title, string Description, Vector3? closeScaleVec = null)
    {
        ReturnDictUIBaseToT<UIPopupSelect>()?.Init(yesAction, noAction, Title, Description, closeScaleVec);
    }


    /// <summary>
    /// 선택된 언어로 Text들을 초기화해주는 메서드
    /// </summary>
    /// <param name="eLanguage"></param>
    public void ChangeLanguage(ELanguage eLanguage)
    {
        ManagerHub.Instance.TextManager.InitDictTextToString(eLanguage);
        foreach(ITextChanger changer in ListTextChanger)
        {
            changer.InitText();
        }
    }


    /// <summary>
    /// ListTextChanger의 요소를 넣어주는 메서드
    /// </summary>
    /// <param name="textChanger"></param>
    public void AddListTextChanger(ITextChanger textChanger)
    {
        ListTextChanger.Add(textChanger);
    }


    /// <summary>
    /// DictEUIToUIBase의 요소를 추가 및 덮어쓰기 해주는 메서드
    /// </summary>
    /// <param name="eUISibling">추가할 요소의 enum</param>
    /// <param name="uIBase">추가할 요소의 uiBase</param>
    public void AddDictEUIToUIBase(EUISibling eUISibling, UIBase uIBase)
    {
        DictEUIToUIBase[eUISibling] = uIBase;
    }


    /// <summary>
    /// 열려있는 UIPopup의 개수를 올리는 메서드
    /// </summary>
    public void AddUIPopupCount()
    {
        openUIPopupCount++;
        CheckIsUiPopupOpen();
    }


    /// <summary>
    /// 열려있는 UIPopup의 개수를 내리는 메서드
    /// </summary>
    public void RemoveUIPopupCount()
    {
        openUIPopupCount--;
        CheckIsUiPopupOpen();
    }


    /// <summary>
    /// 열러있는 팝업 창의 수를 확인하고 팝업Canvas를 활성화/비활성화 처리하는 메서드
    /// </summary>
    public void CheckIsUiPopupOpen()
    {
        bool isActive = (openUIPopupCount > 0) ? true : false;
        if(UIPopupCanvas.gameObject.activeSelf != isActive)
        {
            UIPopupCanvas.gameObject.SetActive(isActive);
        }
    }


    /// <summary>
    /// UIPrefabs를 생성하고 알맞는 Canvas에 넣은 뒤, 초기화도 해주며 해당 프리팹을 반환해주는 메서드
    /// </summary>
    /// <typeparam name="T">생성할 프리팹 타입</typeparam>
    /// <param name="UIPrefab">생성할 프리팹</param>
    /// <returns>생성한 프리팹</returns>
    private void CreateUIPrefabs<T>(T UIPrefab) where T : UIBase
    {
        Transform tr = (UIPrefab is UIPopup) ? UIPopupCanvas.transform : UIPermanentCanvas.transform;
        T originalObj = GameObject.Instantiate(UIPrefab, tr);
        if(originalObj is ITextChanger textChanger)
        {
            AddListTextChanger(textChanger);
        }
        AddDictUIBaseToType(originalObj);
    }


    /// <summary>
    /// DictUIBaseToType에 요소를 집어넣는 메서드
    /// </summary>
    /// <param name="uIBase">집어넣을 uiBase</param>
    private void AddDictUIBaseToType<T>(T uIBase) where T : UIBase
    {
        DictUIBaseToT[uIBase.GetType()] = uIBase;
    }


    /// <summary>
    /// DictUIBaseToType의 요소를 제너릭 형태로 반환받는 메서드
    /// </summary>
    /// <typeparam name="T">반환 받고 싶은 제너릭 타입</typeparam>
    /// <param name="uiBase"></param>
    /// <returns></returns>
    public T ReturnDictUIBaseToT<T>() where T : UIBase
    {
        if(DictUIBaseToT.TryGetValue(typeof(T), out UIBase result))
        {
            return result as T;
        }
        return null;
    }
}


/// <summary>
/// 로컬라이징처리를 할 때 사용할 인터페이스
/// </summary>
public interface ITextChanger
{
    public void InitText();
}


/// <summary>
/// UI의 렌더링 순서를 결정할 enum
/// </summary>
public enum EUISibling
{
    None,

}
