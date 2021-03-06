﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ScriptableObjectArchitecture.Attributes;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Type = System.Type;

namespace ScriptableObjectArchitecture.Editor
{
    public static class SoArchitectureEditorUtility
    {
        static SoArchitectureEditorUtility()
        {
            CreateDebugStyle();
        }

        /// <summary>
        /// A debug <see cref="GUIStyle"/> that allows for identification of EditorGUI Rect issues.
        /// </summary>
        public static GUIStyle DebugStyle { get; private set; }
        private const float DEBUG_STYLE_BACKGROUND_ALPHA = 0.33f;

        private static PropertyDrawerGraph _propertyDrawerGraph;
        private static BindingFlags _fieldBindingsFlag = BindingFlags.Instance | BindingFlags.NonPublic;

        private class AssemblyDefinitionSurrogate
        {
            // ReSharper disable once InconsistentNaming
            public string name = "";
        }

        private static void CreatePropertyDrawerGraph()
        {
            _propertyDrawerGraph = new PropertyDrawerGraph();
            var assemblyNamesToCheck = new HashSet<string>()
            {
                "Assembly-CSharp-Editor",
            };

            GetAllAssetDefintionNames(assemblyNamesToCheck);

            var dataPath = Application.dataPath;
            var libraryPath = dataPath.Substring(0, dataPath.LastIndexOf('/')) + "/Library/ScriptAssemblies";

            foreach (var file in Directory.GetFiles(libraryPath))
            {
                if (assemblyNamesToCheck.Contains(Path.GetFileNameWithoutExtension(file)) && Path.GetExtension(file) == ".dll")
                {
                    var assembly = Assembly.LoadFrom(file);
                    _propertyDrawerGraph.CreateGraph(assembly);
                }
            }
        }
        private static void GetAllAssetDefintionNames(HashSet<string> targetList)
        {
            var assemblyDefinitionGuiDs = AssetDatabase.FindAssets("t:asmdef");

            foreach (var guid in assemblyDefinitionGuiDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                if (path.StartsWith("Assets/"))
                {
                    var fullPath = Application.dataPath + path.Remove(0, path.IndexOf('/'));

                    targetList.Add(GetNameValueFromAssemblyDefinition(fullPath));
                }
            }
        }
        private static string GetNameValueFromAssemblyDefinition(string fullpath)
        {
            var allText = File.ReadAllText(fullpath);
            var surrogate = JsonUtility.FromJson<AssemblyDefinitionSurrogate>(allText);

            return surrogate.name;
        }
        private static void CreateDebugStyle()
        {
            DebugStyle = new GUIStyle();

            var debugColor = Color.magenta;
            debugColor.a = DEBUG_STYLE_BACKGROUND_ALPHA;

            DebugStyle.normal.background = CreateTexture(2, 2, debugColor);
        }

        /// <summary>
        /// Converts the entire rect of a <see cref="UnityEditorInternal.ReorderableList"/> element into a rect used for displaying a field
        /// </summary>
        public static Rect GetReorderableListElementFieldRect(Rect elementRect)
        {
            elementRect.height = EditorGUIUtility.singleLineHeight;
            elementRect.y++;

            return elementRect;
        }
        public static bool SupportsMultiLine(Type type)
        {
            return type.GetCustomAttributes(typeof(MultiLine), true).Length > 0;
        }
        public static bool HasPropertyDrawer(Type type)
        {
            if (HasBuiltinPropertyDrawer(type))
                return true;

            if (_propertyDrawerGraph == null)
                CreatePropertyDrawerGraph();

            return _propertyDrawerGraph.HasPropertyDrawer(type);
        }
        private static bool HasBuiltinPropertyDrawer(Type type)
        {
            if (type.IsPrimitive || type == typeof(string) || IsFromUnityAssembly(type))
                return true;

            return false;
        }
        private static bool IsFromUnityAssembly(Type type)
        {
            return type.Assembly == typeof(GameObject).Assembly;
        }
        [DidReloadScripts]
        private static void OnProjectReloaded()
        {
            _propertyDrawerGraph = null;
        }
        private static Texture2D CreateTexture(int width, int height, Color col)
        {
            var pix = new Color[width * height];
            for (var i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        /// <summary>
        /// Goes through the entirety of the project and collects data about custom property drawers
        /// </summary>
        private class PropertyDrawerGraph
        {
            private List<Type> _supportedTypes = new List<Type>();
            private List<Type> _supportedInheritedTypes = new List<Type>();
            private List<Assembly> _checkedAssemblies = new List<Assembly>();

            public bool HasPropertyDrawer(Type type)
            {
                foreach (var supportedType in _supportedTypes)
                {
                    if (supportedType == type)
                        return true;
                }

                foreach (var inheritedSupportedType in _supportedInheritedTypes)
                {
                    if (type.IsSubclassOf(inheritedSupportedType))
                        return true;
                }

                return false;
            }
            public void CreateGraph(Assembly assembly)
            {
                if (_checkedAssemblies.Contains(assembly))
                    return;

                _checkedAssemblies.Add(assembly);

                foreach (var type in assembly.GetTypes())
                {
                    var attributes = type.GetCustomAttributes(typeof(CustomPropertyDrawer), false);

                    foreach (var attribute in attributes)
                    {
                        if (attribute is CustomPropertyDrawer)
                        {
                            var drawerData = attribute as CustomPropertyDrawer;

                            var useForChildren = (bool)typeof(CustomPropertyDrawer).GetField("m_UseForChildren", _fieldBindingsFlag).GetValue(drawerData);
                            var targetType = (Type)typeof(CustomPropertyDrawer).GetField("m_Type", _fieldBindingsFlag).GetValue(drawerData);

                            if (useForChildren)
                            {
                                _supportedInheritedTypes.Add(targetType);
                            }
                            else
                            {
                                _supportedTypes.Add(targetType);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static object GetTargetObjectOfProperty(this SerializedProperty prop)
        {
            if (prop == null) return null;

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (var i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }
    }
}