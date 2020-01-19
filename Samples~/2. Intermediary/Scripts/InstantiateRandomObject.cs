using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class InstantiateRandomObject : MonoBehaviour
{
    public Transform parent;
    public GameObject [] availableObjects;
    public Vector3Reference [] availablePositions;
    public Vector3Reference [] availableScales;
    public FloatReference [] availableRotations;

    public void Spawn()
    {
        var obj = availableObjects[Random.Range(0, availableObjects.Length)];

        Vector3 pos;
        if (availablePositions != null && availablePositions.Length > 0)
        {
            pos = availablePositions[Random.Range(0, availablePositions.Length)].Value;
        }
        else
        {
            pos = transform.position;
        }

        Quaternion rot;
        if (availableRotations != null && availableRotations.Length > 0)
        {
            var euler = availableRotations[Random.Range(0, availableRotations.Length)];
            rot = Quaternion.Euler(0, 0, euler.Value);
        }
        else
        {
            rot = Quaternion.identity;
        }

        Vector3 scl;
        if (availableScales != null && availableScales.Length > 0)
        {
            scl = availableScales[Random.Range(0, availableScales.Length)].Value;
        }
        else
        {
            scl = Vector3.one;
        }

        //if (parent != null)
        {
            Instantiate(obj, pos, rot, parent).transform.localScale = scl;
        }
    }
}
