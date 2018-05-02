using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocalizationItem))]
public class LocalizationItemEditor : PropertyDrawer
{

    static int keyRectWidth = 250;
    static int xOffset = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.indentLevel++;

        var keyRect = new Rect(position.x, position.y, keyRectWidth, position.height);
        var valueRect = new Rect(
            position.x + keyRectWidth + xOffset, 
            position.y, position.width - keyRectWidth - xOffset, 
            position.height);

        EditorGUI.PropertyField(keyRect, property.FindPropertyRelative("key"), GUIContent.none);
        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);

        EditorGUI.EndProperty();

        EditorGUI.indentLevel--;
    }
}
