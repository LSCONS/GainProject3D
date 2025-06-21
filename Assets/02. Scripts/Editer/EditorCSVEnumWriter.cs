#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EditorCSVEnumWriter : EditorWindow
{
    private static readonly string EPath = "Assets/02. Scripts/Enum";
    private static readonly string ETextInfoName = "ETextInfo.cs";
    private static readonly string ELanguageName = "ELanguage.cs";
    private static readonly string ETextStrKeyName = "ETextStrKey.cs";


    /// <summary>
    /// CSV의 데이터를 기준으로 새로운 Enum.cs들을 제작
    /// </summary>
    [MenuItem("Tools/CSV Importer/LanguageText")]
    private static void Start()
    {
        string[] lines = CSVLoader.LoadCSV();
        WriteELanguage(lines);
        WriteEText(lines);
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
        CreateELanguage(ListLanguage);
    }


    /// <summary>
    /// CSV파일을 읽고 ETextInfo.cs를 자동 생성해주는 메서드
    /// </summary>
    private static void WriteEText(string[] lines)
    {
        List<string> ListTextInfo = new List<string>();
        List<string> ListStringKey = new List<string>();
        for (int i = 1; i < lines.Length; i++)
        {
            //ETextInfo를 만들 값 추출
            string[] cols = lines[i].Split(',');
            if (cols.Length == 0) continue;
            StringBuilder lineSB = new StringBuilder();
            lineSB.Append(cols[0].Trim());
            lineSB.Append(",\t//");
            lineSB.Append(cols[1].Trim());
            ListTextInfo.Add(lineSB.ToString());

            //매개변수 넣을 StringKey값 추출
            int pos = 0;
            while (true)
            {
                int start = cols[1].IndexOf('{', pos);
                if (start < 0) break;
                int end = cols[1].IndexOf('}', start + 1);
                if (end < 0) break;

                string result = cols[1].Substring(start + 1, end - start - 1);
                ListStringKey.Add(result);
                pos = end + 1;
            }
        }
        CreateETextInfo(ListTextInfo);
        CreateETextStrKey(ListStringKey);
    }


    /// <summary>
    /// ELanguage.cs를 만들어주는 메서드
    /// </summary>
    /// <param name="ListLanguage"></param>
    private static void CreateELanguage(List<string> ListLanguage)
    {
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

        if (!Directory.Exists(EPath)) Directory.CreateDirectory(EPath);
        File.WriteAllText($"{EPath}/{ELanguageName}", sb.ToString(), System.Text.Encoding.UTF8);
    }


    /// <summary>
    /// ETextInfo.cs를 자동 생성해주는 메서드
    /// </summary>
    /// <param name="ListTextInfo"></param>
    private static void CreateETextInfo(List<string> ListTextInfo)
    {
        StringBuilder ETextInfoSB = new StringBuilder();
        ETextInfoSB.AppendLine("//EditorCSVEnumWriter에서 자동 생성");
        ETextInfoSB.AppendLine("/// <summary>");
        ETextInfoSB.AppendLine("/// Text의 정보들을 담을 enum");
        ETextInfoSB.AppendLine("/// <summary>");
        ETextInfoSB.AppendLine("public enum ETextInfo");
        ETextInfoSB.AppendLine("{");
        for (int i = 0; i < ListTextInfo.Count; i++)
        {
            ETextInfoSB.AppendLine($"    {ListTextInfo[i]}");
        }
        ETextInfoSB.AppendLine("}");

        if (!Directory.Exists(EPath)) Directory.CreateDirectory(EPath);
        File.WriteAllText($"{EPath}/{ETextInfoName}", ETextInfoSB.ToString(), System.Text.Encoding.UTF8);
    }


    /// <summary>
    /// ETextStrKey.cs를 자동생성해주는 메서드
    /// </summary>
    /// <param name="ListStringKey"></param>
    private static void CreateETextStrKey(List<string> ListStringKey)
    {
        StringBuilder ETextStrKeySB = new StringBuilder();
        ETextStrKeySB.AppendLine("//EditorCSVEnumWriter에서 자동 생성");
        ETextStrKeySB.AppendLine("/// <summary>");
        ETextStrKeySB.AppendLine("/// Text의 StringKey들을 담을 enum");
        ETextStrKeySB.AppendLine("/// <summary>");
        ETextStrKeySB.AppendLine("public enum ETextStrKey");
        ETextStrKeySB.AppendLine("{");
        for (int i = 0; i < ListStringKey.Count; i++)
        {
            ETextStrKeySB.AppendLine($"    {ListStringKey[i]},");
        }
        ETextStrKeySB.AppendLine("}");

        if (!Directory.Exists(EPath)) Directory.CreateDirectory(EPath);
        File.WriteAllText($"{EPath}/{ETextStrKeyName}", ETextStrKeySB.ToString(), System.Text.Encoding.UTF8);
    }
}
#endif
