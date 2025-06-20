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
        Debug.Log("���� ����");
        var dep = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dep != DependencyStatus.Available)
        {
            Debug.LogError("���̾�̽� ���Ӽ� ����");
            return;
        }
        Debug.Log("�ε� �Ϸ�");
        var db = FirebaseFirestore.DefaultInstance;
        await UploadCSV(db);      // UploadCSV�� Task ��ȯ
        Debug.Log("���� ��");
    }


    private static async Task UploadCSV(FirebaseFirestore db)
    {
        Debug.Log("���ε� ����");
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

            Debug.Log("����");
            await db.Collection(CollectionName).Document(docID).SetAsync(data);
            Debug.Log("����");
        }

        Debug.Log("CSV ���ε� �Ϸ�");
    }
}
#endif