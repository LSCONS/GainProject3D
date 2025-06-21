using System.Collections.Generic;
using Trismegistus.SmartFormat;
using Unity.VisualScripting;
using UnityEngine;

public class TextManager
{
    private Dictionary<ETextInfo, string> DictETextToString = new(); //텍스트 정보를 저장할 Dictionary
    private Dictionary<string, object> DictTextStrKeyToObj = new(); //초기화 인자로 넣을 Dictionary

    public void Awake()
    {
        int languageIndex = PlayerPrefs.GetInt(ReadonlyData.LanguagePrefs, (int)ELanguage.English);
        ManagerHub.Instance.UIManager.ChangeLanguage((ELanguage)languageIndex);
    }


    /// <summary>
    /// 입력받은 enum을 통해 해당하는 string을 반환
    /// </summary>
    public string this[ETextInfo eTextInfo]
    {
        get
        {
            if(DictETextToString.TryGetValue(eTextInfo, out string str))
            {
                return Smart.Format(str, DictTextStrKeyToObj);
            }
            else
            {
                return "[Not Found]";
            }
        }
    }


    /// <summary>
    /// Dictionary에 매개변수 텍스트를 추가할 메서드
    /// </summary>
    /// <param name="key">매개변수 텍스트의 key값</param>
    /// <param name="value">매개변수 텍스트의 value값</param>
    public void AddDictTextStrKeyToObj(ETextStrKey key, object value)
    {
        DictTextStrKeyToObj[key.ToString()] = value;
    }


    /// <summary>
    /// 출력받을 텍스트 stringKey를 받아 stringValue 반환하는 메서드
    /// </summary>
    /// <param name="key">출력 받을 텍스트의 stringKey</param>
    /// <returns>반환 받을 텍스트의 stringValue</returns>
    public Dictionary<string, object> ReturnDictStrToStr()
    {
        return DictTextStrKeyToObj;
    }


    /// <summary>
    /// Dictionary에 텍스트를 추가하는 메서드
    /// </summary>
    /// <param name="eTextInfo">추가할 텍스트 enum</param>
    /// <param name="text">추가할 텍스트 string</param>
    public void AddDictETextToString(ETextInfo eTextInfo, string text)
    {
        DictETextToString[eTextInfo] = text;
    }


    /// <summary>
    /// 출력 받을 텍스트 enum을 받아 string을 반환하는 메서드
    /// </summary>
    /// <param name="eTextInfo">출력할 텍스트 enum</param>
    /// <returns>반환받을 텍스트 string</returns>
    public string ReturnDictTextToString(ETextInfo eTextInfo)
    {
        if(DictETextToString.TryGetValue(eTextInfo, out string text))
        {
            return text;
        }
        else
        {
            return "[ETextInfo Not Found]";
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
