using UnityEngine;

public class DataManager
{
    public ELanguage NowLanguage
    {
        get => (ELanguage)PlayerPrefs.GetInt(ReadonlyData.LanguagePrefs, (int)ELanguage.English);
    }


    /// <summary>
    /// 옵션에서 언어 설정을 새롭게 저장했을 때 실행할 메서드
    /// </summary>
    /// <param name="eLanguage">바꿀 언어의 enum</param>
    public void SetNowLanguage(ELanguage eLanguage)
    {
        if(NowLanguage != eLanguage)
        {
            PlayerPrefs.SetInt(ReadonlyData.LanguagePrefs, (int)eLanguage);
            ManagerHub.Instance.UIManager.ChangeLanguage(eLanguage);
        }
    }
}
