using UnityEditor;
using UnityEngine;

namespace Utils.ReadOnlyField 
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            var previousGUIState = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = previousGUIState;
        }
    }
}
