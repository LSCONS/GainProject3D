using DG.Tweening;
using UnityEngine;

public class UIPopup : UIBase
{
    protected Vector3 closeScaleVector = Vector3.zero;
    /// <summary>
    /// UI를 열어주는 메서드
    /// </summary>
    public virtual void UIOpen()
    {
        transform.SetAsLastSibling();               //UIPopupCanva의 맨 뒤에 렌더링
        transform.localScale = closeScaleVector;    //닫히는 스케일 조정
        gameObject.SetActive(true);                 //활성화
        transform.DOScale(Vector3.one, 0.3f);       //두트윈 실행
    }


    /// <summary>
    ///  UI를 닫아주는 메서드
    /// </summary>
    public virtual void UIClose()
    {
        Sequence temp = DOTween.Sequence();
        temp.Append(transform.DOScale(closeScaleVector, 0.3f));
        temp.AppendCallback(() => gameObject.SetActive(false));
        //TODO: UIManager에서 UI활성화 여부 확인
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
