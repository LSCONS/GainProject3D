using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EditorCSVEnumWriter : EditorWindow
{
    private static readonly string ETextInfoPath = "Assets/02. Scripts/Enum";
    private static readonly string ETextInfoName = "ETextInfo.cs";
    private static readonly string ELanguagePath = "Assets/02. Scripts/Enum";
    private static readonly string ELanguageName = "ELanguage.cs";

    [MenuItem("Tools/CSV Importer/LanguageText")]
    private static void Start()
    {
        string[] lines = CSVLoader.LoadCSV();
        WriteELanguage(lines);
        WriteETextInfo(lines);
    }

    private static void WriteELanguage(string[] lines)
    {
        string[] headers = lines[0].Split(',');
        if (headers.Length < 2)
        {
            Debug.LogError("����� ������ �����մϴ�.");
            return;
        }

        List<string> ListLanguage = new List<string>();
        for (int i = 1; i < headers.Length; i++)
        {
            ListLanguage.Add(headers[i].Trim());
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("//EditorCSVEnumWriter.cs���� �ڵ� ����");
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
        sb.AppendLine("//EditorCSVEnumWriter���� �ڵ� ����");
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
