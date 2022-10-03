using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public Color TeamColor;

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    private string GetDataPath()
    {
        return Application.persistentDataPath + "/data.json";
    }

    public void SaveColor()
    {
        Debug.Log("SaveColor");
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        string path = GetDataPath();
        Debug.Log("path: " + path);
        File.WriteAllText(path, json);
    }

    public void LoadColor()
    {
        string path = GetDataPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            TeamColor = data.TeamColor;
        }
    }
}
