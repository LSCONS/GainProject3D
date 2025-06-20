using UnityEngine;

public class ManagerHub : Singleton<ManagerHub>
{
    public UIManager UIManager { get; private set; } = new UIManager();
    public TextManager TextManager { get; private set; } = new TextManager();

    protected override void Awake()
    {
        base.Awake();
    }
}
