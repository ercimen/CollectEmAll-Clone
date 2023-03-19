using System.IO;
using UnityEngine;

public static class JsonDataHelper
{
    public static string GetJsonFilePath(string fileName)
    {
        string path;

#if UNITY_EDITOR || UNITY_STANDALONE
        path = Path.Combine(Application.dataPath, fileName);
#elif UNITY_ANDROID
        path = Path.Combine(Application.persistentDataPath, fileName);
#elif UNITY_IOS
        path = Path.Combine(Application.persistentDataPath, "Raw", fileName);
#else
        path = Path.Combine(Application.dataPath, fileName);
#endif

            return path;
    }

    public static T LoadJsonData<T>(string fileName)
    {
        T data = default;
        string filePath = GetJsonFilePath(fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<T>(json);
        }
        else
        {
            Debug.LogError($"JSON file not found at: {filePath}");
        }

        return data;
    }
}
