#if UNITY_EDITOR
using Firebase;
using Firebase.Firestore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class EditorFirestoreUploader : MonoBehaviour
{
    private static string CollectionName = "localization";
    [MenuItem("FireBase/Upload CSV to Firestore")]
    private static async void Upload()
    {
        Debug.Log("실행 시작");
        var dep = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dep != DependencyStatus.Available)
        {
            Debug.LogError("파이어베이스 종속성 오류");
            return;
        }
        Debug.Log("로드 완료");
        var db = FirebaseFirestore.DefaultInstance;
        await UploadCSV(db);      // UploadCSV는 Task 반환
        Debug.Log("실행 끝");
    }


    private static async Task UploadCSV(FirebaseFirestore db)
    {
        Debug.Log("업로드 시작");
        string csvPath = $"Assets/05. Data/Firestore/{CollectionName}.csv";
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length == 0) Debug.LogError("Not Found csv");
        string[] headers = lines[0].Split(',');
        Debug.Log(lines.Length);
        Debug.Log(headers.Length);

        for (int i = 1; i < lines.Length; i++)
        {
            var cols = lines[i].Split(',');
            var docID = cols[0];
            var data = new Dictionary<string, object>();

            for (int j = 1; j < headers.Length; j++)
            {
                data[headers[j]] = cols[j];
            }

            Debug.Log("시작");
            await db.Collection(CollectionName).Document(docID).SetAsync(data);
            Debug.Log("종료");
        }

        Debug.Log("CSV 업로드 완료");
    }
}
#endif