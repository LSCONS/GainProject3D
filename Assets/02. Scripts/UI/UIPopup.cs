using DG.Tweening;
using UnityEngine;

public class UIPopup : UIBase
{
    protected Vector3 closeScaleVector = Vector3.zero;
    private Sequence closeSequence = null;
    /// <summary>
    /// UI를 열어주는 메서드
    /// </summary>
    public virtual void UIOpen()
    {
        Debug.Log(ManagerHub.Instance.UIManager.openUIPopupCount);
        ManagerHub.Instance.UIManager.AddUIPopupCount();    //UIPopup의 개수를 늘리고 Canvas를 활성화
        transform.SetAsLastSibling();                       //UIPopupCanvas의 맨 뒤에 렌더링
        transform.localScale = closeScaleVector;            //닫히는 스케일 조정
        gameObject.SetActive(true);                         //활성화
        transform.DOScale(Vector3.one, 0.3f);               //두트윈 실행
    }


    /// <summary>
    ///  UI를 닫아주는 메서드
    /// </summary>
    public virtual void UIClose()
    {
        Debug.Log(ManagerHub.Instance.UIManager.openUIPopupCount);
        if (closeSequence != null) return;
        closeSequence = DOTween.Sequence();
        closeSequence.Append(transform.DOScale(closeScaleVector, 0.3f));
        closeSequence.AppendCallback(() => gameObject.SetActive(false));
        closeSequence.AppendCallback(() => ManagerHub.Instance.UIManager.RemoveUIPopupCount());
        closeSequence.AppendCallback(() => closeSequence = null);
    }


    /// <summary>
    /// 프리팹 첫 생성에 초기화해줄 메서드
    /// </summary>
    public override void Init()
    {
        base.Init();
        gameObject.SetActive(false);
    }
}
