using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextDataSO", menuName = "SO/TextData")]
public class TextDataSO : ScriptableObject
{
    public List<TextData> ListTextData;
}


/// <summary>
/// string �����Ϳ� enum�� ������ TextData
/// </summary>
[Serializable]
public class TextData
{
    public ETextInfo ETextInfo;
    public string Text = "";
}
