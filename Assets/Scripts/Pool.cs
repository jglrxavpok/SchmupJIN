using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PrefabPool {
    private readonly GameObject _prefab;

    private List<GameObject> free = new List<GameObject>();

    public PrefabPool(GameObject prefab) {
        _prefab = prefab;
    }
    
    public GameObject Retrieve(Vector3 position, Quaternion rotation, [CanBeNull] Transform parent = null) {
        GameObject obj = GetOrNew();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.parent = parent;
        return obj;
    }
    
    private GameObject CreateNew() {
        return Object.Instantiate(_prefab);
    }

    private GameObject GetOrNew() {
        if (free.Count != 0) {
            GameObject result = free[0];
            free.RemoveAt(0);
            result.SetActive(true);
            return result;
        }

        return CreateNew();
    }

    public void Free(GameObject obj) {
        obj.SetActive(false);
        free.Add(obj);
    }

    ~PrefabPool() {
        foreach (var o in free) {
            Object.Destroy(o);
        }
    }
}
