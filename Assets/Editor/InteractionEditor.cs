using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class InteractionEditor : EditorWindow
{
    InteractionData interaction;

    [MenuItem("Window/Interactions Editor")]
    static void Init()
    {
        GetWindow(typeof(InteractionEditor)).Show();
    }

    Vector2 scrollPosition = Vector2.zero;

    private void OnGUI()
    {


        if (interaction != null)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            interaction.id = EditorGUILayout.TextField("Interaction Id", interaction.id);

            for (int i = 0; i < interaction.nodes.Count; i++)
            {
                Node n = interaction.nodes[i];

                RenderSpecificNodeData(n);

                n.evtId = EditorGUILayout.TextField("Event Id", n.evtId);

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }

            GUILayout.EndScrollView();

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Add text node"))
                {
                    interaction.nodes.Add(new TextNode("", "", "", ""));
                }

                if (GUILayout.Button("Add choice node"))
                {
                    interaction.nodes.Add(new ChoiceNode("", new List<ChoiceNodeOption>()));
                }

                if (GUILayout.Button("Add branch node"))
                {
                    interaction.nodes.Add(new BranchNode("", new List<BranchNodeOption>()));
                }

                if (GUILayout.Button("Add custom node"))
                {
                    interaction.nodes.Add(new CustomNode("", ""));
                }
            }
        }


        using (new GUILayout.HorizontalScope())
        {
            GUI.enabled = interaction != null && interaction.id != null;

            if (GUILayout.Button("Save data"))
            {
                SaveData();
            }

            GUI.enabled = true;

            if (GUILayout.Button("Load data"))
            {
                LoadInteraction();
            }

            if (GUILayout.Button("New"))
            {
                CreateInteraction();
            }
        }
    }

    private void RenderSpecificNodeData(Node n)
    {
        ChoiceNode cNode = n as ChoiceNode;
        if (cNode)
        {
            RenderChoiceNode(cNode);
            return;
        }

        TextNode tNode = n as TextNode;

        if (tNode)
        {
            RenderTextNode(tNode);
            return;
        }

        BranchNode bNode = n as BranchNode;

        if (bNode)
        {
            RenderBranchNode(bNode);
            return;
        }

        CustomNode cmNode = n as CustomNode;

        if (cmNode)
        {
            RenderCustomNode(cmNode);
            return;
        }

        throw new Exception("Uknown type");
    }

    private void RenderTextNode(TextNode tNode)
    {
        EditorGUILayout.LabelField("Text Node", EditorStyles.boldLabel);

        tNode.ownerId = EditorGUILayout.TextField("Owner id", tNode.ownerId);

        tNode.id = EditorGUILayout.TextField("Node id", tNode.id);
        GUI.enabled = false;
        // EditorGUILayout.TextArea(LocalizationManager.instance.GetLocalizedValue(tNode.id));
        GUI.enabled = true;

        tNode.nextNodeId = EditorGUILayout.TextField("Next node id", tNode.nextNodeId);

        if (tNode.nextNodeId != Database.EXIT_NODE_ID)
        {
            /*
            Node nextNode = Database.instance.GetNode(tNode.nextNodeId);

            if (nextNode.GetType() == typeof(TextNode))
            {
                GUI.enabled = false;
                EditorGUILayout.TextArea(LocalizationManager.instance.GetLocalizedValue(tNode.nextNodeId));
                GUI.enabled = true;
            }
            */
        }

        tNode.emphasis = EditorGUILayout.TextField("Emphasis", tNode.emphasis);
    }

    private void RenderChoiceNode(ChoiceNode cNode)
    {
        EditorGUILayout.LabelField("Choice Node", EditorStyles.boldLabel);

        cNode.id = EditorGUILayout.TextField("node id", cNode.id);

        for (int i = 0; i < cNode.options.Count; i++)
        {
            ChoiceNodeOption cno = cNode.options[i];
            Condition condition = cno.condition;

            using (new GUILayout.HorizontalScope())
            {
                cno.nodeId = EditorGUILayout.TextField("Option node id", cno.nodeId);
                cno.condition.eventId = EditorGUILayout.TextField("Trigger id", cno.condition.eventId);
                cno.condition.type = (Condition.ConditionType)EditorGUILayout.EnumPopup("Condition type:", cno.condition.type);
            }
        }

        if (GUILayout.Button("Add choice"))
        {
            cNode.options.Add(new ChoiceNodeOption("node id"));
        }
    }

    private void RenderBranchNode(BranchNode bNode)
    {
        EditorGUILayout.LabelField("Branch Node", EditorStyles.boldLabel);

        bNode.id = EditorGUILayout.TextField("Node id", bNode.id);

        for (int i = 0; i < bNode.options.Count; i++)
        {
            BranchNodeOption bno = bNode.options[i];
            Condition condition = bno.condition;

            using (new GUILayout.HorizontalScope())
            {
                bno.nodeId = EditorGUILayout.TextField("Subnode id", bno.nodeId);
                bno.condition.eventId = EditorGUILayout.TextField("Trigger id", bno.condition.eventId);
                bno.condition.type = (Condition.ConditionType)EditorGUILayout.EnumPopup("Condition type:", bno.condition.type);
            }
        }

        if (GUILayout.Button("Add choice"))
        {
            bNode.options.Add(new BranchNodeOption("node id"));
        }
    }

    private void RenderCustomNode(CustomNode cmNode)
    {
        EditorGUILayout.LabelField("Custom Node", EditorStyles.boldLabel);
        cmNode.id = EditorGUILayout.TextField("Node id", cmNode.id);
        cmNode.nextNodeId = EditorGUILayout.TextField("Node id", cmNode.nextNodeId);
    }

    private void CreateInteraction()
    {
        if (interaction != null)
        {
            if (!EditorUtility.DisplayDialog("You sure m8?", "This will erase unsaved changes.", "ok"))
            {
                return;
            }
        }
        interaction = new InteractionData();
    }

    private void LoadInteraction()
    {
        string path = EditorUtility.OpenFilePanel("Choose the interaction file", Application.streamingAssetsPath + "/interactions", "json");
        string dataAsJson = File.ReadAllText(path);

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        interaction = JsonConvert.DeserializeObject<InteractionData>(dataAsJson, settings);
    }

    private void SaveData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "interactions/" + interaction.id + ".json");

        if (File.Exists(filePath))
        {
            if (!EditorUtility.DisplayDialog("You sure m8?", "This will overwrite the current file.", "ok"))
            {
                return;
            }
        }

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        string dataAsJson = JsonConvert.SerializeObject(interaction, settings);
        File.WriteAllText(filePath, dataAsJson);
    }
}