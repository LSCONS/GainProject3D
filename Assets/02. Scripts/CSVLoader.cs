using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoader
{
    private static readonly string csvFilePath = "Assets/05. Datas/Firestore/localization.csv";

    /// <summary>
    /// CSV파일의 모든 줄을 반환해주는 메서드.
    /// </summary>
    public static string[] LoadCSV()
    {
#if UNITY_EDITOR
        string[] lines = File.ReadAllLines(csvFilePath);
#else
        string fullPath = Path.Combine(Application.dataPath, csvFilePath);
        string[] lines = File.ReadAllLines(fullPath);
#endif
        if (lines.Length < 2) Debug.LogError("Not Found OR Null is CSV");
        return lines;
    }


    /// <summary>
    /// CSV에 접근해서 언어 설정과 알맞는 텍스트 리스트를 뽑아 Dictionary로 만들어 반환해주는 메서드
    /// </summary>
    /// <param name="eLanguage">뽑을 언어의 enum</param>
    public static Dictionary<ETextInfo, string> LoadText(ELanguage eLanguage)
    {
        Dictionary<ETextInfo, string> dictResult = new Dictionary<ETextInfo, string>();

        string[] lines = LoadCSV();
        int selectIndex = 0;
        
        
        if(lines.Length - 1 != Enum.GetValues(typeof(ETextInfo)).Length)
        {
            Debug.LogError("자동 생성된 Enum의 수와 CSV의 Enum의 수가 맞지 않습니다. 업데이트를 해주세요.");
            return null;
        }

        string[] headers = lines[0].Split(',');
        for(int i = 1; i < headers.Length; i++)
        {
            if (headers[i] == eLanguage.ToString())
            {
                selectIndex = i;
                break;
            }
        }

        if(selectIndex == 0)
        {
            Debug.LogError("자동 생성된 Enum의 언어를 CSV에서 찾을 수 없습니다. 업데이트를 해주세요.");
            return null;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            headers = lines[i].Split(",");
            dictResult.Add((ETextInfo)(i - 1), headers[selectIndex]);
        }

        return dictResult;
    }
}
