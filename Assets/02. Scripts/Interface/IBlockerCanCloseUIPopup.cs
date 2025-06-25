/// <summary>
/// UIPopup클래스 중 Blocker의 클릭으로 취소될 수 있는 UIPopup에 붙여서 사용.
/// UIPopup의 클릭 가림막을 눌러 UIPopup을 닫을 수 있음.
/// </summary>
public interface IBlockerCanCloseUIPopup
{
    public void BlockerClose();
}
