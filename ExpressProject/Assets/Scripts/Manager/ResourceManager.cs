using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, Object>();
    public Manager Manager => Manager.Instance;

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if(prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {key}");
            return null;
        }
        if(pooling)
        {
            return Manager.Pool.Pop(prefab);
        }

        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        if(Manager.Pool.Push(go))
        {
            return;
        }

        Object.Destroy(go);
    }


    public T Load<T>(string key) where T : Object
    {
        if(_resources.TryGetValue(key, out Object resource))
        {
            if(typeof(T) == typeof(Sprite))
            {
                Texture2D tex = resource as Texture2D;
                Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                return spr as T;
            }
            return resource as T;
        }


        return Resources.Load<T>(key);
    }

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            if (typeof(T) == typeof(Sprite))
            {
                callback?.Invoke(resource as T);
                return;
            }
        }
            var asyncOperation = Addressables.LoadAssetAsync<T>(key);
            asyncOperation.Completed += (op) =>
            {
                _resources.Add(key, op.Result);
                callback?.Invoke(op.Result);
            };
    }
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback = null) where T : UnityEngine.Object
    {
        var OpHandel = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        OpHandel.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var reslut in op.Result)
            {
                LoadAsync<T>(reslut.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(reslut.PrimaryKey, loadCount, totalCount);
                });
            }

        };
    }

}
