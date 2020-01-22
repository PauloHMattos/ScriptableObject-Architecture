using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class InstantiateRandomObject : MonoBehaviour
{
    public Transform parent;
    public GameObjectPool [] availablePools;
    public Vector3Reference [] availablePositions;
    public Vector3Reference [] availableScales;
    public FloatReference [] availableRotations;

    public void Spawn()
    {
        var obj = availablePools[Random.Range(0, availablePools.Length)].GetGameObject();

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
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.transform.localScale = scl;
            obj.SetActive(true);

            for(int i = 0; i < obj.transform.childCount; i++)
            {
                var child = obj.transform.GetChild(i);
                child.gameObject.SetActive(true);
            }
        }
    }

    public void OnDisable()
    {
        foreach (var pool in availablePools)
        {
            pool.Clear();
        }
    }
}
