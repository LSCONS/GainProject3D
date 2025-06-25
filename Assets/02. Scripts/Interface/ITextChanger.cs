/// <summary>
/// 로컬라이징처리를 할 때 사용할 인터페이스
/// 텍스트 출력이 있는 클래스에 붙여서 사용.
/// </summary>
public interface ITextChanger
{
    /// <summary>
    /// Text의 출력을 초기화해주는 메서드
    /// </summary>
    public void InitText();


    /// <summary>
    /// Text의 Font를 초기화해주는 메서드
    /// </summary>
    public void InitFont();
}
