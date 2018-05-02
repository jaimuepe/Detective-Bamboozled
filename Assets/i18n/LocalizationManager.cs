using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "<!!! not found !!!>";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalizedText("localizedText_en.json");
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        LocalizationData loadedData;

        if (!File.Exists(filePath))
        {
            filePath = Path.Combine(Application.streamingAssetsPath, "localizedText_en.json");

            if (!File.Exists(filePath))
            {
                Debug.LogError("Fatal error");
                return;
            }
        }

        string dataAsJson = File.ReadAllText(filePath);
        loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        for (int i = 0; i < loadedData.items.Count; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText == null)
        {
            LoadLocalizedText("localizedText_en.json");
        }

        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
