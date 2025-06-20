using UnityEngine;

public class UIManager
{
    public Canvas Canvas { get; private set; }

    public void Awake()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
}
