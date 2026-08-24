using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardData))]
public class CardDataEditor : Editor
{
    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        var cardName = serializedObject.FindProperty("_cardName");
        var cardDescription = serializedObject.FindProperty("_cardDescription");
        var manaCost = serializedObject.FindProperty("_manaCost");
        var requiresManualTargeting = serializedObject.FindProperty("_requiresManualTargeting");
        var manualTargetingRules = serializedObject.FindProperty("_manualTargetingRules");
        var effects = serializedObject.FindProperty("_effects");

        var titleStyle = new GUIStyle();
        titleStyle.fontSize = 18;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleLeft;
        titleStyle.normal.textColor = Color.white;


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Card Information", titleStyle);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(cardName);
        EditorGUILayout.PropertyField(cardDescription);
        EditorGUILayout.PropertyField(manaCost);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Targeting", titleStyle);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(requiresManualTargeting);

        if (requiresManualTargeting.boolValue)
        {
            EditorGUILayout.PropertyField(manualTargetingRules, true);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Card Effects", titleStyle);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(effects);

        serializedObject.ApplyModifiedProperties();
    }
}