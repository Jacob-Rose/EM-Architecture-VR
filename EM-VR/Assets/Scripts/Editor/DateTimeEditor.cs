using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TimelineDate))]
public class DateTimeEditor : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // __prefab__ override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var dayRect = new Rect(position.x, position.y, 30, position.height);
        var monthRect = new Rect(position.x + 35, position.y, 50, position.height);
        var yearRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(dayRect, property.FindPropertyRelative("day"), GUIContent.none);
        EditorGUI.PropertyField(monthRect, property.FindPropertyRelative("month"), GUIContent.none);
        EditorGUI.PropertyField(yearRect, property.FindPropertyRelative("year"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
