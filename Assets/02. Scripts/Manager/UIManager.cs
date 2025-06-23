using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager
{
    public Canvas UIPopupCanvas { get; private set; }
    public Canvas UIPermanentCanvas { get; private set; }
    public UIPopupSelect UIPopupSelect { get; private set; }
    public UIOptionOpen UIOptionOpen { get; set; }
    public UIOptionPanel UIOptionPanel { get; set; }
    public List<ITextChanger> ListTextChanger { get; set; } = new();
    public Dictionary<EUISibling, UIBase> DictEUIToUIBase { get; private set; } = new();

    public void Awake()
    {
        UIPopupCanvas = GameObject.Find("UIPopupCanvas").GetComponent<Canvas>();
        UIPermanentCanvas = GameObject.Find("UIPermanentCanvas").GetComponent<Canvas>();
    }


    public void Init()
    {
        UIPopupSelect = CreateUIPrefabs<UIPopupSelect>(ManagerHub.Instance.ResourceManager.UIPopupSelect);
        UIOptionOpen = CreateUIPrefabs<UIOptionOpen>(ManagerHub.Instance.ResourceManager.UIOptionOpen);
        UIOptionPanel = CreateUIPrefabs<UIOptionPanel>(ManagerHub.Instance.ResourceManager.UIOptionPanel);
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
        UIPopupSelect.Init(yesAction, noAction, Title, Description, closeScaleVec);
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
    /// DictEUIToUIBase의 요소를 추가 및 덮어쓰기 해주는 메서드
    /// </summary>
    /// <param name="eUISibling">추가할 요소의 enum</param>
    /// <param name="uIBase">추가할 요소의 uiBase</param>
    public void AddDictEUIToUIBase(EUISibling eUISibling, UIBase uIBase)
    {
        DictEUIToUIBase[eUISibling] = uIBase;
    }


    /// <summary>
    /// UIPrefabs를 생성하고 알맞는 Canvas에 넣은 뒤, 초기화도 해주며 해당 프리팹을 반환해주는 메서드
    /// </summary>
    /// <typeparam name="T">생성할 프리팹 타입</typeparam>
    /// <param name="UIPrefab">생성할 프리팹</param>
    /// <returns>생성한 프리팹</returns>
    private T CreateUIPrefabs<T>(T UIPrefab) where T : UIBase
    {
        Transform tr = (UIPrefab is UIPopup) ? UIPopupCanvas.transform : UIPermanentCanvas.transform;
        T result = GameObject.Instantiate(UIPrefab, tr);
        result.Init();
        return result;
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
