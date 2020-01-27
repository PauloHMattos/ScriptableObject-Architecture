using ScriptableObjectArchitecture.References;
using UnityEngine;

namespace Assets.ScriptableObjectArchitecture.Samples.Jetpack.Scripts
{
    public class InstantiateRandomObject : MonoBehaviour
    {
        public Transform Parent;
        public GameObject [] AvailableObjects;
        public Vector3Reference [] AvailablePositions;
        public Vector3Reference [] AvailableScales;
        public FloatReference [] AvailableRotations;

        public void Spawn()
        {
            var obj = AvailableObjects[Random.Range(0, AvailableObjects.Length)];

            Vector3 pos;
            if (AvailablePositions != null && AvailablePositions.Length > 0)
            {
                pos = AvailablePositions[Random.Range(0, AvailablePositions.Length)].Value;
            }
            else
            {
                pos = transform.position;
            }

            Quaternion rot;
            if (AvailableRotations != null && AvailableRotations.Length > 0)
            {
                var euler = AvailableRotations[Random.Range(0, AvailableRotations.Length)];
                rot = Quaternion.Euler(0, 0, euler.Value);
            }
            else
            {
                rot = Quaternion.identity;
            }

            Vector3 scl;
            if (AvailableScales != null && AvailableScales.Length > 0)
            {
                scl = AvailableScales[Random.Range(0, AvailableScales.Length)].Value;
            }
            else
            {
                scl = Vector3.one;
            }

            //if (parent != null)
            {
                Instantiate(obj, pos, rot, Parent).transform.localScale = scl;
            }
        }
    }
}
