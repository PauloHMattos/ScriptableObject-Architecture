using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace Assets.ScriptableObjectArchitecture.Editor.Code_Generation
{
    public static class SoCodeGenerator
    {
        static SoCodeGenerator()
        {
            CreateTargetDirectories();
            GatherFilePaths();
        }

        private static void CreateTargetDirectories()
        {
            _targetDirectories = new string[TYPE_COUNT]
            {
                Application.dataPath + "/" + SoArchitectureSettings.Instance.CodeGenerationTargetDirectory + "/Events/Listeners",
                Application.dataPath + "/" + SoArchitectureSettings.Instance.CodeGenerationTargetDirectory + "/Events/Game Events",
                Application.dataPath + "/" + SoArchitectureSettings.Instance.CodeGenerationTargetDirectory + "/References",
                Application.dataPath + "/" + SoArchitectureSettings.Instance.CodeGenerationTargetDirectory + "/Collections",
                Application.dataPath + "/" + SoArchitectureSettings.Instance.CodeGenerationTargetDirectory + "/Events/Responses",
                Application.dataPath + "/" + SoArchitectureSettings.Instance.CodeGenerationTargetDirectory + "/Variables",
                Application.dataPath + "/" + SoArchitectureSettings.Instance.CodeGenerationTargetDirectory + "/Observers",
            };
        }
        private static void GatherFilePaths()
        {
            var assetPath = Application.dataPath;
            var folderToStartSearch = Directory.GetParent(assetPath).FullName;

            var foldersToCheck = new Queue<string>();
            foldersToCheck.Enqueue(folderToStartSearch);

            while (foldersToCheck.Count > 0)
            {
                var currentDirectory = foldersToCheck.Dequeue();

                foreach (var filePath in Directory.GetFiles(currentDirectory))
                {
                    var fileName = Path.GetFileName(filePath);

                    for (var i = 0; i < TYPE_COUNT; i++)
                    {
                        if (_templateNames[i] == fileName)
                            _templatePaths[i] = filePath;
                    }
                }
                foreach (var subDirectory in Directory.GetDirectories(currentDirectory))
                {
                    foldersToCheck.Enqueue(subDirectory);
                }
            }

            //Double check that all filepaths were found
            for (var i = 0; i < TYPE_COUNT; i++)
            {
                if (_templatePaths[i] == default)
                {
                    Debug.LogError("Couldn't find path for " + _templatePaths[i]);
                }
            }
        }

        public const int TYPE_COUNT = 7;

        public struct Data
        {
            public bool[] Types;
            public string TypeName;
            public string MenuName;
            public int Order;
        }

        private static string[] _templateNames = new string[TYPE_COUNT]
        {
            "GameEventListenerTemplate",
            "GameEventTemplate",
            "ReferenceTemplate",
            "CollectionTemplate",
            "UnityEventTemplate",
            "VariableTemplate",
            "ObserverTemplate"
        };

        private static string[] _targetFileNames = new string[TYPE_COUNT]
        {
            "{0}GameEventListener.cs",
            "{0}Variable.cs",
            "{0}Reference.cs",
            "{0}Collection.cs",
            "{0}UnityEvent.cs",
            "{0}Variable.cs",
            "{0}Observer.cs",
        };

        private static string[] _targetDirectories = null;
        private static string[] _templatePaths = new string[TYPE_COUNT];
        private static string[,] _replacementStrings = null;

        private static string TypeName => _replacementStrings[1, 1];

        public static void Generate(Data data)
        {
            _replacementStrings = new string[4, 2]
            {
            { "$TYPE$", data.TypeName },
            { "$TYPE_NAME$", CapitalizeFirstLetter(data.TypeName) },
            { "$MENU_NAME$", data.MenuName },
            { "$ORDER$", data.Order.ToString() },
            };

            for (var i = 0; i < TYPE_COUNT; i++)
            {
                if (data.Types[i])
                {
                    GenerateScript(i);
                }
            }
        }
        private static void GenerateScript(int index)
        {
            var targetFilePath = GetTargetFilePath(index);
            var contents = GetScriptContents(index);

            if (File.Exists(targetFilePath) && !SoArchitectureSettings.Instance.CodeGenerationAllowOverwrite)
            {
                Debug.Log("Cannot create file at " + targetFilePath + " because a file already exists, and overwrites are disabled");
                return;
            }

            Debug.Log("Creating " + targetFilePath);

            Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
            File.WriteAllText(targetFilePath, contents);
        }
        private static string GetScriptContents(int index)
        {
            var templatePath = _templatePaths[index];
            var templateContent = File.ReadAllText(templatePath);

            var output = templateContent;

            for (var i = 0; i < _replacementStrings.GetLength(0); i++)
            {
                output = output.Replace(_replacementStrings[i, 0], _replacementStrings[i, 1]);
            }

            return output;
        }
        private static string GetTargetFilePath(int index)
        {
            return _targetDirectories[index] + "/" + string.Format(_targetFileNames[index], TypeName);
        }
        private static string CapitalizeFirstLetter(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}