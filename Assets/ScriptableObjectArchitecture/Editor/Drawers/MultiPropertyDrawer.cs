using System.Linq;
using ScriptableObjectArchitecture.Attributes;
using UnityEditor;
using UnityEngine;

namespace Assets.ScriptableObjectArchitecture.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(MultiPropertyAttribute), true)]
    public class MultiPropertyDrawer : PropertyDrawer
    {
        private MultiPropertyAttribute RetrieveAttributes()
        {
            var mAttribute = attribute as MultiPropertyAttribute;

            // Get the attribute list, sorted by "order".
            if (mAttribute.Stored == null)
            {
                mAttribute.Stored = fieldInfo.GetCustomAttributes(typeof(MultiPropertyAttribute), false).OrderBy(s => ((PropertyAttribute)s).order);
            }

            return mAttribute;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var mAttribute = RetrieveAttributes();

            // If the attribute is invisible, regain the standard vertical spacing.
            foreach (MultiPropertyAttribute attr in mAttribute.Stored)
                if (!attr.IsVisible(property))
                    return -EditorGUIUtility.standardVerticalSpacing;

            // In case no attribute returns a modified height, return the property's default one:
            var height = base.GetPropertyHeight(property, label);

            // Check if any of the attributes wants to modify height:
            foreach (var atr in mAttribute.Stored)
            {
                if (atr as MultiPropertyAttribute != null)
                {
                    var tempheight = ((MultiPropertyAttribute)atr).GetPropertyHeight(property, label);
                    if (tempheight.HasValue)
                    {
                        height = tempheight.Value;
                        break;
                    }
                }
            }
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var mAttribute = RetrieveAttributes();

            // Calls to IsVisible. If it returns false for any attribute, the property will not be rendered.
            foreach (MultiPropertyAttribute attr in mAttribute.Stored)
                if (!attr.IsVisible(property)) return;

            // Calls to OnPreRender before the last attribute draws the UI.
            foreach (MultiPropertyAttribute attr in mAttribute.Stored)
                attr.OnPreGUI(position, property, label);

            // The last attribute is in charge of actually drawing something:
            ((MultiPropertyAttribute)mAttribute.Stored.Last()).OnGUI(position, property, label);

            // Calls to OnPostRender after the last attribute draws the UI. These are called in reverse order.
            foreach (MultiPropertyAttribute attr in mAttribute.Stored.Reverse())
                attr.OnPostGUI(position, property);
        }
    }
}