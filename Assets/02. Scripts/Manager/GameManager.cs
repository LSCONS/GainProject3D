using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameObject player;

    protected override void Awake()
    {
        base.Awake();
    }
}
