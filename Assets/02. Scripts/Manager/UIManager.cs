using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VInspector.Libs;

public class UIManager
{
    public Canvas Canvas { get; private set; }
    public UIPopupSelect UIPopupSelect { get; private set; }
    public List<ITextChanger> ListTextChanger { get; set; } = new();

    public void Awake()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        UIPopupSelect = GameObject.Instantiate(ManagerHub.Instance.DataManager.UIPopupSelect, Canvas.transform);
        UIPopupSelect.gameObject.SetActive(false);
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


    public void ChangeLanguage(ELanguage eLanguage)
    {
        ManagerHub.Instance.TextManager.InitDictTextToString(eLanguage);
        foreach(ITextChanger changer in ListTextChanger)
        {
            changer.InitText();
        }
    }
}


public interface ITextChanger
{
    public void InitText();
}
