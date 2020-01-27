using ScriptableObjectArchitecture.Utility;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "SceneCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_COLLECTION + "Scene",
        order = 120)]
    public class SceneCollection : Collection<SceneVariable>
    {

    }
}