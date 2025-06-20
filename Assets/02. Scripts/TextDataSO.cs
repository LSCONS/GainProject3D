using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextDataSO", menuName = "SO/TextData")]
public class TextDataSO : ScriptableObject
{
    public List<TextData> ListTextData;
}


/// <summary>
/// string 데이터와 enum을 묶어줄 TextData
/// </summary>
[Serializable]
public class TextData
{
    public ETextInfo ETextInfo;
    public string Text = "";
}
