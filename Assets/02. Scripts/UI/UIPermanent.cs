using UnityEngine;

public class UIPermanent : UIBase
{
    [field: Header("렌더링 순서를 결정하는 enum. 위쪽이 먼저 그려짐")]
    [field: SerializeField] public EUISibling EUISibling { get; private set; } = EUISibling.None;


    /// <summary>
    /// 첫 프리팹 생성 시 실행할 초기화 메서드
    /// </summary>
    public override void Init()
    {
        base.Init();
        ManagerHub.Instance.UIManager.AddDictEUIToUIBase(EUISibling, this);
        transform.SetSiblingIndex((int)EUISibling);
        gameObject.SetActive(true);
    }
}
