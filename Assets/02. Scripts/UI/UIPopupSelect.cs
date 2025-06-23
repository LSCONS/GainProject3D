using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPopupSelect : UIPopup, IBlockerCanCloseUIPopup
{
    [field: SerializeField] private Button BtnYes { get; set; }    //확인 버튼
    [field: SerializeField] private Button BtnNo { get; set; }     //취소 버튼
    [field: SerializeField] private TextMeshProUGUI TextTitle { get; set; }    //타이틀 텍스트
    [field: SerializeField] private TextMeshProUGUI TextDescription { get; set; }      //설명 텍스트

    /// <summary>
    /// 팝업 창을 초기화하며 열어주는 메서드
    /// </summary>
    /// <param name="yesAction">확인 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="noAction">취소 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="closeScaleVec">팝업창이 닫칠 때 적용할 스케일 벡터</param>
    public void Init(UnityAction yesAction, UnityAction noAction, string Title, string Description, Vector3? closeScaleVec = null)
    {
        closeScaleVector = closeScaleVec ?? Vector3.zero;
        InitText(Title, Description);
        RemoveAllBtnEvent();
        AddBtnEvent(yesAction, noAction);
        UIOpen();
    }


    /// <summary>
    /// UI를 열어주는 메서드
    /// </summary>
    public override void UIOpen()
    {
        base.UIOpen();
    }


    /// <summary>
    ///  UI를 닫아주는 메서드
    /// </summary>
    public override void UIClose()
    {
        base.UIClose();
    }


    /// <summary>
    /// 텍스트를 초기화해주는 메서드
    /// </summary>
    /// <param name="Title">팝업의 제목</param>
    /// <param name="Description">팝업의 설명</param>
    private void InitText(string Title, string Description)
    {
        TextTitle.font = ManagerHub.Instance.TextManager.nowFont;
        TextDescription.font = ManagerHub.Instance.TextManager.nowFont;
        TextTitle.text = Title;
        TextDescription.text = Description;
    }


    /// <summary>
    /// 버튼의 모든 이벤트룰 추가해주는 메서드
    /// </summary>
    /// <param name="yesAction">확인 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="noAction">취소 버튼을 눌렀을 때 실행할 메서드</param>
    private void AddBtnEvent(UnityAction yesAction, UnityAction noAction)
    {
        if (yesAction != null) BtnYes.onClick.AddListener(yesAction);
        BtnYes.onClick.AddListener(UIClose);
        if (noAction != null) BtnNo.onClick.AddListener(noAction);
        BtnNo.onClick.AddListener(UIClose);
    }


    /// <summary>
    /// 버튼의 모든 이벤트를 지워주는 메서드
    /// </summary>
    private void RemoveAllBtnEvent()
    {
        BtnYes.onClick.RemoveAllListeners();
        BtnNo.onClick.RemoveAllListeners();
    }


    /// <summary>
    /// 가림막을 눌러 해당 창을 닫을 수 있도록 설정.
    /// </summary>
    public void BlockerClose()
    {
        BtnNo.onClick?.Invoke();
    }
}
