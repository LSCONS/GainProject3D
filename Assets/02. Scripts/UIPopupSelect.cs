using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPopupSelect : UIPopup
{
    [field : SerializeField] private Button BtnYes { get; set; }    //확인 버튼
    [field : SerializeField] private Button BtnNo { get; set; }     //취소 버튼
    [field : SerializeField] private TextMeshProUGUI TextTitle { get; set; }    //타이틀 텍스트
    [field:  SerializeField] private TextMeshProUGUI TextDescription { get; set; }      //설명 텍스트
    private Vector3 closeScaleVector = Vector3.zero;        //팝업 창이 닫칠 때 적용할 스케일 벡터
    private ETextInfo ETextInfoTitle = ETextInfo.None;          //타이틀 텍스트 enum
    private ETextInfo ETextInfoDescription = ETextInfo.None;    //설명 텍스트 enum

    /// <summary>
    /// 팝업 창을 초기화하며 열어주는 메서드
    /// </summary>
    /// <param name="yesAction">확인 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="noAction">취소 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="closeScaleVec">팝업창이 닫칠 때 적용할 스케일 벡터</param>
    public void Init(UnityAction yesAction, UnityAction noAction, ETextInfo ETitle, ETextInfo EDescription, Vector3? closeScaleVec = null)
    {
        closeScaleVector = closeScaleVec ?? Vector3.zero;
        ETextInfoTitle = ETitle;
        ETextInfoDescription = EDescription;
        RemoveAllBtnEvent();
        AddBtnEvent(yesAction, noAction);
        InitText();
        UIOpen();
    }


    /// <summary>
    /// UI를 열어주는 메서드
    /// </summary>
    public override void UIOpen()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 1f);
    }


    /// <summary>
    ///  UI를 닫아주는 메서드
    /// </summary>
    public override void UIClose()
    {
        Sequence temp = null;
        temp.Append(transform.DOScale(closeScaleVector, 1f));
        temp.AppendCallback(() => gameObject.SetActive(false));
    }


    /// <summary>
    /// 등록된 Text를 노출 및 새로고침 해주는 메서드
    /// </summary>
    public override void InitText()
    {
        base.InitText();
        TextTitle.text = ManagerHub.Instance.TextManager[ETextInfoTitle];
        TextDescription.text = ManagerHub.Instance.TextManager[ETextInfoDescription];
    }


    /// <summary>
    /// 버튼의 모든 이벤트룰 추가해주는 메서드
    /// </summary>
    /// <param name="yesAction">확인 버튼을 눌렀을 때 실행할 메서드</param>
    /// <param name="noAction">취소 버튼을 눌렀을 때 실행할 메서드</param>
    private void AddBtnEvent(UnityAction yesAction, UnityAction noAction)
    {
        BtnYes.onClick.AddListener(yesAction);
        BtnYes.onClick.AddListener(UIClose);
        BtnNo.onClick.AddListener(noAction);
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
}
