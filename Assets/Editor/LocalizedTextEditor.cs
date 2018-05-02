using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;

    Vector2 scrollPos;

    int previousLangSelection;
    int langSelection;

    [MenuItem("Window/Localized Text Editor")]
    static void Init()
    {
        GetWindow(typeof(LocalizedTextEditor)).Show();
    }

    string[] options = new string[] { "...", "ENGLISH", "SPANISH" };

    private void OnGUI()
    {
        langSelection = EditorGUILayout.Popup(langSelection, options, GUILayout.MaxWidth(150));
        if (langSelection != previousLangSelection)
        {
            LoadGameData();
        }

        scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);

        if (localizationData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Add"))
            {
                localizationData.items.Add(new LocalizationItem());
            }

            if (GUILayout.Button("Remove"))
            {
                int currentControlID = 0;
                if (GUIUtility.hotControl != 0)
                {
                    currentControlID = GUIUtility.hotControl;
                }
            }

            if (langSelection == 0)
            {
                GUI.enabled = false;
            }

            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }

            GUI.enabled = true;
        }

        GUILayout.EndScrollView();

        previousLangSelection = langSelection;
    }

    private void LoadGameData()
    {
        if (langSelection == 0)
        {
            localizationData = null;
            return;
        }

        string lang = langSelection == 1 ? "en" : "es";

        string filePath = Path.Combine(Application.streamingAssetsPath, "localizedText_" + lang + ".json");

        if (!File.Exists(filePath))
        {
            CreateNewData();
        }
        else
        {
            string dataAsJson = File.ReadAllText(filePath);
            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string lang = langSelection == 1 ? "en" : "es";

        string filePath = Path.Combine(Application.streamingAssetsPath, "localizedText_" + lang + ".json");

        string dataAsJson = JsonUtility.ToJson(localizationData);
        File.WriteAllText(filePath, dataAsJson);
    }

    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }
}