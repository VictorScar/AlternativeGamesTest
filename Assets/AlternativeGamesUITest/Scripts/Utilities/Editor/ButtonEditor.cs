#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;

[CanEditMultipleObjects]
[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Получаем все методы с атрибутом Button
        MethodInfo[] methods = target.GetType().GetMethods(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (MethodInfo method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(ButtonAttribute), true);
            if (attributes.Length > 0)
            {
                if (method.GetParameters().Length == 0)
                {
                    ButtonAttribute buttonAttribute = (ButtonAttribute)attributes[0];
                    string buttonName = string.IsNullOrEmpty(buttonAttribute.Name)
                        ? ObjectNames.NicifyVariableName(method.Name)
                        : buttonAttribute.Name;

                    if (GUILayout.Button(buttonName))
                    {
                        method.Invoke(target, null);
                    }
                }
            }
        }
    }
}
#endif