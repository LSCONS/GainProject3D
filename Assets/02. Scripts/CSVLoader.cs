using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoader
{
    private static readonly string csvFilePath = "Assets/05. Data/Firestore/localization.csv";


    /// <summary>
    /// CSV파일의 모든 줄을 반환해주는 메서드.
    /// </summary>
    public static string[] LoadCSV()
    {
        string[] lines = File.ReadAllLines(csvFilePath);
        if (lines.Length < 2) Debug.LogError("Not Found OR Null is CSV");
        return lines;
    }


    public void LoadText(ELanguage eLanguage)
    {
        Dictionary<ETextInfo, string> dictResult = new Dictionary<ETextInfo, string>();
    }

    public void LoadKey()
    {

    }
}
