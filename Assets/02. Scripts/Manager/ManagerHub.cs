using UnityEngine;

public class ManagerHub : Singleton<ManagerHub>
{
    public UIManager UIManager { get; private set; } = new UIManager();
    public TextManager TextManager { get; private set; } = new TextManager();
    public ResourceManager ResourceManager { get; private set; } = new ResourceManager();
    public DataManager DataManager { get; private set; } = new DataManager();
    public AudioManager SoundManager { get; private set; } = new AudioManager();

    private Transform trAudioPool = null;
    public Transform TrAudioPool
    {
        get
        {
            if(trAudioPool == null)
            {
                trAudioPool = new GameObject("AudioPool").transform;
                trAudioPool.parent = transform;
            }
            return trAudioPool;
        }
    }

    protected override async void Awake()
    {
        base.Awake();
        await ResourceManager.Awake();
        UIManager.Awake();
        SoundManager.Awake();
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
