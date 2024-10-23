using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseTest : MonoBehaviour
{
    DatabaseReference reference;

    void Start()
    {
        // Firebase初期化処理
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Firebaseが使用可能になったらDatabaseに接続
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    void InitializeFirebase()
    {
        // Firebase Realtime Databaseの参照を取得
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // ランダムな値を生成してデータベースに書き込む
        WriteRandomValueToDatabase();
    }

    void WriteRandomValueToDatabase()
    {
        // ランダムな値を生成 (例: 0から100の整数)
        int randomValue = Random.Range(0, 101);

        // データベースの指定パスにランダムな値を書き込む
        string key = reference.Child("test_data").Push().Key;
        reference.Child("test_data").Child(key).SetValueAsync(randomValue).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("データベースにランダム値を保存しました: " + randomValue);
            }
            else
            {
                Debug.LogError("データの保存に失敗しました: " + task.Exception);
            }
        });
    }
}
