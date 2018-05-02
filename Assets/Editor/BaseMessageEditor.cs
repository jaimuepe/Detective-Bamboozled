using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseMessageEditor : Editor
{

    /*
    public override void OnInspectorGUI()
    {
        DialogueTree dialogueTree = (DialogueTree)target;

        dialogueTree.dialogueId = EditorGUILayout.TextField("Dialogue Id", dialogueTree.dialogueId);

        EditorGUI.indentLevel++;

        for (int i = 0; i < dialogueTree.messages.Count; i++)
        {
            DBaseMessage message = dialogueTree.messages[i];
            SetupEditorForMessage(message);
        }

        EditorGUI.indentLevel--;

        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("MESSAGE"))
            {
                dialogueTree.messages.Add(new DBaseMessage());
            }
            if (GUILayout.Button("QUESTION"))
            {
                dialogueTree.messages.Add(new DQuestion());
            }
        };
    }

    void SetupEditorForMessage(DBaseMessage m)
    {
        DialogueTree dialogueTree = (DialogueTree)target;

        using (new GUILayout.HorizontalScope())
        {
            m.messageId = EditorGUILayout.TextField("Message Id", m.messageId);

            if (GUILayout.Button("X", GUILayout.ExpandWidth(false)))
            {
                dialogueTree.messages.Remove(m);
            }
        };

        m.ownerId = EditorGUILayout.TextField("Owner Id", m.ownerId);
        m.text = EditorGUILayout.TextArea(m.text, GUILayout.MaxHeight(75));

        DQuestion q = m as DQuestion;
        if (q != null)
        {
            SetupEditorForQuestion(q);
        }
    }

    void SetupEditorForQuestion(DQuestion q)
    {
        EditorGUI.indentLevel++;

        GUILayout.Label("Answers:");

        for (int j = 0; j < q.answers.Count; j++)
        {
            DAnswer answer = q.answers[j];
            SetupEditorForMessage(answer);
        }

        if (GUILayout.Button("ADD", GUILayout.ExpandWidth(false)))
        {
            q.answers.Add(new DAnswer());
        }

        EditorGUI.indentLevel--;
    }
     */
}