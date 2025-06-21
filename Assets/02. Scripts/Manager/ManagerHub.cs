using UnityEngine;

public class ManagerHub : Singleton<ManagerHub>
{
    public UIManager UIManager { get; private set; } = new UIManager();
    public TextManager TextManager { get; private set; } = new TextManager();
    public DataManager DataManager { get; private set; } = new DataManager();

    protected override async void Awake()
    {
        base.Awake();
        await DataManager.Awake();
        UIManager.Awake();
        TextManager.Awake();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.InitUIPopupSelect
                (null,
                null,
                ManagerHub.Instance.TextManager[ETextInfo.Popup_Title],
                ManagerHub.Instance.TextManager[ETextInfo.Popup_Description],
                Vector3.up);
        }
    }
}
