#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EditorCSVEnumWriter : EditorWindow
{
    private static readonly string ETextInfoPath = "Assets/02. Scripts/Enum";
    private static readonly string ETextInfoName = "ETextInfo.cs";
    private static readonly string ELanguagePath = "Assets/02. Scripts/Enum";
    private static readonly string ELanguageName = "ELanguage.cs";


    /// <summary>
    /// CSV의 데이터를 기준으로 새로운 Enum.cs들을 제작
    /// </summary>
    [MenuItem("Tools/CSV Importer/LanguageText")]
    private static void Start()
    {
        string[] lines = CSVLoader.LoadCSV();
        WriteELanguage(lines);
        WriteETextInfo(lines);
    }


    /// <summary>
    /// CSV파일을 읽고 ELanguage.cs를 자동 생성해주는 메서드
    /// </summary>
    private static void WriteELanguage(string[] lines) 
    {
        string[] headers = lines[0].Split(',');
        if (headers.Length < 2)
        {
            Debug.LogError("헤더의 개수가 부족합니다.");
            return;
        }

        List<string> ListLanguage = new List<string>();
        for (int i = 1; i < headers.Length; i++)
        {
            ListLanguage.Add(headers[i].Trim());
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("//EditorCSVEnumWriter.cs에서 자동 생성");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// 언어를 선택할 enum");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("public enum ELanguage");
        sb.AppendLine("{");
        for (int i = 0; i < ListLanguage.Count; i++)
        {
            sb.Append($"    {ListLanguage[i]}");
            if (i < ListLanguage.Count - 1) sb.AppendLine(",");
            else sb.AppendLine();
        }
        sb.AppendLine("}");

        if(!Directory.Exists(ELanguagePath)) Directory.CreateDirectory(ELanguagePath);
        File.WriteAllText($"{ELanguagePath}/{ELanguageName}", sb.ToString(), System.Text.Encoding.UTF8);
    }


    /// <summary>
    /// CSV파일을 읽고 ETextInfo.cs를 자동 생성해주는 메서드
    /// </summary>
    private static void WriteETextInfo(string[] lines)
    {
        List<string> ListTextInfo = new List<string>();
        for(int i = 1; i < lines.Length; i++)
        {
            string[] cols = lines[i].Split(',');
            if (cols.Length == 0) continue;
            string key = cols[0].Trim();

            ListTextInfo.Add(key);
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("//EditorCSVEnumWriter에서 자동 생성");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Text의 정보들을 담을 enum");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("public enum ETextInfo");
        sb.AppendLine("{");
        for(int i = 0;i < ListTextInfo.Count; i++)
        {
            sb.Append($"    {ListTextInfo[i]}");
            if (i < ListTextInfo.Count - 1) sb.AppendLine(",");
            else sb.AppendLine();
        }
        sb.AppendLine("}");

        if(!Directory.Exists(ETextInfoPath))Directory.CreateDirectory(ETextInfoPath);
        File.WriteAllText($"{ETextInfoPath}/{ETextInfoName}", sb.ToString(), System.Text.Encoding.UTF8);
    }
}
#endif
