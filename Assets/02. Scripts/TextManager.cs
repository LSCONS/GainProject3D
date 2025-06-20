using System.Collections.Generic;
using UnityEngine;

public class TextManager
{
    public Dictionary<ETextInfo, string> DictETextToString = new(); //텍스트 정보를 저장할 Dictionary

    public void Awake()
    {
        int languageIndex = PlayerPrefs.GetInt(ReadonlyData.LanguagePrefs, (int)ELanguage.EN);
        InitDictTextToString((ELanguage)languageIndex);
    }


    /// <summary>
    /// 입력받은 enum을 통해 해당하는 string을 반환
    /// </summary>
    public string this[ETextInfo eTextInfo]
    {
        get
        {
            return DictETextToString[eTextInfo];
        }
    }


    /// <summary>
    /// Dictionary의 값을 초기화하는 메서드.
    /// </summary>
    /// <param name="eLanguage"></param>
    public void InitDictTextToString(ELanguage eLanguage)
    {
        DictETextToString.Clear();
        DictETextToString = CSVLoader.LoadText(eLanguage);
    }
}
