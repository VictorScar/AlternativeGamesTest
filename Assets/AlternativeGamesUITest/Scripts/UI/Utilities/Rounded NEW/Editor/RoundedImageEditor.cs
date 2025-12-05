using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(RoundedImageV2))]
[CanEditMultipleObjects]
public class RoundedImageV2Editor : ImageEditor
{
    SerializedProperty _cornerRadius;
    SerializedProperty _softness;

    protected override void OnEnable()
    {
        base.OnEnable();
        _cornerRadius = serializedObject.FindProperty("cornerRadius");
        _softness = serializedObject.FindProperty("softness");
    }

    public override void OnInspectorGUI()
    {
        // Рисуем стандартный инспектор Image (Source Image, Color, etc)
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Rounded Settings", EditorStyles.boldLabel);

        serializedObject.Update();
        
        EditorGUILayout.PropertyField(_cornerRadius);
        EditorGUILayout.PropertyField(_softness);

        serializedObject.ApplyModifiedProperties();
    }
}