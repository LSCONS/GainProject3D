/// <summary>
/// 옵션 창의 메뉴임을 알리는 인터페이스.
/// </summary>
public interface IOptionMenu
{
    /// <summary>
    /// 오브젝트가 활성화되었을 때 실행할 메서드.
    /// </summary>
    public void OnEnable();


    /// <summary>
    /// UI를 처음으로 생성했을 때 실행할 메서드.
    /// </summary>
    public void Init();


    /// <summary>
    /// OptionPanel에서 해당 UIMenu를 열었을 때 실행할 메서드
    /// </summary>
    public void UIOpen();


    /// <summary>
    /// OptionPanel에서 해당 UIMenu를 닫았을 때 실행할 메서드
    /// </summary>
    public void UIClose();


    /// <summary>
    /// 환경설정이 변경되었는지 확인하는 메서드.
    /// </summary>
    /// <returns>false면 변경 없음. true면 변경사항 있음.</returns>
    public bool IsChangeSetting();


    /// <summary>
    /// 버튼의 이벤트를 추가하는 메서드.
    /// </summary>
    public void AddBtnEvent();
}
