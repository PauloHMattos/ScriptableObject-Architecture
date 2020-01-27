using System;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TagAttribute : MultiPropertyAttribute
    {
        public TagAttribute()
        {
        }

#if UNITY_ENGINE
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                // generate the taglist + custom tags
                List<string> tagList = new List<string>();
                tagList.Add("(None)");
                tagList.Add("Untagged");
                tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

                string propertyString = property.stringValue;
                int index = 0;
                // check if there is an entry that matches the entry and get the index
                // we skip index 0 as that is a special custom case
                for (int i = 1; i < tagList.Count; i++)
                {
                    if (tagList[i] == propertyString)
                    {
                        index = i;
                        break;
                    }
                }

                //index = EditorGUILayout.Popup(label, index, tagList.ToArray());
                // Draw the popup box with the current selected index
                index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                // Adjust the actual string value of the property based on the selection
                if (index > 0)
                {
                    property.stringValue = tagList[index];
                }
                else
                {
                    property.stringValue = string.Empty;
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
                string message = string.Format("{0} supports only string fields", typeof(TagAttribute).Name);
                EditorGUILayout.HelpBox(message, MessageType.Warning);
            }
        }
#endif
    }
}