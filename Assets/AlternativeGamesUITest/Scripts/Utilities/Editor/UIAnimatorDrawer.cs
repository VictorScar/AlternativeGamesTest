using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlternativeGamesTest.UI;
using UnityEditor;
using UnityEngine;

// Этот атрибут говорит Unity использовать этот отрисовщик для всех полей типа UIAnimator
[CustomPropertyDrawer(typeof(UIAnimator), true)]
public class UIAnimatorDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Если поле раскрыто, считаем высоту всех полей внутри, иначе одна строка
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // 1. Рисуем заголовок и выпадающий список в одной строке
        Rect headerRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        
        // Разделяем заголовок на лейбл и кнопку
        Rect labelRect = headerRect;
        labelRect.width = EditorGUIUtility.labelWidth;
        
        Rect buttonRect = headerRect;
        buttonRect.x += EditorGUIUtility.labelWidth;
        buttonRect.width -= EditorGUIUtility.labelWidth;

        // Рисуем стандартный лейбл ("Show Animator")
        EditorGUI.LabelField(labelRect, label);

        // Получаем имя текущего типа для кнопки
        string typeName = "Null (None)";
        if (property.managedReferenceValue != null)
        {
            Type type = property.managedReferenceValue.GetType();
            typeName = type.Name; 
            // Можно сделать красивее, убрав "Animator" из названия
            typeName = typeName.Replace("UIAnimator", "").Replace("Animator", ""); 
        }

        // Рисуем кнопку выбора типа
        if (GUI.Button(buttonRect, new GUIContent(typeName, "Select Animator Type"), EditorStyles.popup))
        {
            ShowTypeSelectorMenu(property);
        }

        // 2. Если объект создан, рисуем его поля (стандартная отрисовка Unity)
        if (property.managedReferenceValue != null)
        {
            // Сдвигаем позицию вниз, чтобы не наехать на кнопку
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }

        EditorGUI.EndProperty();
    }

    private void ShowTypeSelectorMenu(SerializedProperty property)
    {
        GenericMenu menu = new GenericMenu();

        // Добавляем пункт "None" для очистки
        menu.AddItem(new GUIContent("None"), property.managedReferenceValue == null, () =>
        {
            property.managedReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
        });

        menu.AddSeparator("");

        // Ищем все классы, наследуемые от UIAnimator через Reflection
        var type = typeof(UIAnimator);
        var types = Assembly.GetAssembly(type).GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type));

        foreach (var t in types)
        {
            string menuPath = t.Name.Replace("UIAnimator", "").Replace("Animator", ""); 
            
            // Создаем пункт меню
            menu.AddItem(new GUIContent(menuPath), 
                property.managedReferenceValue != null && property.managedReferenceValue.GetType() == t, 
                () =>
                {
                    // Создаем новый экземпляр выбранного класса
                    property.managedReferenceValue = Activator.CreateInstance(t);
                    property.serializedObject.ApplyModifiedProperties();
                });
        }

        menu.ShowAsContext();
    }
}
