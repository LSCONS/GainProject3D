using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public enum ItemType
{
    Health,
    Stamina
}


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Foldout("Info")]
    public int IDItem;
    public string nameItem;
    public string infoItem;
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;

    [Foldout("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Foldout("Buff")]
    public float value;
    public float duration;
}
