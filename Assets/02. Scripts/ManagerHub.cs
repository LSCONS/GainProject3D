using UnityEngine;

public class ManagerHub : Singleton<ManagerHub>
{
    public UIManager UIManager { get; private set; } = new UIManager();
    public TextManager TextManager { get; private set; } = new TextManager();
    public DataManager DataManager { get; private set; } = new DataManager();

    protected override void Awake()
    {
        base.Awake();
        DataManager.Awake();
        UIManager.Awake();
        TextManager.Awake();
    }
}
