using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace ScriptableObjectArchitecture.Utility
{
    public class SoArchitectureSettings : ScriptableObject
    {
        #region Singleton
        public static SoArchitectureSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GetInstance();

                return _instance;
            }
        }
        private static SoArchitectureSettings _instance;

        private static SoArchitectureSettings GetInstance()
        {
#if UNITY_EDITOR
            var instance = FindInstanceInProject();

            if (instance == null)
                return CreateInstance();

            return instance;
#else
            return null;
#endif
        }
        private static SoArchitectureSettings FindInstanceInProject()
        {
#if UNITY_EDITOR
            var settingsGuiDs = AssetDatabase.FindAssets(ASSET_DATABASE_SEARCH_STRING);

            if (settingsGuiDs.Length == 0)
            {
                return null;
            }
            else if (settingsGuiDs.Length > 1)
            {
                Debug.LogWarning("Found more than one instance of SOArchitecture_Settings, you've probably created several SOA settings objects." +
                    $"\nTo find all instances, type {ASSET_DATABASE_SEARCH_STRING} into the project view search bar");
                return null;
            }
            else
            {
                var settingsPath = AssetDatabase.GUIDToAssetPath(settingsGuiDs[0]);
                return AssetDatabase.LoadAssetAtPath<SoArchitectureSettings>(settingsPath);
            }
#else
            throw new System.NullReferenceException();
#endif
        }
        private static SoArchitectureSettings CreateInstance()
        {
#if UNITY_EDITOR
            var newSettings = CreateInstance<SoArchitectureSettings>();

            AssetDatabase.CreateAsset(newSettings, DEFAULT_NEW_SETTINGS_LOCATION + DEFAULT_NEW_SETTINGS_NAME);
            AssetDatabase.SaveAssets();

            Selection.activeObject = newSettings;

            Debug.LogWarning("No SOArchitecture_Settings asset found! " +
                "Created new one at asset root, feel free to move it wherever you please in your project.", newSettings);

            return newSettings;
#else
        throw new System.NullReferenceException();
#endif
        }

        private const string ASSET_DATABASE_SEARCH_STRING = "t:SOArchitecture_Settings";
        private const string DEFAULT_NEW_SETTINGS_LOCATION = "Assets\\";
        private const string DEFAULT_NEW_SETTINGS_NAME = "SOArchitecture_Settings.asset";
        #endregion

        public string CodeGenerationTargetDirectory
        {
            get => _codeGenerationTargetDirectory;
            set => _codeGenerationTargetDirectory = value;
        }

        public bool CodeGenerationAllowOverwrite
        {
            get => _codeGenerationAllowOverwrite;
            set => _codeGenerationAllowOverwrite = value;
        }

        public int DefaultCreateAssetMenuOrder
        {
            get => _defualtCreateAssetMenuOrder;
            set => _defualtCreateAssetMenuOrder = value;
        }

        [SerializeField]
        private string _codeGenerationTargetDirectory = "CODE_GENERATION";

        [SerializeField, Tooltip("Allow newly generated code files to overwrite existing ones")]
        private bool _codeGenerationAllowOverwrite = false;

        [SerializeField]
        private int _defualtCreateAssetMenuOrder = 120;
    }
}