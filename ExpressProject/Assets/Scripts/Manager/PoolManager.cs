using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

class Pool
{
    GameObject _prefab;
    IObjectPool<GameObject> _pool;

    Transform _root;

    Transform Root
    {
        get
        {
            if(_root == null)
            {
                GameObject temp = new GameObject() { name = $"@{_prefab.name}Pool" };
                _root = temp.transform;
            }

            return _root;
        }
    }

    public Pool(GameObject prefab)
    {
        _prefab = prefab;
        _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    public void push(GameObject go)
    {
        if(go.activeSelf)
        {
            _pool.Release(go);
        }
    }

    public GameObject Pop()
    {
        return _pool.Get();
    }

    GameObject OnCreate()
    {
        GameObject go = GameObject.Instantiate(_prefab);
        go.transform.SetParent(Root);
        go.name = _prefab.name;
        return go;
    }

    void OnGet(GameObject go)
    {
        go.SetActive(true);
    }

    void OnRelease(GameObject go)
    {
        go.SetActive(false);
    }

    void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }
}

public class PoolManager 
{
    private Dictionary<string, Pool> _pools { get; } = new Dictionary<string, Pool>();
    public Manager Manager => Manager.Instance;

    public GameObject Pop(GameObject prefab)
    {
        if (_pools.ContainsKey(prefab.name) == false)
        {
            CreatePool(prefab);
        }
            return _pools[prefab.name].Pop();
    }

    public bool Push(GameObject go)
    {
        if (_pools.ContainsKey(go.name) == false)
        {
            return false;
        }
        _pools[go.name].push(go);
        return true;
    }

    public void Clear()
    {
        _pools.Clear();
    }

    void CreatePool(GameObject original)
    {
        Pool pool = new Pool(original);
        _pools.Add(original.name, pool);
    }
}
